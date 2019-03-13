//
// Created by liweina on 2019/1/15.
//
#include <android/log.h>

#define  LOG_TAG    "lwnnative"
#define  LOGI(...)  __android_log_print(ANDROID_LOG_INFO, LOG_TAG, __VA_ARGS__)
#define LOGD(...)  __android_log_print(ANDROID_LOG_INFO, LOG_TAG, __VA_ARGS__)
#define LOGE(...)  __android_log_print(ANDROID_LOG_ERROR, LOG_TAG, __VA_ARGS__)

//#define log(format, ...) printf(format"\n", ##__VA_ARGS__) --不会报错，但是在logcat中不显示。。。