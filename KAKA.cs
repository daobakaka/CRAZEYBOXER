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
            {///.....΢�Ź�����
                WeChatWASM.WXBase.InitSDK((code) =>
                {
                    CreateRewardedVideoAd();

                });//��Ӽ�����Ƶ�ؼ�
                adrewardgets(); ;//��ӹ����

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
    /// //��������
    /// </summary>
    void rechoosenum_recover()//������ѡ���
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
            // �û�����ˡ��رչ�桿��ť
            // С�� 2.1.0 �Ļ�����汾��res ��һ�� undefined
            if (res.isEnded || res == null)
               {
                // �������Ž����������·���Ϸ����
                rewardgets = true;
                   usenum++;
               }
               else
               {
                // ������;�˳������·���Ϸ����
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
                            title = "һ��ս����",
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


