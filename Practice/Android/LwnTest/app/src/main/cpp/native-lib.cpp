#include <jni.h>
#include <string>
#include "Header/android_log.h"

#include "Header/lwn_native.h"

extern "C"
JNIEXPORT jstring JNICALL
Java_com_lwn_lwntest_MainActivity_stringFromJNI(JNIEnv *env, jobject /* this */) {

    int result = lwn_native::lwntest(5,6);
    std::string hello = "Hello from C++";
    LOGE("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAresult = %d",result);

    return env->NewStringUTF(hello.c_str());
}
