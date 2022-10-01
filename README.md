## 使い方
1. 画面右上の「・・・」から「Fork this repository」を選択してください
2. Workspaceが「GarhiSDGs」、プロジェクトが「SDGs」になっていることを確認したら、名前の欄にリポジトリ名を入れてください
	- リポジトリ名は a'プロジェクト番号3桁'_'ゲーム名を半角英数字(ハイフンで繋げる)'
3. リポジトリをフォークボタンを押下してください
4. フォークが完了したら、クローンしてください
5. Unity v2021.3.6f1でクローンしたフォルダを開いたら、以下のチェックをしてください
	- Assets/Scenes/SampleScene.unityを開く
	- ウィンドウ->一般->デバイスシミュレーターを開く
	- ヒエラルキー内のSampleScene/Canvas/Panel/Text (TMP)でテキストを編集し、漢字以外の日本語が表示されることを確認する

---
## フォルダ構成
- Plugins
	- iOS - iOSで使用する広告関係プラグイン
- Resources
	- AppIcons - アプリのアイコンを入れるフォルダ（なければ作成してください）
	- Images - ゲームに使用する画像を入れるフォルダ（なければ作成してください）
- Scenes - シーンファイルを入れるフォルダ
- Scripts - スクリプトファイルを入れるフォルダ
- TextMesh Pro - TextMesh Proのフォルダ

---

## プラグイン

- Google Play Store アプリケーションバージョンチェック用パッケージ
  - [GitHub](https://github.com/google/play-unity-plugins/releases)
- 広告用Xcode用フレームワーク
  - トラッキング許可のダイアログを表示させること
  - 起動時の画面に以下のコードを追記

```C#
using System.Runtime.InteropServices;

public class XXXX: MonoBehaviour
{

#if UNITY_IOS
  [DllImport("__Internal")]
  private static extern void _requestIDFA();
#endif
  
  void Start()
	{
#if UNITY_IOS
  	_requestIDFA();
#endif
	}
}
```

