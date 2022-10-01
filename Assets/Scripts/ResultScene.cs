using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Coffee.UIExtensions;

public class ResultScene : MonoBehaviour
{
    TextMeshProUGUI resultScreenYuusya;
    TextMeshProUGUI resultScreenText;

    [SerializeField] AdManager _adManager;
    private bool _interstitialDisplayed = false;

    public ShinyEffectForUGUI m_shiny;
    // Start is called before the first frame update
    void Start()
    {
        resultScreenYuusya = GameObject.Find("Yuusya").GetComponent<TextMeshProUGUI>();
        resultScreenText = GameObject.Find("ResultText").GetComponent<TextMeshProUGUI>();
        ResultScreen();

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void ResultScreen()
    {
        if (UIManager.stageNum == 11)
        {
            resultScreenYuusya.text = "でんせつのゆうしゃ";
            resultScreenText.text = "きみはついに魔王をたおし、でんせつのゆうしゃとなった。おめでとう！";
        }
        else
        {
            if (UIManager.stageNum <= 3)
            {
                resultScreenYuusya.text = "まだまだゆうしゃ";
            }
            else if (UIManager.stageNum > 3 && UIManager.stageNum <= 6)
            {
                resultScreenYuusya.text = "なかなかゆうしゃ";
            }
            else if (UIManager.stageNum > 6 && UIManager.stageNum <= 10)
            {
                resultScreenYuusya.text = "つよすぎゆうしゃ";
            }
            resultScreenText.text = "きみは" + (--UIManager.stageNum).ToString() + "かいモンスターをやっつけた！そして" + UIManager.monsterName[UIManager.monsterNameidx] + "にやぶれた・・・";
        }
    }

    public void Reset()
    {
        UIManager.stageNum = 1;
        UIManager.monsterNameidx = 0;
        JankenJudge.playerWin = 0;
        JankenJudge.enemyWin = 0;
        JankenJudge.isGameOver = false;
    }

    public void CallInterstitial()
    {
        if (!_interstitialDisplayed && _adManager.IsLoadedInterstitial)
        {
            _adManager.ShowInterstitialAds(); // 動画広告表示
            _interstitialDisplayed = true; // 動画広告表示済にする
        }
    }
}
