#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>

// System.String
struct String_t;

#include "UnityEngine_UnityEngine_MonoBehaviour667441552.h"

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// GameStart
struct  GameStart_t2994032496  : public MonoBehaviour_t667441552
{
public:
	// System.String GameStart::msg
	String_t* ___msg_2;
	// System.String GameStart::filename
	String_t* ___filename_3;

public:
	inline static int32_t get_offset_of_msg_2() { return static_cast<int32_t>(offsetof(GameStart_t2994032496, ___msg_2)); }
	inline String_t* get_msg_2() const { return ___msg_2; }
	inline String_t** get_address_of_msg_2() { return &___msg_2; }
	inline void set_msg_2(String_t* value)
	{
		___msg_2 = value;
		Il2CppCodeGenWriteBarrier(&___msg_2, value);
	}

	inline static int32_t get_offset_of_filename_3() { return static_cast<int32_t>(offsetof(GameStart_t2994032496, ___filename_3)); }
	inline String_t* get_filename_3() const { return ___filename_3; }
	inline String_t** get_address_of_filename_3() { return &___filename_3; }
	inline void set_filename_3(String_t* value)
	{
		___filename_3 = value;
		Il2CppCodeGenWriteBarrier(&___filename_3, value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
