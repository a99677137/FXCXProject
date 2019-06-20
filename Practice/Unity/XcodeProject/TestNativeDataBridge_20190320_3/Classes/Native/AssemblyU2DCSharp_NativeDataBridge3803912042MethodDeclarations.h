#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>
#include <assert.h>
#include <exception>

// NativeDataBridge
struct NativeDataBridge_t3803912042;
// System.String
struct String_t;

#include "codegen/il2cpp-codegen.h"
#include "mscorlib_System_String7231557.h"
#include "mscorlib_System_IntPtr4010401971.h"

// System.Void NativeDataBridge::.ctor()
extern "C"  void NativeDataBridge__ctor_m2811020913 (NativeDataBridge_t3803912042 * __this, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Int32 NativeDataBridge::UnityNativeInit()
extern "C"  int32_t NativeDataBridge_UnityNativeInit_m3600849245 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void NativeDataBridge::UnityNativeRelease()
extern "C"  void NativeDataBridge_UnityNativeRelease_m508401902 (Il2CppObject * __this /* static, unused */, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Int32 NativeDataBridge::CreateFileBuffer(System.String,System.UInt32)
extern "C"  int32_t NativeDataBridge_CreateFileBuffer_m3082573917 (Il2CppObject * __this /* static, unused */, String_t* ___szFileName0, uint32_t ___len1, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Int32 NativeDataBridge::CreateBufferFromFile(System.String,System.UInt32,System.UInt32,System.UInt32)
extern "C"  int32_t NativeDataBridge_CreateBufferFromFile_m2173281723 (Il2CppObject * __this /* static, unused */, String_t* ___szFileName0, uint32_t ___offset1, uint32_t ___dataSize2, uint32_t ___len3, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Int32 NativeDataBridge::DestroyByBufferID(System.Int32)
extern "C"  int32_t NativeDataBridge_DestroyByBufferID_m3917260414 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void NativeDataBridge::BufferGetByte(System.Int32,System.UInt32,System.Byte&)
extern "C"  void NativeDataBridge_BufferGetByte_m3384355797 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, uint8_t* ___data2, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void NativeDataBridge::BufferGetDouble(System.Int32,System.UInt32,System.Double&)
extern "C"  void NativeDataBridge_BufferGetDouble_m336681077 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, double* ___data2, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void NativeDataBridge::BufferGetFloat(System.Int32,System.UInt32,System.Single&)
extern "C"  void NativeDataBridge_BufferGetFloat_m238858121 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, float* ___data2, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void NativeDataBridge::BufferGetInt(System.Int32,System.UInt32,System.Int32&)
extern "C"  void NativeDataBridge_BufferGetInt_m3004845158 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, int32_t* ___data2, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void NativeDataBridge::BufferGetLong(System.Int32,System.UInt32,System.Int64&)
extern "C"  void NativeDataBridge_BufferGetLong_m976865818 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, int64_t* ___data2, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void NativeDataBridge::BufferGetSbyte(System.Int32,System.UInt32,System.SByte&)
extern "C"  void NativeDataBridge_BufferGetSbyte_m3009021119 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, int8_t* ___data2, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void NativeDataBridge::BufferGetShort(System.Int32,System.UInt32,System.Int16&)
extern "C"  void NativeDataBridge_BufferGetShort_m613267449 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, int16_t* ___data2, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void NativeDataBridge::BufferGetUShort(System.Int32,System.UInt32,System.UInt16&)
extern "C"  void NativeDataBridge_BufferGetUShort_m977223949 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, uint16_t* ___data2, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void NativeDataBridge::BufferGetUInt(System.Int32,System.UInt32,System.UInt32&)
extern "C"  void NativeDataBridge_BufferGetUInt_m3204515252 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, uint32_t* ___data2, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void NativeDataBridge::BufferGetULong(System.Int32,System.UInt32,System.UInt64&)
extern "C"  void NativeDataBridge_BufferGetULong_m3919104356 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, uint64_t* ___data2, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.Void NativeDataBridge::BufferGetData(System.Int32,System.UInt32,System.UInt32,System.Byte&)
extern "C"  void NativeDataBridge_BufferGetData_m3284400319 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, uint32_t ___dataSize2, uint8_t* ___data3, const MethodInfo* method) IL2CPP_METHOD_ATTR;
// System.IntPtr NativeDataBridge::BufferGetDataIntPtr(System.Int32,System.UInt32,System.UInt32)
extern "C"  IntPtr_t NativeDataBridge_BufferGetDataIntPtr_m3035516114 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, uint32_t ___dataSize2, const MethodInfo* method) IL2CPP_METHOD_ATTR;
