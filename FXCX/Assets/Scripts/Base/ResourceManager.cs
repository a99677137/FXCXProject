using UnityEngine;
using System;
using Game.Tools.CommonTools;
using System.IO;
using Game.Lwn.Main;

public class ResourceManager:Singleton<ResourceManager> {

    private string _tablePath = "";
    public void LoadGameConfig()
    {
        string fullPath = GetCFGPath() + "GameCFG.cfg";
        string res = ResourceBase.Instance.LoadTxtByteByPath(fullPath);
        GameCFG.Instance.Parse(res);
    }

    public void LoadGameInitConfig()
    {
        string fullPath = GetCFGPath() + "GameInitCFG.cfg";
        string res = ResourceBase.Instance.LoadTxtByteByPath(fullPath);
        GameInitCFG.Instance.Parse(res);
    }

    public byte[] LoadTableConfig() {
        string fullPath = GetTablePath() + "TableConfig.bin";
        return ResourceBase.Instance.LoadDataBytesByPath(fullPath);
    }

    public byte[] LoadTable(string path) {
        string fullPath = GetTablePath() + path;
        return ResourceBase.Instance.LoadDataBytesByPath(fullPath);
    }

    public UnityEngine.Object ResourceLoadUnityObj(string path) {
        return Resources.Load(path);
    }



    private string GetTablePath()
    {
#if UNITY_EDITOR
        return _tablePath == ""?_tablePath = GetEditorTablePath() : _tablePath;
#elif UNITY_ANDROID
        return _tablePath == ""?_tablePath = GetAndroidTablePath() : _tablePath;
#elif UNITY_IPHONE
        return _tablePath == ""?_tablePath = GetiOSTablePath() : _tablePath;
#endif
    }

    private string GetCFGPath()
    {
#if UNITY_EDITOR
        return GetEditorCFGPath();
#elif UNITY_ANDROID
        return GetAndroidCFGPath();
#elif UNITY_IPHONE
        return GetiOSCFGPath();
#endif
    }

    private string GetLuaPath()
    {
#if UNITY_EDITOR
        return GetEditorLuaPath();
#elif UNITY_ANDROID
        return GetAndroidLuaPath();
#elif UNITY_IPHONE
        return GetiOSLuaPath();
#endif
    }



#if UNITY_EDITOR
    private string GetEditorTablePath()
    {
        return Application.dataPath + "/../GameAssets/Table/";
    }

    private string GetEditorCFGPath()
    {
        return Application.dataPath + "/../GameAssets/Config/";
    }

    private string GetEditorLuaPath()
    {
        return Application.dataPath + "/../GameAssets/Lua/";
    }
#elif UNITY_ANDROID
    private string GetAndroidTablePath()
    {
        return Application.dataPath + "/../GameAssets/Table/";
    }

    private string GetAndroidCFGPath()
    {
        return Application.dataPath + "/../GameAssets/Config/";
    }

    private string GetAndroidLuaPath()
    {
        return Application.dataPath + "/../GameAssets/Lua/";
    }
#elif UNITY_IPHONE
    private string GetiOSTablePath()
    {
        return Application.dataPath + "/../GameAssets/Table/";
    }

    private string GetiOSCFGPath()
    {
        return Application.dataPath + "/../GameAssets/Config/";
    }

    private string GetiOSLuaPath()
    {
        return Application.dataPath + "/../GameAssets/Lua/";
    }
#endif


}
