//
//  TapBillboard.h
//  TapBillboardSDK
//
//  Created by TapTap on 2022/7/14.
//

#import <Foundation/Foundation.h>
#import <TapCommonSDK/TapCommonSDK.h>
#import <TapBillboardSDK/BadgeDetails.h>

#define TapBillboardSDK                @"TapBillboard"
#define TapBillboardSDK_VERSION_NUMBER @"31602001"
#define TapBillboardSDK_VERSION        @"3.16.2"

NS_ASSUME_NONNULL_BEGIN
typedef void (^TTBillboardOpenPanelHandler)(bool result, NSError *_Nullable error);
typedef void (^TTBillboardCloseHandler)(void);
typedef void (^TTBillboardRequestHandler)(BadgeDetails* _Nullable result, NSError *_Nullable error);
typedef void (^TTBillboardMessageListener)(NSString* _Nullable customUrl);

@interface TapBillboard : NSObject

+ (instancetype)new NS_UNAVAILABLE;

- (instancetype)init NS_UNAVAILABLE;

+ (void)initWithConfig:(TapConfig *)config;

/// 打开公告
+ (void)openPanel:(TTBillboardOpenPanelHandler)callback;

/// 打开公告
+ (void)openPanel:(TTBillboardOpenPanelHandler)callback closeCallback:(_Nullable TTBillboardCloseHandler)closeCallback;

/**
*  @brief 获取小红点信息
*
*/
+ (void)getBadgeDetails:(TTBillboardRequestHandler)callBack;

/**
 * @brief 注册自定义事件回调
 */
+ (void)registerCustomLinkListener:(TTBillboardMessageListener)listener;

+ (void)unRegisterCustomLinkListener:(TTBillboardMessageListener)litener;

@end

NS_ASSUME_NONNULL_END
