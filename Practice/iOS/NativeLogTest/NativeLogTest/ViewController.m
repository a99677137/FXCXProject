//
//  ViewController.m
//  NativeLogTest
//
//  Created by admin on 2018/10/30.
//  Copyright © 2018 Lwn. All rights reserved.
//

#import "ViewController.h"
#import <objc/runtime.h>


@interface ViewController ()

@end

@implementation ViewController


-(void)runtimeRelaceAlert1{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        Method presentM = class_getInstanceMethod(self.class, @selector(presentViewController:animated:completion:));
        Method presentSwizzlingM = class_getInstanceMethod(self.class, @selector(lq_presentViewController:animated:completion:));
        
        method_exchangeImplementations(presentM, presentSwizzlingM);
    });
    
}

-(void)runtimeRelaceAlert2{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        Method presentM = class_getInstanceMethod(self.class, @selector(presentViewController:animated:completion:));
        Method presentSwizzlingM = class_getInstanceMethod(self.class, @selector(lq_presentViewController:animated:completion:));
        
        method_exchangeImplementations(presentSwizzlingM,presentM );
    });
    
}

- (void)lq_presentViewController:(UIViewController *)viewControllerToPresent animated:(BOOL)flag completion:(void (^)(void))completion {
    
    if ([viewControllerToPresent isKindOfClass:[UIAlertController class]]) {
        //        NSLog(@"title : %@",((UIAlertController *)viewControllerToPresent).title);
        //        NSLog(@"message : %@",((UIAlertController *)viewControllerToPresent).message);
        
        UIAlertController *alertController = (UIAlertController *)viewControllerToPresent;
        if (alertController.title == nil && alertController.message == nil) {
            
            return;
        }
    }
    //[super presentViewController:viewControllerToPresent animated:flag completion:completion];//死循环
    [self presentViewController:viewControllerToPresent animated:flag completion:completion];//死循环
    //[self lq_presentViewController:viewControllerToPresent animated:flag completion:completion];//崩main
    //[super lq_presentViewController:viewControllerToPresent animated:flag completion:completion];//报错
}

-(void)presentViewController:(UIViewController *)viewControllerToPresent animated:(BOOL)flag completion:(void (^)(void))completion{
    
    [super presentViewController:viewControllerToPresent animated:flag completion:completion];
}

- (void)viewDidLoad {
    [super viewDidLoad];
    [self runtimeRelaceAlert1];
    //[self runtimeReplaceAlert];
    // Do any additional setup after loading the view, typically from a nib.
    
    UIButton *btn =[UIButton buttonWithType:UIButtonTypeRoundedRect];
    //初始化按钮，设置按钮类型
    //self.btn = [UIButton buttonWithType:UIButtonTypeSystem];
    //设置按钮位置和尺寸
    btn.frame = CGRectMake(50, 50, 300, 80);
    //设置按钮文字标题
    [btn setTitle:@"我是按钮,点我换图标，不弹系统提示" forState:UIControlStateNormal];
    //设置按钮文字颜色
    [btn setTitleColor:[UIColor orangeColor] forState:UIControlStateNormal];
    
    [btn setBackgroundColor:[UIColor grayColor]];
    //下面的这个属性设置为yes的状态下，按钮按下会发光，这可以用于信息按钮或者有些重要的按钮
    btn.showsTouchWhenHighlighted = YES;
    
    //按钮响应点击事件，最常用的方法：第一个参数是目标对象，一般都是self； 第二个参数是一个SEL类型的方法；第三个参数是按钮点击事件
    [btn addTarget:self action:@selector(butClick:) forControlEvents:UIControlEventTouchUpInside];
    
    //将控件添加到当前视图上
    [self.view addSubview:btn];
    
    UIButton *btn2 =[UIButton buttonWithType:UIButtonTypeRoundedRect];
    
    btn2.frame = CGRectMake(50, 160, 200, 80);
    [btn2 setTitle:@"弹窗" forState:UIControlStateNormal];
    btn2.showsTouchWhenHighlighted = YES;
    [btn2 addTarget:self action:@selector(butClick2:) forControlEvents:UIControlEventTouchUpInside];
    [self.view addSubview:btn2];
}

- (void)butClick2:(UIButton *)sender {
    // 按钮点击事件回调处理
    UIAlertView * alert = [[UIAlertView alloc] initWithTitle:@"普通弹窗"
                                                    message:@"我是普通的弹窗"
                                                    delegate:nil
                                          cancelButtonTitle:@"确认"
                                           otherButtonTitles:nil];    // 设置按钮选中技巧
    [alert show];
    sender.selected = !sender.selected;
}

- (void)butClick:(UIButton *)sender {
    
    // 按钮点击事件回调处理
    [self changeAppIconWithName:@"ios"];
    //[self runtimeRelaceAlert2];
    // 设置按钮选中技巧
    sender.selected = !sender.selected;
}




-(void)changeAppIconWithName:(NSString *)iconName {
    //#if __IPHONE_OS_VERSION_MIN_REQUIRED >= __IPHONE_10_3
    if([[UIDevice currentDevice] systemVersion].doubleValue >= 10.3){
        if(! [[UIApplication sharedApplication] supportsAlternateIcons]){
            return;
        }
        if( [iconName isEqualToString:@""]){
            iconName = nil;
        }
        [[UIApplication sharedApplication] setAlternateIconName:iconName completionHandler:^(NSError * _Nullable error) {
            if(error){
                NSLog(@"$$$$$$$$$$$$$$$$$$$ Change Fail!!! %@",error);
            }
        }];
    }
    //#endif
}





-(void)runtimeReplaceAlert{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        Method presendM = class_getClassMethod(self.class, @selector(presentViewController:animated:completion:));
        Method presentSwizzlingM = class_getClassMethod(self.class, @selector(ox_presentViewController:animated:completion:));
        method_exchangeImplementations(presendM, presentSwizzlingM);
    });
    
}

-(void)ox_presentViewController:(UIViewController *)viewControllerTpPresent animated:(BOOL)flag completion:(void (^)(void))completion{
    if( [viewControllerTpPresent isKindOfClass:[UIAlertController class]]){
        NSLog(@"title: %@",((UIAlertController *) viewControllerTpPresent).title);
        NSLog(@"message: %@",( (UIAlertController *) viewControllerTpPresent).message);
        
        UIAlertController * alertController = (UIAlertController *) viewControllerTpPresent;
        if(alertController.title == nil && alertController.message == nil){
            return;
            
        }else{
            [self ox_presentViewController:viewControllerTpPresent animated:flag completion:completion];
            return;
            
        }
        
    }
    [self ox_presentViewController:viewControllerTpPresent animated:flag completion:completion];
}

@end
