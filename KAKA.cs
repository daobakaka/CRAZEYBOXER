using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAKA : MonoBehaviour
{
    // Start is called before the first frame update
    private bool rewardgets = false;
    public int usenum = 2;

#if UNITY_STANDALONE_LINUX
#elif UNITY_WEBGL
    private WeChatWASM.WXRewardedVideoAd RewardedVideoAd;
#endif
    void Start()
    {
#if UNITY_STANDALONE_LINUX
#elif UNITY_WEBGL

        {
            {///.....微信广告组件
                WeChatWASM.WXBase.InitSDK((code) =>
                {
                    CreateRewardedVideoAd();

                });//添加激励视频控件
                adrewardgets(); ;//添加广告监控

                ///..
            }
        }
#endif

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// //广告监控组件
    /// </summary>
    void rechoosenum_recover()//技能重选监控
    {
        if (rewardgets == true)
        {
            rewardgets = false;


        }
    }
    private void CreateRewardedVideoAd()
    {
#if UNITY_STANDALONE_LINUX
#elif UNITY_WEBGL
        {


            RewardedVideoAd = WeChatWASM.WXBase.CreateRewardedVideoAd(new WeChatWASM.WXCreateRewardedVideoAdParam()
            {
                adUnitId = "adunit-afb7c1cc9c260088",
            });
            RewardedVideoAd.OnLoad((res) =>
            {
                Debug.Log("RewardedVideoAd.OnLoad:" + JsonUtility.ToJson(res));
                var reportShareBehaviorRes = RewardedVideoAd.ReportShareBehavior(new WeChatWASM.RequestAdReportShareBehaviorParam()
                {
                    operation = 1,
                    currentShow = 1,
                    strategy = 0,
                    shareValue = res.shareValue,
                    rewardValue = res.rewardValue,
                    depositAmount = 100,
                });
                Debug.Log("ReportShareBehavior.Res:" + JsonUtility.ToJson(reportShareBehaviorRes));
            });
            RewardedVideoAd.OnError((err) =>
            {
                Debug.Log("RewardedVideoAd.OnError:" + JsonUtility.ToJson(err));
            });
            RewardedVideoAd.OnClose((res) =>
            {
                Debug.Log("RewardedVideoAd.OnClose:" + JsonUtility.ToJson(res));
            });
            RewardedVideoAd.Load();
        }
#endif

    }
    public void ShowRewardedVideoAd()
    {
#if UNITY_STANDALONE_LINUX
#elif UNITY_WEBGL
        {
            RewardedVideoAd.Show();
        }
#endif
    }

    public void DestroyRewardedVideoAd()
    {
#if UNITY_STANDALONE_LINUX
#elif UNITY_WEBGL
        {
            RewardedVideoAd.Destroy();
        }
#endif
    }

    private void adrewardgets()

    {
#if UNITY_STANDALONE_LINUX
#elif UNITY_WEBGL
        {
            RewardedVideoAd.OnClose(res =>
           {
            // 用户点击了【关闭广告】按钮
            // 小于 2.1.0 的基础库版本，res 是一个 undefined
            if (res.isEnded || res == null)
               {
                // 正常播放结束，可以下发游戏奖励
                rewardgets = true;
                   usenum++;
               }
               else
               {
                // 播放中途退出，不下发游戏奖励
                rewardgets = false;
               }
           });
        }
#endif
    }

    public void Sharetime()
    {

#if UNITY_STANDALONE_LINUX
#elif UNITY_WEBGL
        {

            WeChatWASM.WX.InitSDK((code) => { });

            WeChatWASM.WX.OnTouchStart((res) =>
            {
                WeChatWASM.WXCanvas.ToTempFilePath(new WeChatWASM.WXToTempFilePathParam()
                {
                    x = 0,
                    y = 0,
                    width = 375,
                    height = 300,
                    destWidth = 375,
                    destHeight = 300,
                    success = (result) =>
                    {
                        Debug.Log("ToTempFilePath success" + JsonUtility.ToJson(result));
                        WeChatWASM.WX.ShareAppMessage(new WeChatWASM.ShareAppMessageOption()
                        {
                            title = "一起战斗吧",
                            imageUrl = result.tempFilePath,
                        });
                    },
                    fail = (result) =>
                    {
                        Debug.Log("ToTempFilePath fail" + JsonUtility.ToJson(result));
                    },
                    complete = (result) =>
                    {
                        rewardgets = true;
                        usenum++;
                    },
                });
            });


        }
#endif

    }
}


