using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;


public class JankenJudge : MonoBehaviour
{

    int playerHand;
    public static int enemyHand;

    public static int playerWin = 0;
    public static int enemyWin = 0;

    public static bool isDefeat = false;
    public static bool isGameOver = false;

    public static int isWin; // -1 == あいこ, 0 == 勝ち, 1 == 負け

    


    // Start is called before the first frame update
    void Start()
    {
        //UIManager.winUI.SetActive(false);
        //UIManager.loseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void Judge(int playerHand)
    {
        if(isDefeat == false && isGameOver == false)
        {

            enemyHand = Random.Range(1, 4);
            if (playerHand == enemyHand)
            {
                 isWin = -1;
                Debug.Log("あいこ！");
            }
            else if ((playerHand == 1 && enemyHand == 2) || (playerHand == 2 && enemyHand == 3) || (playerHand == 3 && enemyHand == 1))
            {
                //Debug.Log("かち！");
                playerWin++;
                isWin = 0;
                Debug.Log("ゆうしゃのかち" + playerWin);
                if(playerWin == 3)
                {
                    isDefeat = true;
                    
                }
              
            }
            else
            {
                //Debug.Log("まけ！");
                enemyWin++;
                isWin = 1;
                Debug.Log("てきのかち" + enemyWin);
                if(enemyWin == 3)
                {
                    isGameOver = true;
                }
             
            }

        }

        

    }

}
