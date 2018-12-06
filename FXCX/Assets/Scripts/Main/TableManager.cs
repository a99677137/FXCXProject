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
        private void StartLoadTable() {
            if (_tableLoaderList.Count <= 0)
            {
                GameLog.Error("ERROR!!!-------------TableManager:StartLoadTable----没有可以加载的表格！");
                return;
            }
            if (_firstLoadFinish) {
                GameLog.Error("ERROR!!!-------------TableManager:StartLoadTable----重复加载初始化表格！");
                return;
            }
            _loadThread = new Thread(new ParameterizedThreadStart(LoadTable));
            _loadThread.Start();
        }

        private void LoadTable(object param) {
            
            foreach (var item in _tableLoaderList.Values)
            {
                if (item.TableConfig.IsCSharp && item.TableConfig.IsImmediately)
                {
                    item.Load();
                }
            }

            _firstLoadFinish = true;
        }


    }
}