using FlatBuffers;
using Game.Table.Static;
using Game.Tools.CommonTools;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Game.Lwn.Main
{
    public class TableLoader
    {
        private TableConfigVO _tableConfigVO;

        private string _tableName = "";
        public string TableName {
            get { return _tableName; }
        }

        public TableConfigVO TableConfig {
            get { return _tableConfigVO; }
        }


        public TableLoader(TableConfigVO data) {
            _tableConfigVO = data;
            _tableName = _tableConfigVO.Name;
        }

        private Dictionary<int, object> _dataDict = null;
        public Dictionary<int, object> TableDataDict{
            get { return _dataDict; }
        }

        private byte[] _byteData;

        public void Load() {
            //TableConfig表已提前加载
            if (_tableConfigVO.Id<=0) {
                //GameLog.Error("ERROR!!!----TableLoader:Load----_tableConfigVO.Id<0");
                return;
            }
            if (_dataDict != null)
            {
                GameLog.Error("ERROR!!!----TableLoader:Load----重复加载表:" + _tableName);
                return;
            }
            _byteData = ResourceManager.Instance.LoadTable(_tableConfigVO.ResourcePath + ".bin");
            Type tableType = Type.GetType("Game.Table.Static."+_tableName + "Table");
            //Type tableVOType = Type.GetType(_tableName+"VO");

            string methodName = "GetRootAs{0}Table".Replace("{0}", _tableName);
            object obj = tableType.InvokeMember(methodName, BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public, null, null
                , new object[] { new ByteBuffer(_byteData) });
            MethodInfo method = tableType.GetMethod("GetTableData");
            _dataDict = (Dictionary<int, object>)method.Invoke(obj, null);
            if (_dataDict == null) {
                GameLog.Error("ERROR!!!----TableLoader:Load----表{0}序列化失败", _tableName);
            }
        }

    }
}
