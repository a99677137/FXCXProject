using FlatBuffers;
using Game.Table.Static;
using Game.Tools.CommonTools;
using System.Collections.Generic;
using System.Threading;

namespace Game.Lwn.Main
{
    public class TableManager : Singleton<TableManager>
    {
        //PA 策略：安卓表全部加载，iOS表现用现加载
        private TableConfigTable tableConfig;

        private Dictionary<string, TableLoader> _tableLoaderList = new Dictionary<string, TableLoader>();

        private Thread _loadThread = null;

        private bool _firstLoadFinish = false;

        public void Init()
        {
            LoadTableConfig();
        }

        #region GetDataTables

        public object GetTableData(string name,int id){
            if (string.IsNullOrEmpty(name) || id <= 0)
            {
                GameLog.Error("ERROR!!!----TableManager:GetTableData----param is wrong!!!");
                return null;
            }
            Dictionary<int, object> table = GetTableByName(name);
            if(table == null){
                GameLog.Error("ERROR!!!----TableManager:GetTableData----get table failed!!! name = {0}", name);
                return null;
            }
            object tableLine = null;
            if(table.TryGetValue(id,out tableLine)){
                return tableLine;
            }else{
                GameLog.Error("ERROR!!!----TableManager:GetTableData----Table {0} not find id = {1}!!", name,id.ToString());
            }
            return tableLine;
        }


        public Dictionary<int,object> GetTableByName(string name){
            if (string.IsNullOrEmpty(name))
            {
                GameLog.Error("ERROR!!!----TableManager:GetTableByName----name is null!!!");
                return null;
            }
            var loader = GetTableLoaderByName(name);
            if (loader == null)
            {
                GameLog.Error("ERROR!!!----TableManager:GetTableByName----TableLoader get failed!! name = {0}", name);
                return null;
            }
            return loader.TableDataDict;

        }
        


        private TableLoader GetTableLoaderByName(string name){
            TableLoader table = null;
            if(string.IsNullOrEmpty(name)){
                GameLog.Error("ERROR!!!----TableManager:GetTableLoaderByName----name is null!!!");
                return table;
            }
            if(!_tableLoaderList.ContainsKey(name)){
                GameLog.Error("ERROR!!!----TableManager:GetTableLoaderByName----TableConfig don't have table which name is {0}!!!", name);
                return table;
            }

            if(_tableLoaderList.TryGetValue(name,out table)){
                if(table.TableDataDict == null){
                    table.Load();
                }
            }else{
                GameLog.Error("ERROR!!!----TableManager:GetTableLoaderByName----TableLoaderList Get Fail!!! name = {0}", name);
            }

            return table;
        }

        #endregion


        #region GameInitLoadTables
        private void LoadTableConfig()
        {
            byte[] data = ResourceManager.Instance.LoadTableConfig();
            ByteBuffer byteBuffer = new ByteBuffer(data);
            tableConfig = TableConfigTable.GetRootAsTableConfigTable(byteBuffer);
            for (int i = 0; i < tableConfig.DataLength; i++)
            {
                TableConfigVO tableItem = (TableConfigVO)tableConfig.Data(i);
                TableLoader loader = new TableLoader(tableItem);
                _tableLoaderList.Add(tableItem.Name, loader);
            }
            StartLoadTable();

        }
        private void StartLoadTable()
        {
            if (_tableLoaderList.Count <= 0)
            {
                GameLog.Error("ERROR!!!-------------TableManager:StartLoadTable----没有可以加载的表格！");
                return;
            }
            if (_firstLoadFinish)
            {
                GameLog.Error("ERROR!!!-------------TableManager:StartLoadTable----重复加载初始化表格！");
                return;
            }
            _loadThread = new Thread(new ParameterizedThreadStart(LoadAllTable));
            _loadThread.Start();
        }

        private void LoadAllTable(object param)
        {

            foreach (var item in _tableLoaderList.Values)
            {
                if (item.TableConfig.IsCSharp && item.TableConfig.IsImmediately)
                {
                    item.Load();
                }
            }

            _firstLoadFinish = true;
            _loadThread.Abort();
            _loadThread = null;
        }
#endregion

    }
}