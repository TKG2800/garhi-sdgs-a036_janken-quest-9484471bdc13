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
            StartCoroutine(_adManager.ShowBannerAdsAsync()); // �R�[���`���Ńo�i�[�L���\��
            _bannerDisplayed = true; // �o�i�[�L���\���ςɂ���
        }
    }
}*/
