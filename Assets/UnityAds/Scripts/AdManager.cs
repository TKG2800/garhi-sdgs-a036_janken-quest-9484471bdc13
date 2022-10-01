using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    // GameIDs
    string android_gameId = "";
    string ios_gameId = "";
    // BannerID
    const string ios_bannerPlacementId = "Banner_iOS";
    const string android_bannetPlacementId = "Banner_Android";
    // InterstitialID
    const string ios_interstitialPlacementId = "Interstitial_iOS";
    const string android_interstitialPlacementId = "Interstitial_Android";
    bool testMode = false;

    private bool _initializationAds = false;
    private string _gameId;
    private string _bannerAdUnitId;
    private string _interstitialAdUnitId;
    private bool _isLoading = false;
    private bool _isLoadedBanner = false;
    private bool _isLoadedInterstitial = false;
    private bool _adStarted = false;
    private bool _adClosed = false;

    public bool InitializationAds {
		get { return _initializationAds; }
		set { _initializationAds = value; }
	}
    public bool IsLoadedBanner {
		get { return _isLoadedBanner; }
		set { _isLoadedBanner = value; }
	}
	public bool IsLoadedInterstitial {
		get { return _isLoadedInterstitial; }
		set { _isLoadedInterstitial = value; }
	}
	public bool AdStarted {
		get { return _adStarted; }
		set { _adStarted = value; }
	}
	public bool AdClosed {
		get { return _adClosed; }
		set { _adClosed = value; }
	}

    void Start()
    {
        ios_gameId = UnityAdsGameIDs.iOS_GameID;;
        android_gameId = UnityAdsGameIDs.Android_GameID;
        Debug.Log("iOS ID " + ios_gameId);
        Debug.Log("Android ID " + android_gameId);
        setPlatform();
        StartCoroutine(CheckATTracking());
        _initializationAds = Advertisement.isInitialized;
        if (!_initializationAds)
        {
            InitializeAds();
        }
    }
    void Update()
    {
        if(!_isLoading && _initializationAds)
        {
            LoadBannerAds();
            LoadInterstitialAds();
            _isLoading = true;
        }
        IsLoadedBanner = Advertisement.Banner.isLoaded;
    }
    private void setPlatform()
    {
#if UNITY_IOS
        _gameId = ios_gameId;
#elif UNITY_ANDROID
        _gameId = android_gameId;
#endif
        Debug.Log("setPlatform GameID " + _gameId);
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _bannerAdUnitId = ios_bannerPlacementId;
			_interstitialAdUnitId = ios_interstitialPlacementId;
        } else {
            _bannerAdUnitId = android_bannetPlacementId;
			_interstitialAdUnitId = android_interstitialPlacementId;
		}

    }

	// Initialize
    private void InitializeAds()
    {
        Debug.Log("InitializeAds GameID " + _gameId);
        Advertisement.Initialize(_gameId, testMode, this);
    }

    private IEnumerator CheckATTracking()
    {
#if UNITY_IOS
        if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
            while (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                yield return null;
            }
        }
#else
        yield return null;
#endif
    }

    /// <summary>
    /// iOS14以降のトラッキング許可状態表示
    /// </summary>
    private void SetMetaData()
    {
#if UNITY_IOS
        string _adsStr = "";
        // トラッキング許可
        if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
        {
            MetaData gdprMetaData = new MetaData("gdpr");
            gdprMetaData.Set("consent", "true");
            Advertisement.SetMetaData(gdprMetaData);
            _adsStr += "IDFA: " + UnityEngine.iOS.Device.advertisingIdentifier + '\n';
            _adsStr += "GDPR Meta Data: true";
        }
        // 許可しないを選択された場合
        else
        {
            MetaData gdprMetaData = new MetaData("gdpr");
            gdprMetaData.Set("consent", "false");
            Advertisement.SetMetaData(gdprMetaData);
            _adsStr += "IDFA: " + UnityEngine.iOS.Device.advertisingIdentifier + '\n';
            _adsStr += "GDPR Meta Data: false";
        }
        Debug.Log(_adsStr);
#endif
    }

    public void OnInitializationComplete()
    {
        Debug.Log($"Unity Ads initialization({_gameId}) complete.");
        _initializationAds = true;
        SetMetaData();
    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
		_initializationAds = false;
    }
    public void LoadBannerAds()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Debug.Log("LoadBannerAds Ad: " + _bannerAdUnitId);
        Advertisement.Banner.Load(_bannerAdUnitId);
    }
    public void ShowBannerAds()
    {
        Debug.Log("ShowBannerAds Ad: " + _bannerAdUnitId);
        Advertisement.Banner.Show(_bannerAdUnitId);
    }
    public IEnumerator ShowBannerAdsAsync()
    {
        Debug.Log("ShowBannerAds Ad: " + _bannerAdUnitId);
        while (!IsLoadedBanner)
        {
            yield return new WaitForSeconds(0.3f);
        }
        Advertisement.Banner.Show(_bannerAdUnitId);
    }
    public void LoadInterstitialAds()
    {
        Debug.Log("LoadInterstitialAds Ad: " + _interstitialAdUnitId);
        Advertisement.Load(_interstitialAdUnitId, this);
    }
    public void ShowInterstitialAds()
    {
        AdClosed = false;
        Debug.Log("ShowInterstitialAds Ad: " + _interstitialAdUnitId);
		Advertisement.Show(_interstitialAdUnitId, this);
    }
    public IEnumerator ShowInterstitialAdsAsync()
    {
        Debug.Log("ShowInterstitialAds Ad: " + _interstitialAdUnitId);
        while (!IsLoadedInterstitial)
        {
            yield return new WaitForSeconds(0.3f);
        }
        AdClosed = false;
		Advertisement.Show(_interstitialAdUnitId, this);
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("OnUnityAdsAdLoaded:" + placementId);
        switch (placementId)
        {
            case ios_interstitialPlacementId:
            case android_interstitialPlacementId:
                IsLoadedInterstitial = true;
                break;
        }
    }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execite code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execite code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        Debug.Log($"OnUnityAdsShowStart {adUnitId}");
        _adStarted = true;
    }
    public void OnUnityAdsShowClick(string adUnitId) { }

    /// <summary>
    /// 動画広告がスキップあるいは閉じられたらコールされる
    /// </summary>
    /// <param name="placementId"></param>
    /// <param name="showCompletionState"></param>
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        _adClosed = true;
        _adStarted = false;
        Debug.Log("OnUnityAdsShowComplete:" + placementId + " State:" + showCompletionState.ToString());
	}
}