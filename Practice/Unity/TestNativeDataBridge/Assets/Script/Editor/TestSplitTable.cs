using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEditor;
using System.Threading;
using System;
using System.Runtime.Serialization.Formatters.Binary;


public class TestSplitTable : EditorWindow
{

    private Thread _generateBinThread = null;

    private static string baseFilePath = Application.dataPath+"/../Table";
    private static string outputFilePath = Application.dataPath + "/../TableOutPut";

    private static string filePath = "/TableConfig.txt";
    private static string FbsGeneratePath = "/Generate/Fbs";
    private static string JsonGeneratePath = "/Generate/Json";
    private static string BinGeneratePath = "/Generate/bin/Table";

    private static bool _isAllFinish = false;

    private int Space = 200;

    //private string tableName = "StrDictionary";

    private List<TableConfig> CSTableList = null;

    private static string FinallyPath = "/Table";
    private static string FinallyCSPath = "/Table";

    private static string FinallyConfig = "/TableConfig.bin";

    private System.Diagnostics.Process proc = null;

    private List<TableConfig> tableData = null;

#if UNITY_STANDALONE_OSX
    private string ProcessName = "/GenerateAllTable.command";
#elif UNITY_STANDALONE_WIN
    private string ProcessName = "/GenerateAllTable.bat";
#else
    private string ProcessName = "/GenerateAllTable.bat";
#endif

    static public string ValueFormat_Fbs = @" namespace Games.Test.Table.Static;

 table {0}VO<%
 {1}%>

 table {0}<%
    data:[{0}VO];
 %>
 
root_type {0};";

    static public string ValueFormat_FbsJson = @" <%
 	data:
 	[
{0}
    ]
 %> 
";
    [MenuItem("Tools/TableTool")]
    private static void Init()
    {
        TestSplitTable panel = EditorWindow.GetWindow<TestSplitTable>("TableTool", true, typeof(EditorWindow));
        panel.minSize = new Vector2(600, 300);
        panel.wantsMouseMove = true;
        panel.ShowPopup();
        InitTableData();
    }

    // Use this for initialization
    static void InitTableData () {
        Debug.Log("path = " + Application.streamingAssetsPath);
        _isAllFinish = false;
        filePath = baseFilePath + filePath;
        FbsGeneratePath = outputFilePath + FbsGeneratePath;
        if (Directory.Exists(FbsGeneratePath)) {
            Directory.Delete(FbsGeneratePath, true);
        }
        Directory.CreateDirectory(FbsGeneratePath);
        JsonGeneratePath = outputFilePath + JsonGeneratePath;
        if (Directory.Exists(JsonGeneratePath))
        {
            Directory.Delete(JsonGeneratePath, true);
        }
        Directory.CreateDirectory(JsonGeneratePath);
        BinGeneratePath = outputFilePath + BinGeneratePath;
        if (Directory.Exists(BinGeneratePath))
        {
            Directory.Delete(BinGeneratePath, true);
        }
        Directory.CreateDirectory(BinGeneratePath);
        FinallyPath = Application.streamingAssetsPath + FinallyPath;
        if (Directory.Exists(FinallyPath))
        {
            Directory.Delete(FinallyPath, true);
        }
        Directory.CreateDirectory(FinallyPath);
        FinallyCSPath = Application.streamingAssetsPath + "/../Script" + FinallyCSPath;
        if (Directory.Exists(FinallyCSPath))
        {
            Directory.Delete(FinallyCSPath, true);
        }
        Directory.CreateDirectory(FinallyCSPath);

        FinallyConfig = Application.streamingAssetsPath + FinallyConfig;
        if (File.Exists(FinallyConfig)) {
            File.Delete(FinallyConfig);
        }
    }
	

    void OnGUI()
    {
        if (GUI.Button(new Rect(5, 5, 100, 80), "Start"))
        {
            ReadTable(filePath);
        }
    }

    void Update() {
        if (_isAllFinish) {
            //CombineSplitTab();
            GenerateFinalTableConfig(FinallyConfig);
            CopyBin();
            _isAllFinish = false;
            UnityEditor.EditorUtility.DisplayDialog("Success", "表格转换完成", "确定");
        }
    }

    Dictionary<string, KeyValuePair<Int32, byte[]>> configBinData = new Dictionary<string, KeyValuePair<Int32, byte[]>>();
    private void GenerateFinalTableConfig(string path)
    {
        BinaryFormatter binFormatter = new BinaryFormatter();
        MemoryStream memoryStream;
        for (int i = 0; i< CSTableList.Count;i++) {
            var item = CSTableList[i];
            memoryStream = new MemoryStream();
            binFormatter.Serialize(memoryStream, item);
            byte[] result = memoryStream.GetBuffer();

            if (configBinData.ContainsKey(item.TableName))
            {
                Debug.LogError("ERROR!!!----GenerateFinalTableConfig:tableconfig tableName 重复...tableName = " + item.TableName);
                continue;
            }
            configBinData[item.TableName] = new KeyValuePair<Int32, byte[]>(result.Length, result);
            memoryStream.Close();
            memoryStream.Dispose();
        }

        FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        BinaryWriter binWriter = new BinaryWriter(fileStream);
        binWriter.BaseStream.Seek(0, SeekOrigin.Begin);
        foreach (var key in configBinData.Keys) {
            KeyValuePair<Int32, byte[]> itemData;
            if (configBinData.TryGetValue(key, out itemData))
            {
                Int64 startIdx = binWriter.BaseStream.Position;
                binWriter.Write(startIdx);
                byte[] nameByte = System.Text.Encoding.Default.GetBytes(key);
                Int32 nameLenght = nameByte.Length;
                binWriter.Write(nameLenght);
                binWriter.Write(nameByte);
                Int32 lenght = itemData.Key;
                binWriter.Write(lenght);
                binWriter.Write(itemData.Value);
                Debug.Log("----GenerateFinalTableConfig:name = " + key +",start Idx = " + startIdx + ",finish Idx=" + binWriter.BaseStream.Position);
            }
            else {
                Debug.LogError("ERROR!!!----GenerateFinalTableConfig:tableConfig fail...key = " + key);
            }
        }
        binWriter.Close();
        fileStream.Close();
        fileStream.Dispose();


        //FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        //BinaryFormatter binFormatter = new BinaryFormatter();
        //binFormatter.Serialize(fileStream, CSTableList);
        //fileStream.Close();
        //fileStream.Dispose();
    }


    /**
    private void CombineSplitTab(List<string> tableList) {

        int fileID = 0;
        int fileSize = 0;
        int fileTotalSize = 0;
        for (int idx = 0; idx < tableList.Count; idx++)
        {
            string fileEntry = tableList[idx];

            fileEntry = BinGeneratePath + "/" + fileEntry + ".bin";

            FileInfo fileInfo = new FileInfo(fileEntry);

            // Record file ID
            string fileRealName = fileInfo.Name;
            int startPos = fileRealName.LastIndexOf("[");
            int endPos = fileRealName.LastIndexOf("]");
            fileID = int.Parse(fileRealName.Substring(startPos + 1, endPos - (startPos + 1)));

            // Record file size
            fileSize = (int)fileInfo.Length;

            // Record file total size
            fileTotalSize += (int)fileInfo.Length;

            // Add file record to map
            m_SynthesizeMap.AddFileToMap(fileID, fileSize, fileTotalSize);
        }
    }
*/

    private void CopyBin() {
        if (Directory.Exists(FinallyPath))
        {
            Directory.Delete(FinallyPath, true);
        }
        Directory.CreateDirectory(FinallyPath);
        foreach (var item in CSTableList)
        {
            string srcPath = BinGeneratePath +"/"+ item.TableName + ".bin";
            string destPath = FinallyPath + "/" + item.TableName + ".bin";
            var outpath = Path.GetDirectoryName(destPath);
            if (!Directory.Exists(outpath))
            {
                Directory.CreateDirectory(outpath);
            }
            File.Copy(srcPath, destPath, true);
        }
        string csSrcPath = outputFilePath + "/Generate/cs";
        string csDestPath = FinallyCSPath;
        FileHelper.directoryCopy(csSrcPath, csDestPath,true);

    }

    private void ReadTable(string path) {
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        StreamReader reader = new StreamReader(fs, Encoding.Default);
        string tableName = Path.GetFileNameWithoutExtension(path);
        Debug.Log("----ReadTable:tableName = " + tableName);
        string line_1 = reader.ReadLine();
        string line_2 = reader.ReadLine();
        string line_3 = reader.ReadLine();
        CSTableList = new List<TableConfig>();
        //List<string> lineData = new List<string>();
        tableData = new List<TableConfig>();
        while (!reader.EndOfStream)
        {
            string lineContent = reader.ReadLine();
            if (lineContent != null && lineContent.Trim() != "")
            {
                TableConfig item = new TableConfig();
                string[] arr = lineContent.Split('\t');
                item.Id = int.Parse(arr[0]);
                item.TableName = arr[2];
                item.NameSpace = arr[3];
                item.Path = arr[4];
                item.IsSpit = bool.Parse(arr[5]);
                Debug.Log("-----ReadTable:item =" + item.ToString());
                tableData.Add(item);
            }
        }

        fs.Close();
        reader.Close();

        GenerateAllTable(tableData);
        
    }


    private void GenerateAllTable(List<TableConfig> tableData) {

        for (int i = 0; i < tableData.Count; i++) {
            var table = tableData[i];
            GenerateOneTable(table);
        }
        _generateBinThread = new Thread(new ParameterizedThreadStart(GenerateBin));
        _generateBinThread.Start();
    }

    private void GenerateOneTable(TableConfig table) {
        string tableName = table.TableName;
        string path = baseFilePath + table.Path;
        bool isSpit = table.IsSpit;
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        StreamReader reader = new StreamReader(fs, Encoding.Default);

        string line_1 = reader.ReadLine();
        string line_2 = reader.ReadLine();
        string line_3 = reader.ReadLine();

        List<string> typeList = new List<string>();
        List<string> nameList = new List<string>();
        List<int> tmpSkipId = new List<int>();
        string[] typeTmp = line_1.Split('\t');
        string[] nameTmp = line_3.Split('\t');

        for (int i = 0; i < typeTmp.Length; i++)
        {
            if (nameTmp[i].Contains("$"))
            {
                tmpSkipId.Add(i);
                continue;
            }
            if (nameTmp[i].Contains("#"))
            {
                nameTmp[i] = nameTmp[i].Substring(1);
            }
            typeList.Add(typeTmp[i]);
            nameList.Add(nameTmp[i]);
        }
        List<string> lineData = new List<string>();
        while (!reader.EndOfStream)
        {
            string lineContent = reader.ReadLine();
            if (lineContent != null && lineContent.Trim() != "" && !lineContent.Contains("#"))
            {
                lineData.Add(lineContent);
            }
        }
        fs.Close();
        reader.Close();

        if (isSpit)
        {
            Dictionary<string, List<string>> splitTable = SplitTable(table, lineData);
            GenerateFbsJson4Spit(splitTable, typeList, nameList, tmpSkipId);

        }
        else {
            CSTableList.Add(table);
            GenerateFbsJson(tableName, lineData, typeList, nameList, tmpSkipId);
        }
        
    }


    private void GenerateBin(object obj) {
        try
        {
            string filepath = outputFilePath + ProcessName;
            if (!File.Exists(filepath))
            {
                UnityEngine.Debug.LogError("ERROR!!!TableToolManager:GenerateBin---.bat is not exist!path = " + filepath);
                return;
            }
            //ProcessStartInfo info = new ProcessStartInfo();
            //info.WorkingDirectory = FBOutputPath ;
            //info.FileName = ProcessName;
            proc = System.Diagnostics.Process.Start(filepath);
            proc.WaitForExit();
            _isAllFinish = true;
        }
        catch (Exception e)
        {
            _isAllFinish = false;
            Debug.LogError("ERROR!!!TestSplitTable:GenerateBin---e:" + e.ToString());
        }
        finally
        {
            proc.Close();
        }
        _generateBinThread.Abort();
        _generateBinThread = null;
    }


    private void GenerateFbsJson(string tableName,List<string> lineData, List<string> typeList, List<string> nameList, List<int> skipList = null) {
        fbsGenerate(tableName, typeList, nameList);
        jsonGenerate(tableName, lineData, nameList, typeList, skipList);
    }

    private void GenerateFbsJson4Spit(Dictionary<string, List<string>> sp, List<string> typeList, List<string> nameList,List<int> skipList = null) {
        foreach (string key in sp.Keys) {
            var lineData = new List<string>();
            if (sp.TryGetValue(key, out lineData))
            {
                fbsGenerate(key, typeList, nameList);
                jsonGenerate(key, lineData, nameList, typeList,skipList);
            }
            else {
                Debug.LogError("ERROR!!!!-----GenerateFbsJson----lineData is null! key = " + key);
            }
        }

    }

    private void jsonGenerate(string tableClass, List<string> lineData, List<string> nameList, List<string> typeList,List<int> skipList = null) {
        StringBuilder sb_Json = new StringBuilder();
        foreach (string line in lineData)
        {
            string[] values = line.Split('\t');
            StringBuilder sb_JsonKV = new StringBuilder();
            int tmp = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (skipList.Contains(i)) {
                    tmp = tmp + 1;
                    continue;
                }
                values[i] = values[i].Trim();
                if (string.IsNullOrEmpty(values[i]))
                    values[i] = TypeDefValue(typeList[i - tmp].ToLower());
                TransferValue(typeList[i - tmp].ToLower(), ref values[i]);
                sb_JsonKV.AppendFormat("\"{0}\":{1}", nameList[i- tmp], values[i]);
                if (i < values.Length - 1)
                    sb_JsonKV.Append(",");
                sb_JsonKV.Append("\n\r");
            }
            sb_Json.AppendFormat("<%{0}%>,\n\r", sb_JsonKV.ToString());
        }

        string json = string.Format(ValueFormat_FbsJson, sb_Json.ToString()).Replace("<%", "{").Replace("%>", "}");
        string jsonName = "/" + tableClass + ".json";
        FileHelper.CreateFile(JsonGeneratePath+ jsonName, json);

    }

    private void fbsGenerate(string tableClass, List<string> typeList, List<string> nameList) {
        StringBuilder sb_Fbs = new StringBuilder();
        for (int i = 0; i < typeList.Count; i++)
            sb_Fbs.AppendFormat("{0}:{1};\n\r", nameList[i], typeList[i].ToLower());

        string[] arr = tableClass.Split('_');
        string fbs = string.Format(ValueFormat_Fbs, arr[0], sb_Fbs.ToString()).Replace("<%", "{").Replace("%>", "}");
        string fbsName = "/"+tableClass + ".fbs";
        FileHelper.CreateFile(FbsGeneratePath+fbsName, fbs);
    }

    private Dictionary<string, List<string>> SplitTable(TableConfig table , List<string> lineData) {
        Dictionary<string, List<string>> splitTmp = new Dictionary<string, List<string>>();
        for (int i=0;i<lineData.Count;i++) {
            string line = lineData[i];
            string [] arr = line.Split('\t');
            int id = -1;
            bool isInt = int.TryParse(arr[0], out id);
            if (!isInt) {
                continue;
            }
            int suffixId = id / Space;
            string tableSuffix = table.TableName + "_[" + suffixId + "]";
            if (splitTmp.ContainsKey(tableSuffix))
            {
                List<string> splitTable = null;
                if (splitTmp.TryGetValue(tableSuffix, out splitTable)) {
                    splitTable.Add(line);
                }

            }
            else {
                var lineList = new List<string>();
                lineList.Add(line);
                splitTmp.Add(tableSuffix, lineList);
                var item = new TableConfig();
                item.TableName = tableSuffix;
                item.Path = table.Path;
                item.NameSpace = table.NameSpace;
                item.Id = table.Id;
                item.IsSpit = table.IsSpit;

                CSTableList.Add(item);
            }
        }
        Debug.Log("--------SplitTable:SplitDictionary.Count = " + splitTmp.Count);
        return splitTmp;
    }



    #region 表中数据默认可以有的值和类型
    public static bool s_bDefData = false;
    public static char s_chDefData = char.MaxValue;
    public static byte s_btDefData = byte.MaxValue;
    public static Int16 s_n16DefData = -1;
    public static UInt16 s_u16DefData = UInt16.MaxValue;
    public static Int32 s_n32DefData = -1;
    public static UInt32 s_u32DefData = UInt32.MaxValue;
    public static Int64 s_n64DefData = -1;
    public static UInt64 s_u64DefData = UInt64.MaxValue;
    public static float s_fDefData = -1;
    public static double s_dwDefdata = -1;
    public static string s_strDefData = "";
    #endregion


    static protected string TypeDefValue(string strType)
    {
        if (strType.CompareTo("bool") == 0)
        {
            return s_bDefData.ToString();
        }
        else if (strType.CompareTo("byte") == 0)
        {
            return s_btDefData.ToString();
        }
        else if (strType.CompareTo("short") == 0)
        {
            return s_n16DefData.ToString();
        }
        else if (strType.CompareTo("ushort") == 0)
        {
            return s_u16DefData.ToString();
        }
        else if (strType.CompareTo("int") == 0)
        {
            return s_n32DefData.ToString();
        }
        else if (strType.CompareTo("uint") == 0)
        {
            return s_u32DefData.ToString();
        }
        else if (strType.CompareTo("long") == 0)
        {
            return s_n64DefData.ToString();
        }
        else if (strType.CompareTo("ulong") == 0)
        {
            return s_u64DefData.ToString();
        }
        else if (strType.CompareTo("float") == 0)
        {
            return s_fDefData.ToString();
        }
        else if (strType.CompareTo("double") == 0)
        {
            return s_dwDefdata.ToString();
        }
        else if (strType.CompareTo("string") == 0)
        {
            return s_strDefData;
        }
        else
        {
            return null;
        }
    }

    /** 转换 值**/
    static protected void TransferValue(string strType, ref string strValue)
    {
        if (strType.CompareTo("string") == 0)
        {
            if (strValue.Contains("\"")) {
                strValue = string.Format("{0}", strValue);
            }
            else
            {
                strValue = string.Format("\"{0}\"", strValue);
            }
        }
       
    }

}
