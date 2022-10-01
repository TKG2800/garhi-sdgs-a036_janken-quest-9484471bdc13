/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class UnityAdsManager : MonoBehaviour
{
#if UNITY_IOS
            [DllImport("__Internal")] 
            private static extern void _requestIDFA(); 
#endif

    [SerializeField] AdManager _adManager;
    private bool _bannerDisplayed = false;
    private bool _interstitialDisplayed = false;

    void Start()
    {
        #if UNITY_IOS && !UNITY_EDITOR
        _               requestIDFA(); 
        #endif
    }

    // Update is called once per frame
    void Update()
    {

        if (!_bannerDisplayed)
        {
            StartCoroutine(_adManager.ShowBannerAdsAsync()); // コールチンでバナー広告表示
            _bannerDisplayed = true; // バナー広告表示済にする
        }
    }
}*/
