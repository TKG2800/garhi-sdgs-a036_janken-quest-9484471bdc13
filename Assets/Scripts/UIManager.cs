using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;

public class UIManager : MonoBehaviour
{
    [SerializeField] AdManager _adManager;
    private bool _bannerDisplayed = false;
    private bool _interstitialDisplayed = false;

    public GameObject[] jankenButton = new GameObject[3];

    Image hand;
    public Sprite rock;
    public Sprite scissors;
    public Sprite paper;

    Image result;
    public Sprite win;
    public Sprite lose;
    public Sprite draw;
    public Sprite winner;
    public Sprite loser;

    Image monster;
    RectTransform rectTransform;
    float width;
    float height;
    Animation animation;

    public Sprite[] monsters = new Sprite[10];
    int monstersidx = 0;
    public static string[] monsterName = new string[10]
        {"ミイラ男", "ガイコツ兵", "ゴブリン", "ゾンビ", "ヒドラ", "ケルベロス", "ドラゴン", "ドラゴンゾンビ", "魔人", "魔王" };
    public static int monsterNameidx = 0;

    TextMeshProUGUI playerPoint;
    TextMeshProUGUI enemyPoint;
    TextMeshProUGUI stage;
    public static int stageNum = 1;

    private bool isJudgeStart = false;

    [SerializeField] Sprite firstHalf;
    [SerializeField] Sprite secondHalf;
    [SerializeField] Image backGroundImage;

    public static bool adStart;

    

    void Start()
    {
        hand = GameObject.Find("EnemyPlan").GetComponent<Image>();
        hand.enabled = false;

        result = GameObject.Find("Result").GetComponent<Image>();
        result.enabled = false;

        monster = GameObject.Find("Monster").GetComponent<Image>();
        monster.sprite = monsters[monstersidx];
        Invoke("MonsterFadeIn", 1.0f);

        playerPoint = GameObject.Find("PlayerPoint").GetComponent<TextMeshProUGUI>();
        enemyPoint = GameObject.Find("EnemyPoint").GetComponent<TextMeshProUGUI>();
        stage = GameObject.Find("Stage").GetComponent<TextMeshProUGUI>();
        backGroundImage.sprite = firstHalf;

        rectTransform = monster.GetComponent<RectTransform>();
        width = 900f;
        height = 900f;
        rectTransform.sizeDelta = new Vector2(width, height);
        animation = monster.GetComponent<Animation>();
        animation.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!_bannerDisplayed && _adManager.IsLoadedBanner)
        {
            StartCoroutine(_adManager.ShowBannerAdsAsync()); // バナー広告表示
            _bannerDisplayed = true; // バナー広告表示済にする
        }

        playerPoint.text = JankenJudge.playerWin.ToString();
        
        enemyPoint.text = JankenJudge.enemyWin.ToString();
        
        stage.text = "ステージ" + stageNum.ToString() + "    " + monsterName[monsterNameidx];

        ButtonSwitch();

    }

    private void MonsterFadeIn()
    {
        Fade.isFadeOut = true;
    }

    public void enemyHandDisplay()
    {
        switch (JankenJudge.enemyHand)
        {
            case 1:
                hand.enabled = true;
                hand.sprite = rock;
                break;
            case 2:
                hand.enabled = true;
                hand.sprite = scissors;
                break;
            case 3:
                hand.enabled = true;
                hand.sprite = paper;
                break;
        }
    }

    public void ResultDisplay()
    {
        StartCoroutine("ShowResult");
    }

    public IEnumerator ShowResult()
    {
        isJudgeStart = true;
        if (JankenJudge.isWin == -1) // あいこ
        {
            result.enabled = true;
            result.sprite = draw;
            yield return new WaitForSeconds(1.0f);
            result.enabled = false;
        }

        else if(JankenJudge.isWin == 0) // 勝ち
        {
            result.enabled = true;
            result.sprite = win;
            yield return new WaitForSeconds(1.0f);
            result.enabled = false;

            if (JankenJudge.isDefeat) 
            {
                result.enabled = true;
                result.sprite = winner;
                Fade.isFadeIn = true;
                
                yield return new WaitForSeconds(2.0f);
                
                result.enabled = false;
                
                JankenJudge.isDefeat = false;
                stageNum++;

                if(stageNum == 11)
                {
                    SceneChanger.CallSceneChange();
                }

                else
                {
                    if (stageNum == 6)
                    {
                        backGroundImage.sprite = secondHalf;
                    }

                    if (stageNum == 3 || stageNum == 6 || stageNum == 8)
                    {
                        CallInterstitial();

                    }

                    if(adStart == false)
                    {
                        monsterNameidx++;
                        monster.sprite = monsters[monstersidx += 1];
                        if (stageNum == 10)
                        {
                            animation.enabled = true;
                        }
                        Fade.isFadeOut = true;
                        JankenJudge.playerWin = 0;
                        JankenJudge.enemyWin = 0;
                    }
                    
                }
                

            }
        }
        else if(JankenJudge.isWin == 1) // 負け
        {
            result.enabled = true;
            result.sprite = lose;
            yield return new WaitForSeconds(1.0f);
            result.enabled = false;
            
            if(JankenJudge.isGameOver)
            {
                result.enabled = true;
                result.sprite = loser;
                yield return new WaitForSeconds(2.0f);
                SceneChanger.CallSceneChange();
            }
        }
        isJudgeStart = false;
    }

    private void ButtonSwitch()
    {
        int i;
        if(isJudgeStart)
        {
            for (i = 0; i < 3; i++)
            {
               jankenButton[i].GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            for (i = 0; i < 3; i++)
            {
                jankenButton[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    private void CallInterstitial()
    {
        if (!_interstitialDisplayed && _adManager.IsLoadedInterstitial)
        {
            _adManager.ShowInterstitialAds(); // 動画広告表示
            //_interstitialDisplayed = true; // 動画広告表示済にする
        }
    }

    public IEnumerator LastMonsterDisplay()
    {
        
        width = 1600f;
        height = 1179f;
        rectTransform.sizeDelta = new Vector2(width, height);
        yield return new WaitForSeconds(2.0f);
        animation.enabled = true;
    }


}
