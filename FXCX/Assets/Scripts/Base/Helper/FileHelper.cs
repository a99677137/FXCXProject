using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game.Lwn.Main
{

    public class FileHelper
    {

        public static void Explorer(string path)
        {
            if (!string.IsNullOrEmpty(path))
                System.Diagnostics.Process.Start("Explorer.exe", @"/select," + path.Replace("/", "\\"));
        }

        public static bool HasExistFile(string file)
        {
            return File.Exists(file);
        }

        //public static string GetFile(string folder,string filters)
        //{
        //    string[] files = GetFiles(folder);

        //    foreach (var item in files)
        //    {
        //        string extension = Path.GetExtension(item);
        //        if (filters.IndexOf(extension)==-1)
        //        {
        //            continue;
        //        }
        //    }

        //    return "";
        //}

        /// <summary>
        /// 获取指定目录机器所有子目录下的所有文件
        /// </summary>
        /// <param name="root"></param>
        /// <param name="pattern">*.txt|*.lua等</param>
        /// <returns></returns>
        public static string[] GetAllFiles(string root, string pattern)
        {
            string[] files = null;

            files = Directory.GetFiles(root, pattern, SearchOption.AllDirectories);


            return files;
        }

        public static List<string> GetFiles(string parentFolder)
        {
            if (!Directory.Exists(parentFolder))
                return null;


            DirectoryInfo dicInfo = new DirectoryInfo(parentFolder);
            return GetFiles(dicInfo);
        }



        /// <summary>
        /// （这个方法设计不合理，DirectoryInfo需要封装在该类内部）
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>

        public static List<string> GetFiles(DirectoryInfo d)
        {
            if (d == null)
                return null;

            List<string> files = new List<string>();

            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                files.Add(fi.FullName);
            }

            // Add subdirectory files.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                List<string> tmp = GetFiles(di);
                if (tmp != null)
                    files.AddRange(tmp);
            }

            return files;
        }


        public static void DeletePathAndFiles(string path)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }

        public static void DeleteFile(string file)
        {
            if (File.Exists(file))
                File.Delete(file);
        }

        public static void DeleteFilesByExtension(string path, string extension = "*")
        {
            if (Directory.Exists(path))
            {
                string[] files = null;
                if (extension.Equals("*"))
                {
                    files = Directory.GetFiles(path);
                }
                else
                {
                    files = Directory.GetFiles(path, "*." + extension);
                }
                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
            else
            {
                Debug.LogError("DeleteFilesByExtension failed ------------>" + path + "is not Exists");
            }
        }



        #region 文件处理


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="startLine">从第几行开始读取</param>
        /// <returns></returns>
        public static string GetTxtFileContent(string path, int startLine = 1)
        {
            if (!File.Exists(path))
                return null;

            //FileStream stream = File.OpenRead(path);
            //StreamReader reader = new StreamReader(stream, Encoding.Default);
            //string content = reader.ReadToEnd();
            //reader.Close();

            //stream.Close();
            //进程共享形式访问和读取文件
			string content = null;
			FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
#if UNITY_EDITOR_OSX
			BinaryReader r = new BinaryReader(fs);
			byte[] buf = r.ReadBytes ((int)fs.Length);
			string str = System.Text.UTF8Encoding.UTF8.GetString (buf);
			buf = null;
			StringReader s = new StringReader(str);
			for (int i = 1; i < startLine; i++)
			{
				s.ReadLine();
			}
			content = s.ReadToEnd();
#else
            BinaryReader r = new BinaryReader(fs);
            byte[] buf = r.ReadBytes((int)fs.Length);
            CusEncoding.EncodingType encodingType = CusEncoding.FileEncodingType.GetType(buf);
            fs.Seek(0, SeekOrigin.Begin);
            StreamReader reader; 
            switch (encodingType)
            {
                case CusEncoding.EncodingType.Encoding_GBK:
                    reader = new StreamReader(fs, Encoding.GetEncoding("GBK"));
                    break;
                case CusEncoding.EncodingType.Encoding_UTF8:
                case CusEncoding.EncodingType.Encoding_UTF8_BOM:
                    reader = new StreamReader(fs, Encoding.UTF8);
                    break;
                default:
                    reader = new StreamReader(fs, Encoding.Default);
                    break;

            }
            for (int i = 1; i < startLine; i++)
            {
                reader.ReadLine(); 
            }
            content = reader.ReadToEnd();
            reader.Close();
#endif
            fs.Close();
            return content;
        }


        public static bool CreateTxtFile(string file, string content, Encoding encode)
        {
            try
            {
                CheckParentExitAndCreatPath(file);

                if (File.Exists(file))
                    File.Delete(file);

                FileStream stream = File.Open(file, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter writer = new StreamWriter(stream, encode);
                writer.Write(content);

                writer.Close();
                stream.Close();

                return true;
            }
            catch (Exception exception)
            {
                //Debug.LogError(string.Format("[代码生成] 转码时，创建表失败！！！！！{0}\n{1}", file, exception.Message));
                throw exception;
            }
        }

        //public static bool UpdateTxtFile(string file, string content, Encoding encode)
        //{
        //    try
        //    {
        //        File.
        //        FileStream stream = File.Open(file, FileMode.OpenOrCreate);
        //        StreamWriter writer = new StreamWriter(stream, encode);
        //        writer.Write(content);

        //        writer.Close();
        //        stream.Close();

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        Debug.LogError("[代码生成] 转码时，创建表失败！！！！！");
        //        return false;
        //    }
        //}
        public static bool CreateFile(string file, string content)
        {
            try
            {
                CheckParentExitAndCreatPath(file);
                if (File.Exists(file))
                    File.Delete(file);

                using (FileStream fs = new FileStream(file, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(content);
                        sw.Close();
                        fs.Close();
                    }
                }

                return true;
            }
            catch (Exception exception)
            {
                //Debug.LogError(string.Format("[代码生成] 转码时，创建表失败！！！！！{0}\n{1}", file, exception.Message));
                throw exception;
            }
            
        }

        public static bool CopyFile(string srcfile, string destFile)
        {
            try
            {
                CheckParentExitAndCreatPath(srcfile);
                CheckParentExitAndCreatPath(destFile);

                File.Copy(srcfile, destFile, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region 文件路径处理

        public static bool CreatePath(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void CheckParentExitAndCreatPath(string file)

        {
            try
            {
                string pPath = GetFileParentPath(file);
                if (!Directory.Exists(pPath))
                    Directory.CreateDirectory(pPath);
            }
            catch (Exception exception)
            {

                throw exception;
            }
            
        }


        public static string GetFileFullName(string root, string filePathName)
        {
            root = root.Replace("\\", "/");
            filePathName = filePathName.Replace("\\", "/");

            if (!root.EndsWith("/") && !filePathName.StartsWith("/"))
            {
                root = root + "/";
            }


            return root + filePathName;
        }

        public static string GetFileName(string file)
        {
            return Path.GetFileName(file);
        }

        public static string GetFileParentPath(string file)
        {

            file = file.Replace("\\", "/");
            int index = file.LastIndexOf("/");

            return file.Substring(0, index);
            //return Path.GetDirectoryName(file);
        }

        #endregion

		public static void directoryCopy(string sourceDirectory, string targetDirectory,bool bRecursive = true)
		{
			if (!Directory.Exists(sourceDirectory))
			{
				Debug.LogError ("directoryCopy failed ------------>" + sourceDirectory + "is not Exists");
				return;
			}
			if(!Directory.Exists(targetDirectory))
			{
				Directory.CreateDirectory(targetDirectory);
			}
			DirectoryInfo sourceInfo = new DirectoryInfo(sourceDirectory);
			sourceDirectory = sourceInfo.FullName;
			DirectoryInfo targetInfo = new DirectoryInfo(targetDirectory);
			targetDirectory = targetInfo.FullName;
			//			Debug.LogError ("directoryCopy sourceDirectory = " + sourceDirectory.ToString ());
			//			Debug.LogError ("directoryCopy targetDirectory = " + targetDirectory.ToString ());
			FileInfo[] fileInfo = sourceInfo.GetFiles();
			foreach (FileInfo fiTemp in fileInfo) 
			{
				File.Copy(sourceDirectory + "/" + fiTemp.Name, targetDirectory + "/" + fiTemp.Name, true);
			}
			if (bRecursive) 
			{
				DirectoryInfo[] diInfo = sourceInfo.GetDirectories();
				foreach (DirectoryInfo diTemp in diInfo) 
				{
					string sourcePath = diTemp.FullName;
					//					Debug.LogError ("directoryCopy sourcePath = " + sourcePath);
					string targetPath = diTemp.FullName.Replace(sourceDirectory,targetDirectory);
					directoryCopy(sourcePath,targetPath,bRecursive);
				}
			}
		}


    }

}
