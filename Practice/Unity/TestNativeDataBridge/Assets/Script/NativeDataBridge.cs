using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class NativeDataBridge {

#if UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_EDITOR_OSX
    public const string LibName = "NativeDataBridge";
#elif UNITY_IPHONE
		public const string LibName = "__Internal";
#elif UNITY_ANDROID
		public const string LibName = "NativeDataBridge";
#endif

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int UnityNativeInit();

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void UnityNativeRelease();

    //[System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    //public static extern void BufferLittleEndian(bool IsLittleEndian);

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int CreateFileBuffer([MarshalAs(UnmanagedType.LPArray)] string szFileName,uint len);

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int CreateBufferFromFile([MarshalAs(UnmanagedType.LPArray)] string szFileName, uint offset, uint dataSize,uint len);

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int DestroyByBufferID(int bufferID);

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void BufferGetByte(int bufferID, uint offset, ref byte data);

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void BufferGetDouble(int bufferID, uint offset, ref double data);

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void BufferGetFloat(int bufferID, uint offset, ref float data);

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void BufferGetInt(int bufferID, uint offset, ref int data);

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void BufferGetLong(int bufferID, uint offset, ref long data);

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void BufferGetSbyte(int bufferID, uint offset, ref sbyte data);

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void BufferGetShort(int bufferID, uint offset, ref short data);

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void BufferGetUShort(int bufferID, uint offset, ref ushort data);

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void BufferGetUInt(int bufferID, uint offset, ref uint data);

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void BufferGetULong(int bufferID, uint offset, ref ulong data);

    //byte[] bytes = new byte[1024];(ref bytes[0])
    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void BufferGetData(int bufferID, uint offset, uint dataSize, ref byte data);

    [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr BufferGetDataIntPtr(int bufferID, uint offset, uint dataSize);

#if (!UNITY_EDITOR) && UNITY_ANDROID
             
        [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AndroidInit();

        [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SeekReadAssetFile(string filename, ref int size, ref int offset);

        [System.Runtime.InteropServices.DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TryOpenAssetFile(string filename);

#endif
}