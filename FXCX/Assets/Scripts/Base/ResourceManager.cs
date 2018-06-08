using UnityEngine;
using System;
using Game.Tools.CommonTools;
using System.IO;
using Game.Lwn.Base;

public class ResourceManager:Singleton<ResourceManager> {


    public void LoadGameConfig()
    {
        //var absulutePath = ResourceBase.Instance.GetCFGPath() + path;
        string res = ResourceBase.Instance.LoadConfigFile("GameCFG.cfg");
        GameCFG.Instance.Parse(res);
    }



    public UnityEngine.Object ResourceLoadUnityObj(string path) {
        return Resources.Load(path);
    }


}
