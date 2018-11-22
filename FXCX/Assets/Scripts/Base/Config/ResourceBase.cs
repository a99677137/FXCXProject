using Game.Tools.CommonTools;
using UnityEngine;
using System;
using FlatBuffers;

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

        public string LoadTableConfig(string path) {
            string fullPath = "";
            string result = "";
#if UNITY_EDITOR
            fullPath = GetEditorTablePath() + path;
            fullPath = GetEditorCFGPath() + path;
            GameLog.Debug("ResourceBase:LoadConfigFile----fullpath = {0}", fullPath);
            byte[] data = System.IO.File.ReadAllBytes(fullPath);
            if (data == null)
            {
                GameLog.Error("ERROR!!!----ResourceBase:LoadConfigFile----path = {0}", path);
                return "";
            }
            byte[] byteArr = CusEncoding.EncodingUtil.FileByteToLocal(data);
            ByteBuffer byteBuffer = new ByteBuffer(data);
            //rootType = RootType.GetRootAsRootType(byteBuffer);
#elif UNITY_ANDROID

#elif UNITY_IPHONE

#endif

            return "";

        }


        public string LoadConfigFile(string path)
        {
            string fullPath = "";
            string result = "";
#if UNITY_EDITOR
            fullPath = GetEditorCFGPath() + path;
            GameLog.Debug("ResourceBase:LoadConfigFile----fullpath = {0}", fullPath);
            byte[] data = System.IO.File.ReadAllBytes(fullPath);
            if (data == null) {
                GameLog.Error("ERROR!!!----ResourceBase:LoadConfigFile----path = {0}", path);
                return "";
            }
            byte[] byteArr = CusEncoding.EncodingUtil.FileByteToLocal(data);
            result = new String(System.Text.Encoding.UTF8.GetChars(byteArr));
#elif UNITY_ANDROID

#elif UNITY_IPHONE

#endif
            return result;
        }
    }
}
