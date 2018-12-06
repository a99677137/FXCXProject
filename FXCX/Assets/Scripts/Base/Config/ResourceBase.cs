using Game.Tools.CommonTools;
using UnityEngine;
using System;
using FlatBuffers;
using Game.Table.Static;
using System.IO;

namespace Game.Lwn.Main
{
    public class ResourceBase : Singleton<ResourceBase>
    {

        public byte[] LoadDataBytesByPath(string path) {
            path = HandleFilePath(path);
            byte[] byteArr = null;
#if UNITY_EDITOR
            GameLog.Debug("ResourceBase:LoadDataBytesByPath----fullpath = {0}", path);
            byteArr = System.IO.File.ReadAllBytes(path);
            if (byteArr == null)
            {
                GameLog.Error("ERROR!!!----ResourceBase:LoadDataBytesByPath----path = {0}", path);
                return byteArr;
            }
#elif UNITY_ANDROID

#elif UNITY_IPHONE

#endif
            return byteArr;

        }


        public string LoadTxtByteByPath(string path)
        {
            string result = "";
#if UNITY_EDITOR
            
            GameLog.Debug("ResourceBase:LoadConfigFile----fullpath = {0}", path);
            byte[] data = System.IO.File.ReadAllBytes(path);
            if (data == null) {
                GameLog.Error("ERROR!!!----ResourceBase:LoadConfigFile----path = {0}", path);
                return result;
            }
            byte[] byteArr = CusEncoding.EncodingUtil.FileByteToLocal(data);
            result = new String(System.Text.Encoding.UTF8.GetChars(byteArr));
#elif UNITY_ANDROID

#elif UNITY_IPHONE

#endif
            return result;
        }


        public string HandleFilePath(string path) {
            path = path.Replace("//", "/");
            return path;
        }

        //public void CopyDirectory(string srcDirectory,string destDirectory) {
        //    if (string.IsNullOrEmpty(srcDirectory) || string.IsNullOrEmpty(destDirectory)) {
        //        GameLog.Error("ERROR!!!----ResourceBase:CopyDirectory----Directory is null!!!");
        //        return;
        //    }
        //    if (!Directory.Exists(srcDirectory)) {
        //        GameLog.Error("ERROR!!!----ResourceBase:CopyDirectory----srcDirectory is not Exist!!!");
        //        return;
        //    }
        //    if (!Directory.Exists(destDirectory)) {
        //        Directory.CreateDirectory(destDirectory);
        //    }
            
        //}

    }
}
