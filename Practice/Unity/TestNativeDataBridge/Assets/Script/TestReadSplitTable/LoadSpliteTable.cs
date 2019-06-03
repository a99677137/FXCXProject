using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using FlatBuffers;
using System.Reflection;

public class LoadSpliteTable : MonoBehaviour {

    private string msg = "";

    private string tableName = "";

    private string tableId = "";


    private string basePath = "";
    private string tableConfigName = "/TableConfig.bin";
    private string tableDataPath = "/Table/";


    private List<TableConfig> csharpTable;

    // Use this for initialization
    void Start()
    {
        InitPath();
    }


    void OnGUI() {
        if (GUI.Button(new Rect(5, 5, 150, 60), "StartLoadData"))
        {
            msg += "Click Button:StartLoadData!\n";
            //var res = NativeDataBridge.CreateBufferFromFile(filename, 0, 0, (uint)filename.Length);
            //msg += "CreateBufferFromFile's result=" + res + "\n";
            LoadAllTable();
        }

        GUI.Label(new Rect(5, 70, 800, 40), "表名：1CommonItem,2Emotion,3MonsterAttrExTable,4SkillData_V1,5SkillTemplate_V1,6StrDictionary");
        GUI.Label(new Rect(5, 100, 40, 40), "表名：");
        tableName = GUI.TextField(new Rect(40, 95, 200, 30), tableName);

        GUI.Label(new Rect(5, 130, 50, 40), "Id：");
        tableId = GUI.TextField(new Rect(40, 125, 200, 30), tableId);

        if (GUI.Button(new Rect(5, 175, 150, 50), "ReadData")) {
            msg += "Click Button:ReadData!\n";
            string tabName = GetTableNameById(int.Parse(tableName));
            msg += "TableName = " + tabName + " TableId = " + tableId + " \n";

        }

        GUI.TextArea(new Rect(5, Screen.height - 205, Screen.width - 10, 200), msg);
    }


    private void LoadAllTable() {
        AnalysisTableConfig();
    }

    private void GetDataByTabNameAndTabId(int tableName, int tableId) {

    }


    private void InitPath() {
        basePath = Application.streamingAssetsPath;
        tableDataPath = basePath + tableDataPath;
    }


    private void LoadTableData() {
        if (csharpTable == null || csharpTable.Count <=0) {
            UnityEditor.EditorUtility.DisplayDialog("Error", "解析TableConfig.bin文件出错！", "确定");
            return;
        }
        for (int i = 0; i<csharpTable.Count;i++) {
            var item = csharpTable[i];
            try {
                //加载数据
                byte[] data = File.ReadAllBytes(tableDataPath + item.TableName + ".bin");
                //准备解析
                Type type = Type.GetType(string.Format("{0}.{1}", item.NameSpace, item.TableName));
                ByteBuffer bytebuf = new ByteBuffer(data);
                object obj = type.InvokeMember("GetRootAs" + item.TableName, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                                                null, null, new object[] { bytebuf });

            }
            catch (Exception e) {

            }
        }

    }

    private void AnalysisTableConfig() {
        string path = basePath + tableConfigName;
        try
        {
            csharpTable = new List<TableConfig>();
            FileStream fs = new FileStream(path, FileMode.Open,FileAccess.Read);
            BinaryFormatter binFormatter = new BinaryFormatter();
            BinaryReader br = new BinaryReader(fs);
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            MemoryStream ms = null;
            while (br.BaseStream.Position <= br.BaseStream.Length) {
                //Debug.Log("<color=#00aa88>----CurPos = " + br.BaseStream.Position + "</color>");
                Int64 startIdx = br.ReadInt64();
                //Debug.Log("<color=#00aa88>----startIdx = " + startIdx + "</color>");
                Int32 nameLength = br.ReadInt32();
                byte[] namebyte = br.ReadBytes(nameLength);
                string name = System.Text.Encoding.Default.GetString(namebyte);

                Int32 dataLength = br.ReadInt32();
                byte[] data = br.ReadBytes(dataLength);

                ms = new MemoryStream(data);
                TableConfig item = (TableConfig)binFormatter.Deserialize(ms);
                //Debug.Log("<color=#00aa88>----tableName = " +item.TableName+"</color>");
                csharpTable.Add(item);
                ms.Close();
                ms.Dispose();
            }
        }
        catch (IOException e)
        {
            msg += "AnalysisTableConfig Exception="+e.ToString()+"!\n";
            return;
        }
        



    }


    private string GetTableNameById(int Id) {
        string tabName = "";
        switch (Id) {
            case 1:
                tabName = "CommonItem";
                break;
            case 2:
                tabName = "Emotion";
                break;
            case 3:
                tabName = "MonsterAttrExTable";
                break;
            case 4:
                tabName = "SkillData_V1";
                break;
            case 5:
                tabName = "SkillTemplate_V1";
                break;
            case 6:
                tabName = "StrDictionary";
                break;

        }
        return tabName;
    }

    
	
	// Update is called once per frame
	void Update () {
        
    }
}
