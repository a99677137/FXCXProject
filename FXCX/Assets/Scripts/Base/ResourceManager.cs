using UnityEngine;
using System;
using Game.Tools.CommonTools;
using System.IO;

public class ResourceManager:Singleton<ResourceManager> {

    public UnityEngine.Object ResourceLoadUnityObj(string path) {
        return Resources.Load(path);
    }

    public string FileLoadObj(string path) {
        return File.ReadAllText(path);
    }
}
