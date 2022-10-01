using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class StartManager : MonoBehaviour
{
#if UNITY_IOS
   [DllImport("__Internal")]
    private static extern void _requestIDFA();
    private void DelayIDFA()
   {
#if !UNITY_EDITOR
        _requestIDFA();
#endif
   }
#endif

    [SerializeField] AdManager _adManager;
    private bool _bannerDisplayed = false;
    private bool _interstitialDisplayed = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DelayIDFA", 1);
        StartCoroutine(_adManager.ShowInterstitialAdsAsync());
    }

    // Update is called once per frame
    void Update()
    {
        if (!_bannerDisplayed && _adManager.IsLoadedBanner)
        {
            StartCoroutine(_adManager.ShowBannerAdsAsync()); // バナー広告表示
            _bannerDisplayed = true; // バナー広告表示済にする
        }
    }

}
