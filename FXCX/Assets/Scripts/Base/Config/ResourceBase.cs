using Game.Tools.CommonTools;
using UnityEngine;
using System.IO;
using System;

namespace Game.Lwn.Base
{
    public class ResourceBase : Singleton<ResourceBase>
    {
        private string GetAndroidTablePath() {
            return "";
        }

#if UNITY_EDITOR
        private string GetEditorTablePath() {
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

#endif

        public string LoadConfigFile(string path)
        {
            string fullpath = "";
            string result = "";
#if UNITY_EDITOR
            fullpath = GetEditorCFGPath() + path;
            GameLog.Debug("ResourceBase:LoadConfigFile----fullpath = {0}", fullpath);
            byte[] data = System.IO.File.ReadAllBytes(fullpath);
            if (data == null) {
                GameLog.Error("ERROR!!!----ResourceBase:LoadConfigFile----path = {0}", path);
                return "";
            }
            byte[] byteArr = CusEncoding.EncodingUtil.FileByteToLocal(data);
            result = new String(System.Text.Encoding.UTF8.GetChars(byteArr));
#else



#endif
            return result;
        }
    }
}
