using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class AppVersion
{
    public string android;
    public string ios;
}

public class CheckVersion : MonoBehaviour
{
	[SerializeField] private string _versionURL = "";
    [SerializeField] private string _googlePlayStoreUrl = "https://play.google.com";
    [SerializeField] private string _appStoreUrl = "https://apps.apple.com";
    private string _thisVersion;
    private string _latestVersion;
    private int? _storeFlg = null;

    // Start is called before the first frame update
    void Start()
    {
        _thisVersion = Application.version;
        StartCoroutine(GetLatestVersion());
        StartCoroutine(CompareVersion());

        // ストアを開くかどうかのダイアログを表示してからの方が良い
        StartCoroutine(GoToStore());
    }

    /// <summary>
    /// VersionURLで指定したファイルから、リリース済みのバージョン番号を取得して保存します
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetLatestVersion()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(_versionURL))
        {
            yield return req.SendWebRequest();

            switch(req.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(req.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(req.error);
                    break;
                case UnityWebRequest.Result.Success:
                    AppVersion appVersion = JsonUtility.FromJson<AppVersion>(req.downloadHandler.text);
#if UNITY_IOS
                    _latestVersion = appVersion.ios;
#elif UNITY_ANDROID
                    _latestVersion = appVersion.android;
#else
                    _latestVersion = "0.0.0";
#endif
                    break;
            }
        }
    }

    /// <summary>
    /// 実行中のアプリバージョンと、リリースされている最新のアプリバージョンを比較します
    /// </summary>
    /// <returns>1: 実行中アプリの方が新しい, 0: 同じバージョン, -1: 実行中アプリのバージョンが古いか、更新が必要</returns>
    private IEnumerator CompareVersion()
    {
        // _latestVersionが設定されるまで待機
        while (string.IsNullOrEmpty(_latestVersion)) yield return null;

        var thisVersion = _thisVersion.Split('.').Select(a => int.Parse(a));
        var latestVersion = _latestVersion.Split('.').Select(a => int.Parse(a));

        Debug.Log("ThisVersion: " + _thisVersion);
        Debug.Log("LatestVersion: " + _latestVersion);

        // バージョン表記の形式が異なれば要更新とする
        if (thisVersion.Count() != latestVersion.Count()) yield return -1;

        int cv = thisVersion.ElementAt(0) * 100 +
            thisVersion.ElementAt(1) * 10 +
            thisVersion.ElementAt(2);
        int lv = latestVersion.ElementAt(0) * 100 +
            latestVersion.ElementAt(1) * 10 +
            latestVersion.ElementAt(2);
        // Current Version > Latest Version
        if (cv > lv)
        {
            _storeFlg = 1;
        }
        // Current Version < Latest Version
        else if (cv < lv)
        {
            _storeFlg = -1;
        }
        // Current Version = Latest Version
        else
        {
            _storeFlg = 0;
        }
        yield return null;
    }

    /// <summary>
    /// バージョンチェックの結果を受けて、インストールしているアプリがリリースされているアプリより古ければ、ストアページを開く
    /// </summary>
    /// <returns></returns>
    private IEnumerator GoToStore()
    {
        while (_storeFlg == null) yield return null;

        switch (_storeFlg)
        {
            case 1:
            case 0:
                yield return null;
                break;
            // それぞれのストアに遷移
            case -1:
#if UNITY_IOS
                Application.OpenURL(_appStoreUrl);
#elif UNITY_ANDROID
                Application.OpenURL(_googlePlayStoreUrl);
#endif
                yield return null;
                break;
        }
    }
}
