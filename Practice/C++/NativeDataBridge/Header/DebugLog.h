#pragma once
//#ifndef _DEBUGLOG_H_
//#define _DEBUGLOG_H_

#define DEBUG

#ifdef DEBUG  

#if _WIN64

#define alog(format, ...) fprintf(stdout, format"\n",  ##__VA_ARGS__)

#elif __ANDROID__

#include <android/log.h>
#define LOG_TAG    "lwnnative"
//#define logi(...)  __android_log_print(ANDROID_LOG_INFO, LOG_TAG, __VA_ARGS__)
//#define logd(...)  __android_log_print(ANDROID_LOG_DEBUG, LOG_TAG, __VA_ARGS__)
//#define loge(...)  __android_log_print(ANDROID_LOG_ERROR, LOG_TAG, __VA_ARGS__)

#define	alog(...)  __android_log_print(ANDROID_LOG_ERROR, LOG_TAG, __VA_ARGS__)

//#define log(format, ...) printf(format"\n", ##__VA_ARGS__) //不会报错，但是在logcat中不显示。。。

#elif __MACH__  //TARGET_OS_MAC

//#define?NSLog(format, ...)?printf("\n[%s]?%s?[第%d行]?%s\n", __TIME__, __FUNCTION__, __LINE__, [[NSString?stringWithFormat:format, ##?__VA_ARGS__]?UTF8String]);
//#define log(format,...) NSLog(format, ...)
#define alog(format,...) printf("\n[%s_%s] [%s] [Line:%d] %s\n",__DATE__, __TIME__, __FUNCTION__, __LINE__, [[NSString stringWithFormat : format, ## __VA_ARGS__] UTF8String]);

//#elif TARGET_OS_IPHONE
#endif


#else  


#if _WIN64
#define alog(format, ...)

#elif __ANDROID__

#define	alog(...)

#elif __MACH__ //TARGET_OS_MAC

#define alog(format,...)
//#elif TARGET_OS_IPHONE
#endif

#endif  







//#endif