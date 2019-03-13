#ifndef _COMMON_H_
#define _COMMON_H_

#if _WIN64
#define _DLLExport extern "C" __declspec (dllexport)
#elif __ANDROID__
#define _DLLExport extern "C"
#elif __MACH__ //TARGET_OS_MAC
#define _DLLExport extern "C"
//#elif TARGET_OS_IPHONE
#endif


#define VOID void				
typedef unsigned char			BYTE;
typedef unsigned char			UCHAR;	
typedef char					CHAR;	
typedef char					SBYTE;
typedef int						INT;
typedef unsigned int			UINT;
typedef unsigned short			USHORT;
typedef short					SHORT;
typedef float					FLOAT;
typedef double					DOUBLE;
typedef int						BOOL;
typedef long long				INT64;
typedef unsigned long long      UINT64;
typedef const char*				STRING;
typedef const char*				STRING;

//INT GenerateGlobalID()
//{
//	static INT __globalID = 0;
//	return ++__globalID;
//}
#endif



