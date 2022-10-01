using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ファイル名を定数で管理するクラスを作成するスクリプトの例
/// </summary>
public static class CreateUnityAdsConstants{


  // コマンド名
  private const string COMMAND_NAME  = "Tools/UnityAdsSetting";

  /// <summary>
  /// ファイル名を定数で管理するクラスを作成します
  /// </summary>
  [MenuItem(COMMAND_NAME)]
  public static void Create()
  {
    if (!CanCreate())
    {
      return;
    }
    CreateScript();
  }

  /// <summary>
  /// スクリプトを作成します
  /// </summary>
  public static void CreateScript()
  {
    string[] _gameID = new string[2]; 
    _gameID = UnityConnectSettingsReference.UnityAdsGameIds();
    Dictionary<string, string> exampleDic = new Dictionary<string, string> (){
      {"Android_GameID", _gameID[1]},
      {"iOS_GameID"  , _gameID[0]},
    };

    ConstantsClassCreator.Create ("UnityAdsGameIDs", "Unity Ads GAME ID クラス", exampleDic);
  }

  /// <summary>
  /// 定数で管理するクラスを作成できるかどうかを取得します
  /// </summary>
  [MenuItem(COMMAND_NAME, true)]
  private static bool CanCreate()
  {
    return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
  }

}