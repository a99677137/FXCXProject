using UnityEngine;
using System;
using Game.Tools.CommonTools;
using System.IO;
using Game.Lwn.Base;

public class ResourceManager:Singleton<ResourceManager> {


    public string LoadGameConfig(string path)
    {
        //var absulutePath = ResourceBase.Instance.GetCFGPath() + path;

        return null;
    }



    public UnityEngine.Object ResourceLoadUnityObj(string path) {
        return Resources.Load(path);
    }


}
