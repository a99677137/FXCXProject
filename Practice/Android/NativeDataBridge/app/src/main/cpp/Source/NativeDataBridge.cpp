// NativeDataBridge.cpp Defines the exported functions for the DLL application.
//
#include "../Header/Common.h"
#include "../Header/DebugLog.h"
#include "../Header/FileProxy.h"
#include "../Header/ByteBuffer.h"
#include "../Header/ByteBufferManager.h"
#include "../Header/GlobalIDManager.h"

/*
#ifdef _WIN32
//define something for Windows (32-bit and 64-bit, this part is common)
#ifdef _WIN64
//define something for Windows (64-bit only)
#else
//define something for Windows (32-bit only)
#endif
#elif __APPLE__
#include "TargetConditionals.h"
#if TARGET_IPHONE_SIMULATOR
// iOS Simulator
#elif TARGET_OS_IPHONE
// iOS device
#elif TARGET_OS_MAC
// Other kinds of Mac OS
#else
#   error "Unknown Apple platform"
#endif
#elif __ANDROID__
// android
#elif __linux__
// linux
#elif __unix__ // all unices not caught above
// Unix
#elif defined(_POSIX_VERSION)
// POSIX
#else
#   error "Unknown compiler"
#endif
*/

//Windows宏 _WIN64
//Android宏  __ANDROID__
//Mac宏 TARGET_OS_MAC
//IOS宏 TARGET_OS_IPHONE
/*
#if _WIN64

#elif __ANDROID__

#elif TARGET_OS_MAC

#elif TARGET_OS_IPHONE

#endif
*/
//_DLLExport VOID BufferLittleEndian(bool IsLittleEndian)
//{
//	LWN::ByteBuffer::IsLittleEndian = IsLittleEndian;
//}

CHAR * g_sdata = new CHAR[10240];


#if __ANDROID__
#include <jni.h>
#include <string>
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/resource.h>
#include <android/asset_manager.h>
#include <android/asset_manager_jni.h>
#include <fcntl.h>
#include <stdio.h>
#include <string.h>
#include <errno.h>
#include <unistd.h>
#include "../Android/AssetFile.h"
//#include <map>

JNIEnv* jni_env = 0;
_DLLExport jint JNI_OnLoad(JavaVM* vm, void* reserved)
{
	vm->AttachCurrentThread(&jni_env, 0);
	return  JNI_VERSION_1_6;
}

_DLLExport JNIEXPORT jint JNICALL AndroidInit(JNIEnv *, jobject)
{
	if (jni_env == NULL)
		return 0;

	if (jni_env != NULL)
	{
		jclass cls_JavaClass = jni_env->FindClass("com/unity3d/player/UnityPlayer");
		if (cls_JavaClass == NULL)
			return 0;

		jfieldID fid_JavaClass = jni_env->GetStaticFieldID(cls_JavaClass, "currentActivity", "Landroid/app/Activity;");
		if (fid_JavaClass == NULL)
			return 0;

		jobject currentActivity = jni_env->GetStaticObjectField(cls_JavaClass, fid_JavaClass);
		if (currentActivity == NULL)
			return 0;

		jclass cls_activity = jni_env->FindClass("android/app/Activity");
		if (cls_activity == NULL)
			return 0;

		jmethodID mid_activity = jni_env->GetMethodID(cls_activity, "getAssets", "()Landroid/content/res/AssetManager;");
		if (mid_activity == NULL)
			return 0;

		jobject assetManager = jni_env->CallObjectMethod(currentActivity, mid_activity);
		if (assetManager == NULL)
			return 0;

		LWN::AssetFile::s_pAssetMgr = AAssetManager_fromJava(jni_env, assetManager);
		rlimit cur_fd_limit;
		getrlimit(RLIMIT_NOFILE, &cur_fd_limit);
		rlimit new_limit = cur_fd_limit;
		new_limit.rlim_cur = cur_fd_limit.rlim_max;
		new_limit.rlim_max = cur_fd_limit.rlim_max;
		setrlimit(RLIMIT_NOFILE, &new_limit);
		//int result = getrlimit(RLIMIT_NOFILE, &cur_fd_limit);
		if (LWN::AssetFile::s_pAssetMgr == NULL)
			return 0;
		return 1;
	}
	else
	{
		return 0;
	}
}

_DLLExport jint TryOpenAssetFile(const char* filename)
{
	AAsset *pAsset = AAssetManager_open(LWN::AssetFile::s_pAssetMgr, filename, AASSET_MODE_UNKNOWN);
	if (pAsset == NULL)
		return 0;
	AAsset_close(pAsset);
	return 1;
}
_DLLExport unsigned char* SeekReadAssetFile(const char* filename, unsigned int& datasize, unsigned int& offset)
{
	AAsset* pAsset = AAssetManager_open(LWN::AssetFile::s_pAssetMgr, filename, AASSET_MODE_UNKNOWN);
	if (pAsset == NULL)
		return NULL;

	unsigned char* buffer = NULL;
	buffer = new unsigned char[datasize];
	off_t t_offset = AAsset_seek(pAsset, offset, 0);
	int iRet = AAsset_read(pAsset, buffer, datasize);
	if (iRet <= 0)
	{
		delete[] buffer;
		buffer = NULL;
	}
	AAsset_close(pAsset);
	return buffer;
}
#endif

#if _WIN64
FILE * fConsole;
_DLLExport INT Log_Init() {
	
	freopen_s(&fConsole, "nativelog.txt", "w+", stdout);
	alog("-----------------------Native Start-----------------------------");
	return 1;
}

_DLLExport VOID Log_Release() {
	alog("-----------------------Native End-----------------------------");
	fclose(fConsole);
}
#endif

_DLLExport VOID DestroyNative()
{
	LWN::ByteBufferManager::Destroy();
	LWN::GlobalIDManager::ResetGlobalID();
}

_DLLExport INT UnityNativeInit()
{
#if _WIN64
	return Log_Init();
#endif
	return 1;
}

_DLLExport VOID UnityNativeRelease()
{
#if _WIN64
	Log_Release();
#endif
	DestroyNative();
}


_DLLExport INT CreateFileBuffer(STRING szFileName, UINT len)
{
	alog("-----------------------CreateFileBuffer----------szFileName = %s,len=%d", szFileName, len);
	LWN::ByteBuffer& byteBuffer = LWN::ByteBufferManager::CreateByteBuffer(szFileName, -1,-1,len);
	if (byteBuffer.Valid)
	{
		return byteBuffer.BufferID;
	}
	else
	{
		return -1;

	}
	return -1;
}

_DLLExport INT CreateBufferFromFile(STRING szFileName, UINT offset, UINT dataSize, UINT len)
{
	alog("-----------------------CreateBufferFromFile----------szFileName = %s,offset=%d,dataSize=%d", szFileName, offset, dataSize);
	LWN::ByteBuffer& byteBuffer = LWN::ByteBufferManager::CreateByteBuffer(szFileName, dataSize, offset,len);
	if (byteBuffer.Valid)
	{
		return byteBuffer.BufferID;
	}
	else
	{
		return -1;

	}
	return -1;
}

_DLLExport INT DestroyByBufferID(INT bufferID) {
	alog("----------DestroyByBufferID----bufferID = %d", bufferID);
	return LWN::ByteBufferManager::DestroyDataByBufferId(bufferID);
}

_DLLExport VOID BufferGetByte(INT bufferID, UINT offset, BYTE& data)
{
	LWN::ByteBuffer& byteBuffer = LWN::ByteBufferManager::GetByteBuffer(bufferID);
	if (byteBuffer.Valid)
	{
		data = byteBuffer.GetByte(offset);
	}
}

_DLLExport VOID BufferGetDouble(INT bufferID, UINT offset, DOUBLE& data)
{
	LWN::ByteBuffer& byteBuffer = LWN::ByteBufferManager::GetByteBuffer(bufferID);
	if (byteBuffer.Valid)
	{
		data = byteBuffer.GetDouble(offset);
	}
}

_DLLExport VOID BufferGetFloat(INT bufferID, UINT offset, FLOAT& data)
{
	LWN::ByteBuffer& byteBuffer = LWN::ByteBufferManager::GetByteBuffer(bufferID);
	if (byteBuffer.Valid)
	{
		data = byteBuffer.GetFloat(offset);
	}
}

_DLLExport VOID BufferGetInt(INT bufferID, UINT offset, INT& data)
{
	LWN::ByteBuffer& byteBuffer = LWN::ByteBufferManager::GetByteBuffer(bufferID);
	if (byteBuffer.Valid)
	{
		data = byteBuffer.GetInt(offset);
	}
}

_DLLExport VOID BufferGetLong(INT bufferID, UINT offset, INT64& data)
{
	LWN::ByteBuffer& byteBuffer = LWN::ByteBufferManager::GetByteBuffer(bufferID);
	if (byteBuffer.Valid)
	{
		data = byteBuffer.GetLong(offset);
	}
}

_DLLExport VOID BufferGetSbyte(INT bufferID, UINT offset, SBYTE& data)
{
	LWN::ByteBuffer& byteBuffer = LWN::ByteBufferManager::GetByteBuffer(bufferID);
	if (byteBuffer.Valid)
	{
		data = byteBuffer.GetSbyte(offset);
	}
}

_DLLExport VOID BufferGetShort(INT bufferID, UINT offset, SHORT& data)
{
	LWN::ByteBuffer& byteBuffer = LWN::ByteBufferManager::GetByteBuffer(bufferID);
	if (byteBuffer.Valid)
	{
		data = byteBuffer.GetShort(offset);
	}
}

_DLLExport VOID BufferGetUShort(INT bufferID, UINT offset, USHORT& data)
{
	LWN::ByteBuffer& byteBuffer = LWN::ByteBufferManager::GetByteBuffer(bufferID);
	if (byteBuffer.Valid)
	{
		data = byteBuffer.GetUShort(offset);
	}
}

_DLLExport VOID BufferGetUInt(INT bufferID, UINT offset, UINT& data)
{
	LWN::ByteBuffer& byteBuffer = LWN::ByteBufferManager::GetByteBuffer(bufferID);
	if (byteBuffer.Valid)
	{
		data = byteBuffer.GetUInt(offset);
	}
}

_DLLExport VOID BufferGetULong(INT bufferID, UINT offset, UINT64& data)
{
	LWN::ByteBuffer& byteBuffer = LWN::ByteBufferManager::GetByteBuffer(bufferID);
	if (byteBuffer.Valid)
	{
		data = byteBuffer.GetULong(offset);
	}
}

_DLLExport VOID BufferGetData(INT bufferID, UINT offset, UINT dataSize, CHAR* data)
{
	LWN::ByteBuffer& byteBuffer = LWN::ByteBufferManager::GetByteBuffer(bufferID);
	if (byteBuffer.Valid)
	{
		memcpy(data, byteBuffer.GetData() + offset, dataSize);
	}
}

_DLLExport CHAR * BufferGetDataIntPtr(INT bufferID, UINT offset, UINT dataSize)
{
	alog("--------BufferGetDataIntPtr---------bufferID=%d offset=%d dataSize = %d", bufferID, offset,dataSize);
	LWN::ByteBuffer& byteBuffer = LWN::ByteBufferManager::GetByteBuffer(bufferID);
	if (byteBuffer.Valid)
	{
		memcpy(g_sdata, byteBuffer.GetData() + offset, dataSize);
		g_sdata[dataSize] = '\0';
		return g_sdata;
	}
	return NULL;
}

