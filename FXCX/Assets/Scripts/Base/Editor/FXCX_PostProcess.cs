using System;
using UnityEditor;
using UnityEditor.Callbacks;
using Game.Tools.CommonTools;
using System.IO;
using UnityEngine;

public class FXCX_PostProcess
{

#if UNITY_EDITOR
    [PostProcessBuild(100)]
    public static void OnFxcxPostProcessBuild(BuildTarget target, string pathToBuiltProject) {
        if (BuildTarget.Android == target) {
            GameLog.Debug("<color=#aa0099>--------------FXCX_PostProcess:OnFxcxPostProcessBuild----Target = Android---------</color>");
            GameLog.Debug("<color=#aa0099>--------------FXCX_PostProcess:OnFxcxPostProcessBuild----pathToBuiltProject = " + pathToBuiltProject + "---------</color>");
            //PostProcessAndroidBuild(pathToBuiltProject);
        } else if (BuildTarget.iOS == target) {
            GameLog.Debug("<color=#aa0099>--------------FXCX_PostProcess:OnFxcxPostProcessBuild----Target = iOS---------</color>");
            GameLog.Debug("<color=#aa0099>--------------FXCX_PostProcess:OnFxcxPostProcessBuild----pathToBuiltProject = "+ pathToBuiltProject + "---------</color>");
        }
    }
#endif


    public static void PostProcessAndroidBuild(string pathToBuiltProject)
    {
        ScriptingImplementation backend = PlayerSettings.GetScriptingBackend(BuildTargetGroup.Android);

        if (backend == ScriptingImplementation.IL2CPP)
        {
            CopyAndroidIL2CPPSymbols(pathToBuiltProject, PlayerSettings.Android.targetDevice);
        }
    }

    public static void CopyAndroidIL2CPPSymbols(string pathToBuiltProject, AndroidTargetDevice targetDevice)
    {
        string buildName = Path.GetFileNameWithoutExtension(pathToBuiltProject);
        FileInfo fileInfo = new FileInfo(pathToBuiltProject);
        string symbolsDir = fileInfo.Directory.Name;
        symbolsDir = symbolsDir + "/" + buildName + "_IL2CPPSymbols";

        CreateDir(symbolsDir);

        switch (PlayerSettings.Android.targetDevice)
        {
            case AndroidTargetDevice.FAT:
                {
                    CopyARMSymbols(symbolsDir);
                    CopyX86Symbols(symbolsDir);
                    break;
                }
            case AndroidTargetDevice.ARMv7:
                {
                    CopyARMSymbols(symbolsDir);
                    break;
                }
            case AndroidTargetDevice.x86:
                {
                    CopyX86Symbols(symbolsDir);
                    break;
                }
            default:
                break;
        }
    }


    const string libpath = "/../Temp/StagingArea/libs/";
    const string libFilename = "libil2cpp.so.debug";
    private static void CopyARMSymbols(string symbolsDir)
    {
        string sourcefileARM = Application.dataPath + libpath + "armeabi-v7a/" + libFilename;
        CreateDir(symbolsDir + "/armeabi-v7a/");
        File.Copy(sourcefileARM, symbolsDir + "/armeabi-v7a/libil2cpp.so.debug");
    }

    private static void CopyX86Symbols(string symbolsDir)
    {
        string sourcefileX86 = Application.dataPath + libpath + "x86/libil2cpp.so.debug";
        File.Copy(sourcefileX86, symbolsDir + "/x86/libil2cpp.so.debug");
    }

    public static void CreateDir(string path)
    {
        if (Directory.Exists(path))
            return;

        Directory.CreateDirectory(path);
    }

}
