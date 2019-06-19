//
//  NativeLogHelper.m
//  NativeLogTest
//
//  Created by Lwn on 2018/10/31.
//  Copyright © 2018 Lwn. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "NativeLogHelper.h"
#import <UIKit/UIKit.h>
#include <cstdio>
#include <iostream>


@implementation NativeLogHelper

static NativeLogHelper *_instance = nil;

+(instancetype)shareInstance{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _instance =  [[super allocWithZone:NULL] init];
        
    });
    _instance.filePath = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory,NSUserDomainMask,YES).firstObject;
    NSLog(@"NativeLogHelper Instance filepath = %@",_instance.filePath);
    return _instance;
}


- (void) RegisterNSLog2DocumentFolder{
    

    UIDevice * device = [UIDevice currentDevice];
    if([[device model] hasPrefix:@"Simulator"]){
        return;
    }

    //NSString * logpath = [_filePath stringByAppendingPathComponent:@"NativeLog"];
    
    NSFileManager * fileManager = [NSFileManager defaultManager];
    //BOOL fileExists = [fileManager fileExistsAtPath:_filePath];
    //if(!fileExists){
    //    [fileManager createDirectoryAtPath:logpath withIntermediateDirectories:YES attributes:nil error:nil];
        
    //}

    NSDateFormatter * format =[[NSDateFormatter alloc] init];
    [format setLocale:[[NSLocale alloc] initWithLocaleIdentifier:@"zh_CN"] ];
    [format setDateFormat:@"yyyy_MM_dd HH_mm_ss"];
    NSString *dateStr = [format stringFromDate:[NSDate date]];
    NSString * logfilepath = [_filePath stringByAppendingFormat:@"/log_%@.txt",dateStr ];
    
    BOOL logExist = [fileManager fileExistsAtPath:logfilepath];
    if(!logExist){
        [fileManager createFileAtPath:logfilepath contents:nil attributes:nil];
    }
    
    freopen([logfilepath cStringUsingEncoding:NSASCIIStringEncoding], "a+" ,stdout);
    freopen([logfilepath cStringUsingEncoding:NSASCIIStringEncoding], "a+", stderr);
}

@end
