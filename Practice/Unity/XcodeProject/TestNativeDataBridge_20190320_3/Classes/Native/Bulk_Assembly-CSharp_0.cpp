#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <cstring>
#include <string.h>
#include <stdio.h>
#include <cmath>
#include <limits>
#include <assert.h>

// GameStart
struct GameStart_t2994032496;
// NativeDataBridge
struct NativeDataBridge_t3803912042;
// System.String
struct String_t;

#include "class-internals.h"
#include "codegen/il2cpp-codegen.h"
#include "mscorlib_System_Array1146569071.h"
#include "AssemblyU2DCSharp_U3CModuleU3E86524790.h"
#include "AssemblyU2DCSharp_U3CModuleU3E86524790MethodDeclarations.h"
#include "AssemblyU2DCSharp_GameStart2994032496.h"
#include "AssemblyU2DCSharp_GameStart2994032496MethodDeclarations.h"
#include "mscorlib_System_Void2863195528.h"
#include "UnityEngine_UnityEngine_MonoBehaviour667441552MethodDeclarations.h"
#include "mscorlib_System_String7231557.h"
#include "mscorlib_System_String7231557MethodDeclarations.h"
#include "UnityEngine_UnityEngine_Application2856536070MethodDeclarations.h"
#include "UnityEngine_UnityEngine_GUI3134605553MethodDeclarations.h"
#include "AssemblyU2DCSharp_NativeDataBridge3803912042MethodDeclarations.h"
#include "mscorlib_System_Runtime_InteropServices_Marshal87536056MethodDeclarations.h"
#include "UnityEngine_UnityEngine_Screen3187157168MethodDeclarations.h"
#include "mscorlib_System_Int321153838500.h"
#include "mscorlib_System_IntPtr4010401971.h"
#include "UnityEngine_UnityEngine_Rect4241904616.h"
#include "UnityEngine_UnityEngine_Rect4241904616MethodDeclarations.h"
#include "mscorlib_System_Single4291918972.h"
#include "mscorlib_System_Boolean476798718.h"
#include "mscorlib_System_UInt3224667981.h"
#include "mscorlib_ArrayTypes.h"
#include "mscorlib_System_Object4170816371.h"
#include "AssemblyU2DCSharp_NativeDataBridge3803912042.h"
#include "mscorlib_System_Object4170816371MethodDeclarations.h"
#include "mscorlib_System_Byte2862609660.h"
#include "mscorlib_System_Double3868226565.h"
#include "mscorlib_System_Int641153838595.h"
#include "mscorlib_System_SByte1161769777.h"
#include "mscorlib_System_Int161153838442.h"
#include "mscorlib_System_UInt1624667923.h"
#include "mscorlib_System_UInt6424668076.h"

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void GameStart::.ctor()
extern Il2CppClass* String_t_il2cpp_TypeInfo_var;
extern Il2CppCodeGenString* _stringLiteral2548907040;
extern const uint32_t GameStart__ctor_m1213211643_MetadataUsageId;
extern "C"  void GameStart__ctor_m1213211643 (GameStart_t2994032496 * __this, const MethodInfo* method)
{
	static bool s_Il2CppMethodIntialized;
	if (!s_Il2CppMethodIntialized)
	{
		il2cpp_codegen_initialize_method (GameStart__ctor_m1213211643_MetadataUsageId);
		s_Il2CppMethodIntialized = true;
	}
	{
		IL2CPP_RUNTIME_CLASS_INIT(String_t_il2cpp_TypeInfo_var);
		String_t* L_0 = ((String_t_StaticFields*)String_t_il2cpp_TypeInfo_var->static_fields)->get_Empty_2();
		__this->set_msg_2(L_0);
		__this->set_filename_3(_stringLiteral2548907040);
		MonoBehaviour__ctor_m2022291967(__this, /*hidden argument*/NULL);
		return;
	}
}
// System.Void GameStart::Start()
extern Il2CppClass* String_t_il2cpp_TypeInfo_var;
extern Il2CppCodeGenString* _stringLiteral3932108665;
extern Il2CppCodeGenString* _stringLiteral2931146705;
extern const uint32_t GameStart_Start_m160349435_MetadataUsageId;
extern "C"  void GameStart_Start_m160349435 (GameStart_t2994032496 * __this, const MethodInfo* method)
{
	static bool s_Il2CppMethodIntialized;
	if (!s_Il2CppMethodIntialized)
	{
		il2cpp_codegen_initialize_method (GameStart_Start_m160349435_MetadataUsageId);
		s_Il2CppMethodIntialized = true;
	}
	{
		String_t* L_0 = __this->get_msg_2();
		IL2CPP_RUNTIME_CLASS_INIT(String_t_il2cpp_TypeInfo_var);
		String_t* L_1 = String_Concat_m138640077(NULL /*static, unused*/, L_0, _stringLiteral3932108665, /*hidden argument*/NULL);
		__this->set_msg_2(L_1);
		String_t* L_2 = Application_get_streamingAssetsPath_m1181082379(NULL /*static, unused*/, /*hidden argument*/NULL);
		String_t* L_3 = String_Concat_m138640077(NULL /*static, unused*/, L_2, _stringLiteral2931146705, /*hidden argument*/NULL);
		__this->set_filename_3(L_3);
		return;
	}
}
// System.Void GameStart::Update()
extern "C"  void GameStart_Update_m681717362 (GameStart_t2994032496 * __this, const MethodInfo* method)
{
	{
		return;
	}
}
// System.Void GameStart::OnGUI()
extern Il2CppClass* GUI_t3134605553_il2cpp_TypeInfo_var;
extern Il2CppClass* String_t_il2cpp_TypeInfo_var;
extern Il2CppClass* ObjectU5BU5D_t1108656482_il2cpp_TypeInfo_var;
extern Il2CppClass* Int32_t1153838500_il2cpp_TypeInfo_var;
extern Il2CppClass* Marshal_t87536056_il2cpp_TypeInfo_var;
extern Il2CppCodeGenString* _stringLiteral1909762512;
extern Il2CppCodeGenString* _stringLiteral685456690;
extern Il2CppCodeGenString* _stringLiteral566446894;
extern Il2CppCodeGenString* _stringLiteral10;
extern Il2CppCodeGenString* _stringLiteral3491587712;
extern Il2CppCodeGenString* _stringLiteral3834993729;
extern Il2CppCodeGenString* _stringLiteral3317878347;
extern Il2CppCodeGenString* _stringLiteral3979943588;
extern Il2CppCodeGenString* _stringLiteral1611824373;
extern Il2CppCodeGenString* _stringLiteral534168600;
extern const uint32_t GameStart_OnGUI_m708610293_MetadataUsageId;
extern "C"  void GameStart_OnGUI_m708610293 (GameStart_t2994032496 * __this, const MethodInfo* method)
{
	static bool s_Il2CppMethodIntialized;
	if (!s_Il2CppMethodIntialized)
	{
		il2cpp_codegen_initialize_method (GameStart_OnGUI_m708610293_MetadataUsageId);
		s_Il2CppMethodIntialized = true;
	}
	int32_t V_0 = 0;
	IntPtr_t V_1;
	memset(&V_1, 0, sizeof(V_1));
	String_t* V_2 = NULL;
	int32_t V_3 = 0;
	String_t* V_4 = NULL;
	{
		Rect_t4241904616  L_0;
		memset(&L_0, 0, sizeof(L_0));
		Rect__ctor_m3291325233(&L_0, (5.0f), (5.0f), (100.0f), (80.0f), /*hidden argument*/NULL);
		IL2CPP_RUNTIME_CLASS_INIT(GUI_t3134605553_il2cpp_TypeInfo_var);
		bool L_1 = GUI_Button_m885093907(NULL /*static, unused*/, L_0, _stringLiteral1909762512, /*hidden argument*/NULL);
		if (!L_1)
		{
			goto IL_008e;
		}
	}
	{
		String_t* L_2 = __this->get_msg_2();
		IL2CPP_RUNTIME_CLASS_INIT(String_t_il2cpp_TypeInfo_var);
		String_t* L_3 = String_Concat_m138640077(NULL /*static, unused*/, L_2, _stringLiteral685456690, /*hidden argument*/NULL);
		__this->set_msg_2(L_3);
		String_t* L_4 = __this->get_filename_3();
		String_t* L_5 = __this->get_filename_3();
		NullCheck(L_5);
		int32_t L_6 = String_get_Length_m2979997331(L_5, /*hidden argument*/NULL);
		int32_t L_7 = NativeDataBridge_CreateBufferFromFile_m2173281723(NULL /*static, unused*/, L_4, 0, 0, L_6, /*hidden argument*/NULL);
		V_0 = L_7;
		String_t* L_8 = __this->get_msg_2();
		V_4 = L_8;
		ObjectU5BU5D_t1108656482* L_9 = ((ObjectU5BU5D_t1108656482*)SZArrayNew(ObjectU5BU5D_t1108656482_il2cpp_TypeInfo_var, (uint32_t)4));
		String_t* L_10 = V_4;
		NullCheck(L_9);
		IL2CPP_ARRAY_BOUNDS_CHECK(L_9, 0);
		ArrayElementTypeCheck (L_9, L_10);
		(L_9)->SetAt(static_cast<il2cpp_array_size_t>(0), (Il2CppObject *)L_10);
		ObjectU5BU5D_t1108656482* L_11 = L_9;
		NullCheck(L_11);
		IL2CPP_ARRAY_BOUNDS_CHECK(L_11, 1);
		ArrayElementTypeCheck (L_11, _stringLiteral566446894);
		(L_11)->SetAt(static_cast<il2cpp_array_size_t>(1), (Il2CppObject *)_stringLiteral566446894);
		ObjectU5BU5D_t1108656482* L_12 = L_11;
		int32_t L_13 = V_0;
		int32_t L_14 = L_13;
		Il2CppObject * L_15 = Box(Int32_t1153838500_il2cpp_TypeInfo_var, &L_14);
		NullCheck(L_12);
		IL2CPP_ARRAY_BOUNDS_CHECK(L_12, 2);
		ArrayElementTypeCheck (L_12, L_15);
		(L_12)->SetAt(static_cast<il2cpp_array_size_t>(2), (Il2CppObject *)L_15);
		ObjectU5BU5D_t1108656482* L_16 = L_12;
		NullCheck(L_16);
		IL2CPP_ARRAY_BOUNDS_CHECK(L_16, 3);
		ArrayElementTypeCheck (L_16, _stringLiteral10);
		(L_16)->SetAt(static_cast<il2cpp_array_size_t>(3), (Il2CppObject *)_stringLiteral10);
		String_t* L_17 = String_Concat_m3016520001(NULL /*static, unused*/, L_16, /*hidden argument*/NULL);
		__this->set_msg_2(L_17);
	}

IL_008e:
	{
		Rect_t4241904616  L_18;
		memset(&L_18, 0, sizeof(L_18));
		Rect__ctor_m3291325233(&L_18, (110.0f), (5.0f), (100.0f), (80.0f), /*hidden argument*/NULL);
		IL2CPP_RUNTIME_CLASS_INIT(GUI_t3134605553_il2cpp_TypeInfo_var);
		bool L_19 = GUI_Button_m885093907(NULL /*static, unused*/, L_18, _stringLiteral3491587712, /*hidden argument*/NULL);
		if (!L_19)
		{
			goto IL_00fc;
		}
	}
	{
		String_t* L_20 = __this->get_msg_2();
		IL2CPP_RUNTIME_CLASS_INIT(String_t_il2cpp_TypeInfo_var);
		String_t* L_21 = String_Concat_m138640077(NULL /*static, unused*/, L_20, _stringLiteral3834993729, /*hidden argument*/NULL);
		__this->set_msg_2(L_21);
		IntPtr_t L_22 = NativeDataBridge_BufferGetDataIntPtr_m3035516114(NULL /*static, unused*/, 1, ((int32_t)5324880), 6, /*hidden argument*/NULL);
		V_1 = L_22;
		IntPtr_t L_23 = V_1;
		IL2CPP_RUNTIME_CLASS_INIT(Marshal_t87536056_il2cpp_TypeInfo_var);
		String_t* L_24 = Marshal_PtrToStringAnsi_m1193920512(NULL /*static, unused*/, L_23, /*hidden argument*/NULL);
		V_2 = L_24;
		String_t* L_25 = __this->get_msg_2();
		String_t* L_26 = V_2;
		String_t* L_27 = String_Concat_m2933632197(NULL /*static, unused*/, L_25, _stringLiteral3317878347, L_26, _stringLiteral10, /*hidden argument*/NULL);
		__this->set_msg_2(L_27);
	}

IL_00fc:
	{
		Rect_t4241904616  L_28;
		memset(&L_28, 0, sizeof(L_28));
		Rect__ctor_m3291325233(&L_28, (215.0f), (5.0f), (100.0f), (80.0f), /*hidden argument*/NULL);
		IL2CPP_RUNTIME_CLASS_INIT(GUI_t3134605553_il2cpp_TypeInfo_var);
		bool L_29 = GUI_Button_m885093907(NULL /*static, unused*/, L_28, _stringLiteral3979943588, /*hidden argument*/NULL);
		if (!L_29)
		{
			goto IL_0178;
		}
	}
	{
		String_t* L_30 = __this->get_msg_2();
		IL2CPP_RUNTIME_CLASS_INIT(String_t_il2cpp_TypeInfo_var);
		String_t* L_31 = String_Concat_m138640077(NULL /*static, unused*/, L_30, _stringLiteral1611824373, /*hidden argument*/NULL);
		__this->set_msg_2(L_31);
		int32_t L_32 = NativeDataBridge_DestroyByBufferID_m3917260414(NULL /*static, unused*/, 1, /*hidden argument*/NULL);
		V_3 = L_32;
		String_t* L_33 = __this->get_msg_2();
		V_4 = L_33;
		ObjectU5BU5D_t1108656482* L_34 = ((ObjectU5BU5D_t1108656482*)SZArrayNew(ObjectU5BU5D_t1108656482_il2cpp_TypeInfo_var, (uint32_t)4));
		String_t* L_35 = V_4;
		NullCheck(L_34);
		IL2CPP_ARRAY_BOUNDS_CHECK(L_34, 0);
		ArrayElementTypeCheck (L_34, L_35);
		(L_34)->SetAt(static_cast<il2cpp_array_size_t>(0), (Il2CppObject *)L_35);
		ObjectU5BU5D_t1108656482* L_36 = L_34;
		NullCheck(L_36);
		IL2CPP_ARRAY_BOUNDS_CHECK(L_36, 1);
		ArrayElementTypeCheck (L_36, _stringLiteral534168600);
		(L_36)->SetAt(static_cast<il2cpp_array_size_t>(1), (Il2CppObject *)_stringLiteral534168600);
		ObjectU5BU5D_t1108656482* L_37 = L_36;
		int32_t L_38 = V_3;
		int32_t L_39 = L_38;
		Il2CppObject * L_40 = Box(Int32_t1153838500_il2cpp_TypeInfo_var, &L_39);
		NullCheck(L_37);
		IL2CPP_ARRAY_BOUNDS_CHECK(L_37, 2);
		ArrayElementTypeCheck (L_37, L_40);
		(L_37)->SetAt(static_cast<il2cpp_array_size_t>(2), (Il2CppObject *)L_40);
		ObjectU5BU5D_t1108656482* L_41 = L_37;
		NullCheck(L_41);
		IL2CPP_ARRAY_BOUNDS_CHECK(L_41, 3);
		ArrayElementTypeCheck (L_41, _stringLiteral10);
		(L_41)->SetAt(static_cast<il2cpp_array_size_t>(3), (Il2CppObject *)_stringLiteral10);
		String_t* L_42 = String_Concat_m3016520001(NULL /*static, unused*/, L_41, /*hidden argument*/NULL);
		__this->set_msg_2(L_42);
	}

IL_0178:
	{
		int32_t L_43 = Screen_get_height_m1504859443(NULL /*static, unused*/, /*hidden argument*/NULL);
		int32_t L_44 = Screen_get_width_m3080333084(NULL /*static, unused*/, /*hidden argument*/NULL);
		Rect_t4241904616  L_45;
		memset(&L_45, 0, sizeof(L_45));
		Rect__ctor_m3291325233(&L_45, (5.0f), (((float)((float)((int32_t)((int32_t)L_43-(int32_t)((int32_t)205)))))), (((float)((float)((int32_t)((int32_t)L_44-(int32_t)((int32_t)10)))))), (200.0f), /*hidden argument*/NULL);
		String_t* L_46 = __this->get_msg_2();
		IL2CPP_RUNTIME_CLASS_INIT(GUI_t3134605553_il2cpp_TypeInfo_var);
		GUI_TextArea_m679253918(NULL /*static, unused*/, L_45, L_46, /*hidden argument*/NULL);
		return;
	}
}
// System.Void GameStart::OnApplicationQuit()
extern Il2CppClass* String_t_il2cpp_TypeInfo_var;
extern Il2CppCodeGenString* _stringLiteral3562914792;
extern const uint32_t GameStart_OnApplicationQuit_m28537081_MetadataUsageId;
extern "C"  void GameStart_OnApplicationQuit_m28537081 (GameStart_t2994032496 * __this, const MethodInfo* method)
{
	static bool s_Il2CppMethodIntialized;
	if (!s_Il2CppMethodIntialized)
	{
		il2cpp_codegen_initialize_method (GameStart_OnApplicationQuit_m28537081_MetadataUsageId);
		s_Il2CppMethodIntialized = true;
	}
	{
		String_t* L_0 = __this->get_msg_2();
		IL2CPP_RUNTIME_CLASS_INIT(String_t_il2cpp_TypeInfo_var);
		String_t* L_1 = String_Concat_m138640077(NULL /*static, unused*/, L_0, _stringLiteral3562914792, /*hidden argument*/NULL);
		__this->set_msg_2(L_1);
		NativeDataBridge_UnityNativeRelease_m508401902(NULL /*static, unused*/, /*hidden argument*/NULL);
		return;
	}
}
// System.Void NativeDataBridge::.ctor()
extern "C"  void NativeDataBridge__ctor_m2811020913 (NativeDataBridge_t3803912042 * __this, const MethodInfo* method)
{
	{
		Object__ctor_m1772956182(__this, /*hidden argument*/NULL);
		return;
	}
}
extern "C" int32_t CDECL UnityNativeInit();
// System.Int32 NativeDataBridge::UnityNativeInit()
extern "C"  int32_t NativeDataBridge_UnityNativeInit_m3600849245 (Il2CppObject * __this /* static, unused */, const MethodInfo* method)
{
	typedef int32_t (CDECL *PInvokeFunc) ();

	// Native function invocation
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(UnityNativeInit)();

	return returnValue;
}
extern "C" void CDECL UnityNativeRelease();
// System.Void NativeDataBridge::UnityNativeRelease()
extern "C"  void NativeDataBridge_UnityNativeRelease_m508401902 (Il2CppObject * __this /* static, unused */, const MethodInfo* method)
{
	typedef void (CDECL *PInvokeFunc) ();

	// Native function invocation
	reinterpret_cast<PInvokeFunc>(UnityNativeRelease)();

}
extern "C" int32_t CDECL CreateFileBuffer(char*, uint32_t);
// System.Int32 NativeDataBridge::CreateFileBuffer(System.String,System.UInt32)
extern "C"  int32_t NativeDataBridge_CreateFileBuffer_m3082573917 (Il2CppObject * __this /* static, unused */, String_t* ___szFileName0, uint32_t ___len1, const MethodInfo* method)
{
	typedef int32_t (CDECL *PInvokeFunc) (char*, uint32_t);

	// Marshaling of parameter '___szFileName0' to native representation
	char* ____szFileName0_marshaled = NULL;
	____szFileName0_marshaled = il2cpp_codegen_marshal_string(___szFileName0);

	// Native function invocation
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(CreateFileBuffer)(____szFileName0_marshaled, ___len1);

	// Marshaling cleanup of parameter '___szFileName0' native representation
	il2cpp_codegen_marshal_free(____szFileName0_marshaled);
	____szFileName0_marshaled = NULL;

	return returnValue;
}
extern "C" int32_t CDECL CreateBufferFromFile(char*, uint32_t, uint32_t, uint32_t);
// System.Int32 NativeDataBridge::CreateBufferFromFile(System.String,System.UInt32,System.UInt32,System.UInt32)
extern "C"  int32_t NativeDataBridge_CreateBufferFromFile_m2173281723 (Il2CppObject * __this /* static, unused */, String_t* ___szFileName0, uint32_t ___offset1, uint32_t ___dataSize2, uint32_t ___len3, const MethodInfo* method)
{
	typedef int32_t (CDECL *PInvokeFunc) (char*, uint32_t, uint32_t, uint32_t);

	// Marshaling of parameter '___szFileName0' to native representation
	char* ____szFileName0_marshaled = NULL;
	____szFileName0_marshaled = il2cpp_codegen_marshal_string(___szFileName0);

	// Native function invocation
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(CreateBufferFromFile)(____szFileName0_marshaled, ___offset1, ___dataSize2, ___len3);

	// Marshaling cleanup of parameter '___szFileName0' native representation
	il2cpp_codegen_marshal_free(____szFileName0_marshaled);
	____szFileName0_marshaled = NULL;

	return returnValue;
}
extern "C" int32_t CDECL DestroyByBufferID(int32_t);
// System.Int32 NativeDataBridge::DestroyByBufferID(System.Int32)
extern "C"  int32_t NativeDataBridge_DestroyByBufferID_m3917260414 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, const MethodInfo* method)
{
	typedef int32_t (CDECL *PInvokeFunc) (int32_t);

	// Native function invocation
	int32_t returnValue = reinterpret_cast<PInvokeFunc>(DestroyByBufferID)(___bufferID0);

	return returnValue;
}
extern "C" void CDECL BufferGetByte(int32_t, uint32_t, uint8_t*);
// System.Void NativeDataBridge::BufferGetByte(System.Int32,System.UInt32,System.Byte&)
extern "C"  void NativeDataBridge_BufferGetByte_m3384355797 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, uint8_t* ___data2, const MethodInfo* method)
{
	typedef void (CDECL *PInvokeFunc) (int32_t, uint32_t, uint8_t*);

	// Marshaling of parameter '___data2' to native representation
	uint8_t* ____data2_marshaled = NULL;
	uint8_t ____data2_marshaled_dereferenced = 0;
	____data2_marshaled_dereferenced = *___data2;
	____data2_marshaled = &____data2_marshaled_dereferenced;

	// Native function invocation
	reinterpret_cast<PInvokeFunc>(BufferGetByte)(___bufferID0, ___offset1, ____data2_marshaled);

	// Marshaling of parameter '___data2' back from native representation
	uint8_t _____data2_marshaled_unmarshaled_dereferenced = 0x0;
	_____data2_marshaled_unmarshaled_dereferenced = *____data2_marshaled;
	*___data2 = _____data2_marshaled_unmarshaled_dereferenced;;

}
extern "C" void CDECL BufferGetDouble(int32_t, uint32_t, double*);
// System.Void NativeDataBridge::BufferGetDouble(System.Int32,System.UInt32,System.Double&)
extern "C"  void NativeDataBridge_BufferGetDouble_m336681077 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, double* ___data2, const MethodInfo* method)
{
	typedef void (CDECL *PInvokeFunc) (int32_t, uint32_t, double*);

	// Marshaling of parameter '___data2' to native representation
	double* ____data2_marshaled = NULL;
	double ____data2_marshaled_dereferenced = 0.0;
	____data2_marshaled_dereferenced = *___data2;
	____data2_marshaled = &____data2_marshaled_dereferenced;

	// Native function invocation
	reinterpret_cast<PInvokeFunc>(BufferGetDouble)(___bufferID0, ___offset1, ____data2_marshaled);

	// Marshaling of parameter '___data2' back from native representation
	double _____data2_marshaled_unmarshaled_dereferenced = 0.0;
	_____data2_marshaled_unmarshaled_dereferenced = *____data2_marshaled;
	*___data2 = _____data2_marshaled_unmarshaled_dereferenced;;

}
extern "C" void CDECL BufferGetFloat(int32_t, uint32_t, float*);
// System.Void NativeDataBridge::BufferGetFloat(System.Int32,System.UInt32,System.Single&)
extern "C"  void NativeDataBridge_BufferGetFloat_m238858121 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, float* ___data2, const MethodInfo* method)
{
	typedef void (CDECL *PInvokeFunc) (int32_t, uint32_t, float*);

	// Marshaling of parameter '___data2' to native representation
	float* ____data2_marshaled = NULL;
	float ____data2_marshaled_dereferenced = 0.0f;
	____data2_marshaled_dereferenced = *___data2;
	____data2_marshaled = &____data2_marshaled_dereferenced;

	// Native function invocation
	reinterpret_cast<PInvokeFunc>(BufferGetFloat)(___bufferID0, ___offset1, ____data2_marshaled);

	// Marshaling of parameter '___data2' back from native representation
	float _____data2_marshaled_unmarshaled_dereferenced = 0.0f;
	_____data2_marshaled_unmarshaled_dereferenced = *____data2_marshaled;
	*___data2 = _____data2_marshaled_unmarshaled_dereferenced;;

}
extern "C" void CDECL BufferGetInt(int32_t, uint32_t, int32_t*);
// System.Void NativeDataBridge::BufferGetInt(System.Int32,System.UInt32,System.Int32&)
extern "C"  void NativeDataBridge_BufferGetInt_m3004845158 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, int32_t* ___data2, const MethodInfo* method)
{
	typedef void (CDECL *PInvokeFunc) (int32_t, uint32_t, int32_t*);

	// Marshaling of parameter '___data2' to native representation
	int32_t* ____data2_marshaled = NULL;
	int32_t ____data2_marshaled_dereferenced = 0;
	____data2_marshaled_dereferenced = *___data2;
	____data2_marshaled = &____data2_marshaled_dereferenced;

	// Native function invocation
	reinterpret_cast<PInvokeFunc>(BufferGetInt)(___bufferID0, ___offset1, ____data2_marshaled);

	// Marshaling of parameter '___data2' back from native representation
	int32_t _____data2_marshaled_unmarshaled_dereferenced = 0;
	_____data2_marshaled_unmarshaled_dereferenced = *____data2_marshaled;
	*___data2 = _____data2_marshaled_unmarshaled_dereferenced;;

}
extern "C" void CDECL BufferGetLong(int32_t, uint32_t, int64_t*);
// System.Void NativeDataBridge::BufferGetLong(System.Int32,System.UInt32,System.Int64&)
extern "C"  void NativeDataBridge_BufferGetLong_m976865818 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, int64_t* ___data2, const MethodInfo* method)
{
	typedef void (CDECL *PInvokeFunc) (int32_t, uint32_t, int64_t*);

	// Marshaling of parameter '___data2' to native representation
	int64_t* ____data2_marshaled = NULL;
	int64_t ____data2_marshaled_dereferenced = 0;
	____data2_marshaled_dereferenced = *___data2;
	____data2_marshaled = &____data2_marshaled_dereferenced;

	// Native function invocation
	reinterpret_cast<PInvokeFunc>(BufferGetLong)(___bufferID0, ___offset1, ____data2_marshaled);

	// Marshaling of parameter '___data2' back from native representation
	int64_t _____data2_marshaled_unmarshaled_dereferenced = 0;
	_____data2_marshaled_unmarshaled_dereferenced = *____data2_marshaled;
	*___data2 = _____data2_marshaled_unmarshaled_dereferenced;;

}
extern "C" void CDECL BufferGetSbyte(int32_t, uint32_t, int8_t*);
// System.Void NativeDataBridge::BufferGetSbyte(System.Int32,System.UInt32,System.SByte&)
extern "C"  void NativeDataBridge_BufferGetSbyte_m3009021119 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, int8_t* ___data2, const MethodInfo* method)
{
	typedef void (CDECL *PInvokeFunc) (int32_t, uint32_t, int8_t*);

	// Marshaling of parameter '___data2' to native representation
	int8_t* ____data2_marshaled = NULL;
	int8_t ____data2_marshaled_dereferenced = 0;
	____data2_marshaled_dereferenced = *___data2;
	____data2_marshaled = &____data2_marshaled_dereferenced;

	// Native function invocation
	reinterpret_cast<PInvokeFunc>(BufferGetSbyte)(___bufferID0, ___offset1, ____data2_marshaled);

	// Marshaling of parameter '___data2' back from native representation
	int8_t _____data2_marshaled_unmarshaled_dereferenced = 0x0;
	_____data2_marshaled_unmarshaled_dereferenced = *____data2_marshaled;
	*___data2 = _____data2_marshaled_unmarshaled_dereferenced;;

}
extern "C" void CDECL BufferGetShort(int32_t, uint32_t, int16_t*);
// System.Void NativeDataBridge::BufferGetShort(System.Int32,System.UInt32,System.Int16&)
extern "C"  void NativeDataBridge_BufferGetShort_m613267449 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, int16_t* ___data2, const MethodInfo* method)
{
	typedef void (CDECL *PInvokeFunc) (int32_t, uint32_t, int16_t*);

	// Marshaling of parameter '___data2' to native representation
	int16_t* ____data2_marshaled = NULL;
	int16_t ____data2_marshaled_dereferenced = 0;
	____data2_marshaled_dereferenced = *___data2;
	____data2_marshaled = &____data2_marshaled_dereferenced;

	// Native function invocation
	reinterpret_cast<PInvokeFunc>(BufferGetShort)(___bufferID0, ___offset1, ____data2_marshaled);

	// Marshaling of parameter '___data2' back from native representation
	int16_t _____data2_marshaled_unmarshaled_dereferenced = 0;
	_____data2_marshaled_unmarshaled_dereferenced = *____data2_marshaled;
	*___data2 = _____data2_marshaled_unmarshaled_dereferenced;;

}
extern "C" void CDECL BufferGetUShort(int32_t, uint32_t, uint16_t*);
// System.Void NativeDataBridge::BufferGetUShort(System.Int32,System.UInt32,System.UInt16&)
extern "C"  void NativeDataBridge_BufferGetUShort_m977223949 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, uint16_t* ___data2, const MethodInfo* method)
{
	typedef void (CDECL *PInvokeFunc) (int32_t, uint32_t, uint16_t*);

	// Marshaling of parameter '___data2' to native representation
	uint16_t* ____data2_marshaled = NULL;
	uint16_t ____data2_marshaled_dereferenced = 0;
	____data2_marshaled_dereferenced = *___data2;
	____data2_marshaled = &____data2_marshaled_dereferenced;

	// Native function invocation
	reinterpret_cast<PInvokeFunc>(BufferGetUShort)(___bufferID0, ___offset1, ____data2_marshaled);

	// Marshaling of parameter '___data2' back from native representation
	uint16_t _____data2_marshaled_unmarshaled_dereferenced = 0;
	_____data2_marshaled_unmarshaled_dereferenced = *____data2_marshaled;
	*___data2 = _____data2_marshaled_unmarshaled_dereferenced;;

}
extern "C" void CDECL BufferGetUInt(int32_t, uint32_t, uint32_t*);
// System.Void NativeDataBridge::BufferGetUInt(System.Int32,System.UInt32,System.UInt32&)
extern "C"  void NativeDataBridge_BufferGetUInt_m3204515252 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, uint32_t* ___data2, const MethodInfo* method)
{
	typedef void (CDECL *PInvokeFunc) (int32_t, uint32_t, uint32_t*);

	// Marshaling of parameter '___data2' to native representation
	uint32_t* ____data2_marshaled = NULL;
	uint32_t ____data2_marshaled_dereferenced = 0;
	____data2_marshaled_dereferenced = *___data2;
	____data2_marshaled = &____data2_marshaled_dereferenced;

	// Native function invocation
	reinterpret_cast<PInvokeFunc>(BufferGetUInt)(___bufferID0, ___offset1, ____data2_marshaled);

	// Marshaling of parameter '___data2' back from native representation
	uint32_t _____data2_marshaled_unmarshaled_dereferenced = 0;
	_____data2_marshaled_unmarshaled_dereferenced = *____data2_marshaled;
	*___data2 = _____data2_marshaled_unmarshaled_dereferenced;;

}
extern "C" void CDECL BufferGetULong(int32_t, uint32_t, uint64_t*);
// System.Void NativeDataBridge::BufferGetULong(System.Int32,System.UInt32,System.UInt64&)
extern "C"  void NativeDataBridge_BufferGetULong_m3919104356 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, uint64_t* ___data2, const MethodInfo* method)
{
	typedef void (CDECL *PInvokeFunc) (int32_t, uint32_t, uint64_t*);

	// Marshaling of parameter '___data2' to native representation
	uint64_t* ____data2_marshaled = NULL;
	uint64_t ____data2_marshaled_dereferenced = 0;
	____data2_marshaled_dereferenced = *___data2;
	____data2_marshaled = &____data2_marshaled_dereferenced;

	// Native function invocation
	reinterpret_cast<PInvokeFunc>(BufferGetULong)(___bufferID0, ___offset1, ____data2_marshaled);

	// Marshaling of parameter '___data2' back from native representation
	uint64_t _____data2_marshaled_unmarshaled_dereferenced = 0;
	_____data2_marshaled_unmarshaled_dereferenced = *____data2_marshaled;
	*___data2 = _____data2_marshaled_unmarshaled_dereferenced;;

}
extern "C" void CDECL BufferGetData(int32_t, uint32_t, uint32_t, uint8_t*);
// System.Void NativeDataBridge::BufferGetData(System.Int32,System.UInt32,System.UInt32,System.Byte&)
extern "C"  void NativeDataBridge_BufferGetData_m3284400319 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, uint32_t ___dataSize2, uint8_t* ___data3, const MethodInfo* method)
{
	typedef void (CDECL *PInvokeFunc) (int32_t, uint32_t, uint32_t, uint8_t*);

	// Marshaling of parameter '___data3' to native representation
	uint8_t* ____data3_marshaled = NULL;
	uint8_t ____data3_marshaled_dereferenced = 0;
	____data3_marshaled_dereferenced = *___data3;
	____data3_marshaled = &____data3_marshaled_dereferenced;

	// Native function invocation
	reinterpret_cast<PInvokeFunc>(BufferGetData)(___bufferID0, ___offset1, ___dataSize2, ____data3_marshaled);

	// Marshaling of parameter '___data3' back from native representation
	uint8_t _____data3_marshaled_unmarshaled_dereferenced = 0x0;
	_____data3_marshaled_unmarshaled_dereferenced = *____data3_marshaled;
	*___data3 = _____data3_marshaled_unmarshaled_dereferenced;;

}
extern "C" intptr_t CDECL BufferGetDataIntPtr(int32_t, uint32_t, uint32_t);
// System.IntPtr NativeDataBridge::BufferGetDataIntPtr(System.Int32,System.UInt32,System.UInt32)
extern "C"  IntPtr_t NativeDataBridge_BufferGetDataIntPtr_m3035516114 (Il2CppObject * __this /* static, unused */, int32_t ___bufferID0, uint32_t ___offset1, uint32_t ___dataSize2, const MethodInfo* method)
{
	typedef intptr_t (CDECL *PInvokeFunc) (int32_t, uint32_t, uint32_t);

	// Native function invocation
	intptr_t returnValue = reinterpret_cast<PInvokeFunc>(BufferGetDataIntPtr)(___bufferID0, ___offset1, ___dataSize2);

	// Marshaling of return value back from native representation
	IntPtr_t _returnValue_unmarshaled;
	_returnValue_unmarshaled.set_m_value_0(reinterpret_cast<void*>((intptr_t)returnValue));

	return _returnValue_unmarshaled;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
