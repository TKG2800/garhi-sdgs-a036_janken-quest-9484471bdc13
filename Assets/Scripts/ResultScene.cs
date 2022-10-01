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
            resultScreenYuusya.text = "�ł񂹂̂䂤����";
            resultScreenText.text = "���݂͂��ɖ������������A�ł񂹂̂䂤����ƂȂ����B���߂łƂ��I";
        }
        else
        {
            if (UIManager.stageNum <= 3)
            {
                resultScreenYuusya.text = "�܂��܂��䂤����";
            }
            else if (UIManager.stageNum > 3 && UIManager.stageNum <= 6)
            {
                resultScreenYuusya.text = "�Ȃ��Ȃ��䂤����";
            }
            else if (UIManager.stageNum > 6 && UIManager.stageNum <= 10)
            {
                resultScreenYuusya.text = "�悷���䂤����";
            }
            resultScreenText.text = "���݂�" + (--UIManager.stageNum).ToString() + "���������X�^�[����������I������" + UIManager.monsterName[UIManager.monsterNameidx] + "�ɂ�Ԃꂽ�E�E�E";
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
            _adManager.ShowInterstitialAds(); // ����L���\��
            _interstitialDisplayed = true; // ����L���\���ςɂ���
        }
    }
}
