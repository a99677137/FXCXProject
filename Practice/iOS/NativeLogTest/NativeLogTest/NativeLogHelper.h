//
//  NativeLogHelper.h
//  NativeLogTest
//
//  Created by Lwn on 2018/10/30.
//  Copyright © 2018 Lwn. All rights reserved.
//

//#ifndef NativeLogHelper_h
//#define NativeLogHelper_h

#import <Foundation/Foundation.h>



@interface NativeLogHelper : NSObject

@property(nonatomic,copy) NSString * filePath;

+(instancetype) shareInstance;

- (void) RegisterNSLog2DocumentFolder;


@end



//#endif /* NativeLogHelper_h */
