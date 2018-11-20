using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Text;
using Game.Lwn.Base;
using UnityEditor;
using System.Text.RegularExpressions;

class TableConfigItem {
    
}

public class TableToolManager  {
    private static TableToolManager _instance = null;
    public static TableToolManager  Instance {
        get { if (_instance == null) return _instance = new TableToolManager(); else return _instance; }
    }

    public static string BasePath = Application.dataPath;

    public string TableConfigPath = TableToolManager.BasePath + "/../../TableRes/";

    public static string FBOutputPath = TableToolManager.BasePath + "/../FB_Output";

    public string Ready2ChangeTablesPath = FBOutputPath + "/Ready2ChangeTables/";
    public string FbsPath = FBOutputPath +"/fbs/";
    public string JsonPath = FBOutputPath+"/json/";

    public string PrintMsg = "";

    public string TablePath = "";

    private string tableConfig = "TableConfig.txt";

    public Dictionary<int, TableItemVO> TableList = new Dictionary<int, TableItemVO>();

    public Dictionary<int, FileInfo> TableFileList = new Dictionary<int, FileInfo>();

    private List<string> tableConfigLineData = new List<string>(); 

    private List<string> columnNames = new List<string>();
    private List<string> typeNames = new List<string>();
    private List<string> dataValues = new List<string>();

    private string fbsModle = @"namespace {0}; 
    table {1}VO{ 
        {2} 
    } 

    table {1}Table{
        data:[{1}VO];
    }

    root_type {1}Table;";

    private string jsonModle = @"{
        data:[
            {0}
        ]
    }";


    private int tabelTotalCount = 0;

    public void GO() {


        PrepareTables();

        GenerateFbsAndJsonList();

        GenerateBin();


    }

    public void PrepareTables() {
        if (string.IsNullOrEmpty(TablePath)) {
            Print("TablePath is null!!!");
            return;
        }
        string path = TablePath + tableConfig;
        Debug.Log("TableConfig:" + path);

        GetTableConfig(path);
        perpare2TableData();
    }

    private void GetTableConfig(string path) {
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
        string line1 = sr.ReadLine();
        string line2 = sr.ReadLine();
        string line3 = sr.ReadLine();
        Debug.Log("<color=#ff5642>line1 = " + line1 + "</color>");
        Debug.Log("<color=#ff5642>line2 = " + line2 + "</color>");
        Debug.Log("<color=#ff5642>line3 = " + line3 + "</color>");

        //string [] nameArr = line3.Split('\t');
        //string [] typeArr = line1.Split('\t');

        ////tableConfigLineData.Add(line3);
        //for (int i = 0; i < nameArr.Length; i++) {
        //    string name = nameArr[i];
        //    if (name.StartsWith("#")) {
        //        continue;
        //    }
        //    columnName.Add(name);
        //    typeName.Add(typeArr[i]);
        //}
        while (!sr.EndOfStream) {
            string line = sr.ReadLine();
            if (!line.StartsWith("#")) {
                tableConfigLineData.Add(line);
            }
        }
        tabelTotalCount = tableConfigLineData.Count;
        sr.Close();
        fs.Close();
    }

    private void perpare2TableData() {
        if (tableConfigLineData == null || tableConfigLineData.Count <= 0) {
            Debug.LogError("Error! TableToolManager:perpare2TableData--->TableConfig Read Faile!");
            return;
        }
        if (Directory.Exists(Ready2ChangeTablesPath))
        {
            Directory.Delete(Ready2ChangeTablesPath, true);
        }
        Directory.CreateDirectory(Ready2ChangeTablesPath);

        //TableItemVO tabConfig = new TableItemVO(0, "TableConfig", "","/TableConfig", "None", false, true, false, true);
        //TableList.Add(0, tabConfig);
        for (int i = 0; i<tableConfigLineData.Count; i++) {
            string line = tableConfigLineData[i];
            string[] linedata = line.Split(new string[1] { "\t" }, System.StringSplitOptions.None);
            TableItemVO item = new TableItemVO(int.Parse(linedata[0]), linedata[2], linedata[3],
             linedata[3], linedata[4], bool.Parse(linedata[5]), bool.Parse(linedata[6]),
            bool.Parse(linedata[7]), bool.Parse(linedata[8]));
            var path = TablePath + item.RelativePath + ".txt";
            var finalPath = Ready2ChangeTablesPath + item.RelativePath + ".txt";
            var outpath = Path.GetDirectoryName(finalPath);
            Directory.CreateDirectory(outpath);
            File.Copy(path, finalPath, true);
            item.AbsolutePath = finalPath;
            TableList.Add(int.Parse(linedata[0]), item);
            Print("prepare " + i + "/" + tabelTotalCount + "  name = " + item.Name);
        }

    }


    //private TableItemVO makeTableItem(string[] linedata) {
    //    if (linedata == null || linedata.Length <= 0)
    //    {
    //        Debug.LogError("Error! TableToolManager:makeTableItem--->MakeTableItem Faile!");
    //        return null;
    //    }
        //TableItemVO item = new TableItemVO(int.Parse(linedata[0]), linedata[2], linedata[3],
        //        linedata[3], linedata[4], bool.Parse(linedata[5]), bool.Parse(linedata[6]),
        //    bool.Parse(linedata[7]), bool.Parse(linedata[8]));

    //    return item;
    //}

    private void GenerateFbsAndJsonList()
    {
        if (Directory.Exists(FbsPath))
        {
            Directory.Delete(FbsPath, true);
        }
        Directory.CreateDirectory(FbsPath);
        if (Directory.Exists(JsonPath))
        {
            Directory.Delete(JsonPath, true);
        }
        Directory.CreateDirectory(JsonPath);
        if (TableList == null || TableList.Count <= 0 ) {
            Debug.LogError("ERROR! TableToolManager:GenerateFbsList--->TableList is null!");
            return;
        }
        foreach(var item in TableList.Values) {
            Debug.Log(" TableToolManager:GenerateFbsList--->table:" + item.Name);
            ReadTableData(item);

            GenerateJson(item);

            GenerateFbs(item);
        }

    }


    private bool ReadTableData(TableItemVO item) {
        if (item == null) {
            Debug.LogError("ERROR! TableToolManager:ReadTableData--->TableItemVO is null");
            return false;
        }
        string path = item.AbsolutePath;
        string content = FileHelper.GetTxtFileContent(path);
        if (content == null)
        {
            if (EditorUtility.DisplayDialog("Error!!!", "Load TableData Error!name = " + item.Name, "OK"))
            {
                return false;
            }
        }
        DeleteNoMeanLines(content);
        Debug.Log("TableToolManager:ReadTableData---> Load table " + item.Name + " Finish!");
        return true;
    }

    private void GenerateJson(TableItemVO item) {
        if (columnNames == null || columnNames.Count <= 0 || typeNames == null || typeNames.Count <= 0 || dataValues == null || dataValues.Count <= 0)
        {
            Debug.LogError("ERROR! TableToolManager:GenerateJson--->typeName or columnNames or dataValues is null!");
            return;
        }
        StringBuilder data = new StringBuilder();
        for (int i =0;i<dataValues.Count; i++) {
            string line = dataValues[i];
            data.Append("{");
            string[] linedata = line.Split('\t');
            if (linedata.Length == columnNames.Count)
            {
                for (int j = 0; j < linedata.Length; j++)
                {
                    string value = GetJsonTypeValue(typeNames[j], linedata[j]);
                    data.AppendFormat("\"{0}\":{1},", columnNames[j], value);
                }
                data.AppendLine("},");
            }
            else {
                Debug.LogError("ERROR! TableToolManager:GenerateJson--->linedata.Length = "+ linedata.Length + ",columnNames.Count = "+ columnNames.Count);
            }
            
        }

        //string json = string.Format(jsonModle, data.ToString());
        string json = jsonModle.Replace("{0}", data.ToString());
        //Debug.Log("<color=#54b123>" + json + "</color>");
        string fullPath = JsonPath + item.RelativePath + ".json";
        FileHelper.CreateFile(fullPath, json);
    }


    private void GenerateFbs(TableItemVO item) {
        if (columnNames == null || columnNames.Count <= 0 || typeNames == null || typeNames.Count <= 0) {
            Debug.LogError("ERROR! TableToolManager:GenerateFbs--->typeName or columnNames is null!");
            return;
        }
        StringBuilder data = new StringBuilder();
        for (int i =0;i< columnNames.Count; i++ ) {
            string line = string.Format("{0}:{1};", columnNames[i], typeNames[i]);
            data.AppendLine(line);
        }

        //var fbs = string.Format(fbsModle, namespaceStr, tablename, data.ToString());
        string fbs = fbsModle.Replace("{0}", item.NameSpace).Replace("{1}", item.Name).Replace("{2}", data.ToString());
        //Debug.Log("<color=#54b123>"+fbs+"</color>");

        string fullPath = FbsPath + item.RelativePath + ".fbs";
        FileHelper.CreateFile(fullPath, fbs);
    }


    private void GenerateBin() {

    }




    private void DeleteNoMeanLines(string data) {
        if (String.IsNullOrEmpty(data)) {
            return;
        }
        typeNames.Clear();
        columnNames.Clear();
        dataValues.Clear();
        string[] lines = data.Split('\n');
        for (int i = 0; i<lines.Length;i++) {
            lines[i] = lines[i].Trim('\n').Trim('\r');
        }
        var types = lines[0].Split('\t');
        var names = lines[2].Split('\t');
        List<int> ready2DelCol = new List<int>();
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].ToString().StartsWith("#"))
            {
                ready2DelCol.Add(i);
            }
            else {
                typeNames.Add(types[i]);
                columnNames.Add(names[i]);
            }
        }
        //删除多余行
        List<string> tmplines = new List<string>();
        for (int i = 3;i<lines.Length;i++) {
            string line = lines[i];
            if (string.IsNullOrEmpty(line)) {
                continue;
            }
            if (line.StartsWith("#")) {
                continue;
            }
            tmplines.Add(line);
        }
        //删除多余列
        if (ready2DelCol.Count > 0)
        {
            for (int i = 0; i<tmplines.Count; i++) {
                string line = tmplines[i];
                string[] linedata = line.Split('\t');
            
                for (int j = 0; j< ready2DelCol.Count; j++) {
                    linedata[ready2DelCol[j]] = "";
                }
                StringBuilder lineSb = new StringBuilder();
                for (int j = 0; j< linedata.Length;j++) {
                    if (string.IsNullOrEmpty(linedata[j])) {
                        continue;
                    }
                    lineSb.AppendFormat("{0}\t", linedata[j]);
                }
                string result = lineSb.ToString().TrimEnd('\t');
                dataValues.Add(result);
                lineSb = null;
            }

        }
        tmplines.Clear();
        tmplines = null;
    }

    public string GetJsonTypeValue(string type, string val) {
        if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(val)) {
            return null;
        }
        string res = "";
        type = type.ToLower();
        switch (type) {
            case "int":
                res = val;
                break;
            case "byte":
                res = val;
                break;
            case "shot":
                res = val;
                break;
            case "float":
                res = val;
                break;
            case "double":
                res = val;
                break;
            case "long":
                res = val;
                break;
            case "string":
                res = "\"" +val +"\"";
                break;
            case "bool":
                res = val.ToLower();
                break;
        }
        return res;
    }



    public void Print(string msg) {
        PrintMsg += msg + "\n";
        string[] arr = PrintMsg.Split(new string[1] { "\n" }, System.StringSplitOptions.None);
        if (arr.Length > 50)
        {
            PrintMsg = "";
        }
    }

    public void Destroy() {
        PrintMsg = "";
        TablePath = "";
        TableList.Clear();
        TableFileList.Clear();
        tableConfigLineData.Clear();
        columnNames.Clear();
        typeNames.Clear();
        dataValues.Clear();

        //TableList = null;
        //TableFileList = null;
        //tableConfigLineData = null;
        //columnNames = null;
        //typeNames = null;
        //dataValues = null;
    }
}
