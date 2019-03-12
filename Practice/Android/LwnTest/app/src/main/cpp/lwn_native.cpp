//
// Created by liweina on 2019/1/15.
//

#include "Header/lwn_native.h"

int lwn_native::lwntest(int a, int b){
    LOGE("------------------lwntest--------------------a=%d,b=%d",a,b);
    int result = a+b;
    return result;
}