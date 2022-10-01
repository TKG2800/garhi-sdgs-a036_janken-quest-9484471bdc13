# 元気玉アプリ チェックリスト



## 基本事項

- 最新の[Template](https://bitbucket.org/garhi-sdgs/template/src/master/)をフォークしてリポジトリを作成した
- Unity Editorバージョンは **2021.3.6f1** である
- スクリプトファイルの文字コードを**UTF-8**で保存してある
- ゲームの動作に不要なファイル・パッケージは削除済みである
  - TextMesh Proを使用していない場合は削除してもよい
  - 基本的に *Assets/Plugins* 以下には *iOS* フォルダしか存在しない
- 各ファイルが適切な位置にある（任意）
  - *Assets/Scenes* 以下には *xxxx.unity* ファイルのみ
  - *Assets/Scripts* 以下には *xxxx.cs* ファイルのみ
- 通常実行時にエラーが発生しない



## 広告設定

- ゲーム中にバナー広告と動画広告が表示される

- プロジェクト設定 > サービス > Ads

  - Adsがオンである
  - Ads PackageのCurrent VersionとLatest Version Availableが同じである
  - Test modeのチェックがオフである
    - テストする際はオンにしてください
  - Game IdのAndroid、iOSに値が入っている

- 以下の場所にファイルが存在し、編集していない

  - Assets/Editor/ConstantsClassCreator.cs
  - Assets/Editor/CreateUnityAdsConstants.cs
  - Assets/Editor/UnityConnectSettingsReference.cs
  - Assets/Editor/XcodeBuild.cs
  - Assets/Plugins/iOS/AdSupport.framework/*
  - Assets/Plugins/iOS/AppTrackingTransparency.framework/*
  - Assets/Plugins/iOS/IDFADialog.mm
  - Assets/Prefabs/UnityAdsManager.prefab
  - Assets/Scripts/UnityAdsGameIDs.cs (ツール > UnityAdsSettingより自動生成)
  - Assets/Scripts/AdManager.cs
  - Assets/YamlDotNet/*

- 最初に呼ばれるスクリプト内に以下の記述がある

  ```c#
  using System.Runtime.InteropServices;
  
  public class XXXXX : MonoBehaviour
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
    	
    	void Start()
  		{
  				Invoke("DelayIDFA", 1);
      }
  }
  ```

  

## リリース直前確認事項

- Android, iOS両方のプラットフォームで実行してもエラーが発生しない
- Android, iOS両方のプラットフォームでビルドしてもエラーが発生しない
- プロジェクト設定 > プレイヤーの項目
  - 正確なプロダクト名が記入してある
  - アプリの向きを指定する（解像度と表示 > 向き > デフォルトの向き）<br>
  ※例：横向きのゲームなら「横向き(右)」に設定されている
  - バージョンが **1.0.0** である（適宜変更）
  - ビルド番号が **0** である（適宜変更）
  - アプリのアイコンを可能な限り設定してある
    - デフォルトのアイコン
    - Androidの設定 > アイコン
    - iOSの設定 > アイコン
  - パッケージ名・バンドル名を設定してある
    - Android: その他の設定 > 識別 > パッケージ名
    - iOS: その他の設定 > 識別 > バンドル識別子
  - Androidビルド時に必要な設定
    - その他の設定 > 識別 > ターゲットAPIレベル（Automatic(highest installed)になっている）
    - その他の設定 > スクリプティングバックエンド（Il2CPPに設定されている）
    - その他の設定 > ターゲットアーキテクチャ（ARMv7とARM64にチェック、x86(Chrome OS)とx86-64(Chrome OS)はチェックが外れている）
    - 公開設定 > カスタムキーストア (チェックを入れる)
      - 選択からキーストアファイルを選択<br>
      [**ここからキーストアファイルをダウンロードしてください**](https://drive.google.com/file/d/1p56WvSfQdkMEB1EDDrAJhWAnEQ4MI4xu/view?usp=sharing)
        - パスワードは「%G4!KZNmvQKV」
          - プロジェクトキーのエイリアスの選択から「元気玉！sdgs～」を選択
            - パスワードは「%G4!KZNmvQKV」
  - 署名チームIDを設定してある ** iOSのみ*
    - iOS: その他の設定 > 識別 > 署名チームID
      - 7VGHRZMRSU
  - 自動的に署名のチェックがオンである ** iOSのみ*
    - iOS: その他の設定 > 識別 > 自動的に署名

