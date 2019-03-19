using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class GameStart : MonoBehaviour {

    private string msg = "";

    string filename = "StrDictionary.bin";

    // Use this for initialization
    void Start () {
        msg += "GameStart!\n";
#if UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_EDITOR_OSX
        NativeDataBridge.UnityNativeInit();
#elif UNITY_ANDROID
        NativeDataBridge.AndroidInit();
#endif
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnGUI() {
        if (GUI.Button(new Rect(5,5,100,80),"LoadData")) {
            msg += "Click LoadBin!\n";
            var res = NativeDataBridge.CreateBufferFromFile(filename,0,0,(uint)filename.Length);
            msg += "CreateBufferFromFile's result=" + res+"\n";
        }

        if (GUI.Button(new Rect(110, 5, 100, 80), "ReadData")) {
            msg += "Click ReadData!\n";
            IntPtr dataPtrs = NativeDataBridge.BufferGetDataIntPtr(1, 5324880, 6);
            string res = Marshal.PtrToStringAnsi(dataPtrs);
            msg += "BufferGetDataIntPtr's result=" + res + "\n";
        }

        if (GUI.Button(new Rect(215, 5, 100, 80), "DestroyData"))
        {
            msg += "Click DestroyData!\n";
            var res= NativeDataBridge.DestroyByBufferID(1);
            msg += "DestroyByBufferID's result=" + res + "\n";
        }

        GUI.TextArea(new Rect(5, Screen.height - 205, Screen.width - 10, 200), msg);
    }

    void OnApplicationQuit() {
        msg += "ApplicationQuit!\n";
        NativeDataBridge.UnityNativeRelease();
    }
}
