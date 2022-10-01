using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

////////////////////////
//スコア数値の値を増加させる//
////////////////////////
public class Score : MonoBehaviour
{

    [SerializeField] int addScore;      //加算スコア
    
    public int nowScore = 0;            //現スコア
    int totalScore = 0;			            //総スコア

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	//現スコアが取得された総スコアより低い場合、スコアを増加させる
		  if(nowScore < totalScore){
			   nowScore += addScore;
      }

      //現スコアが取得された総スコアより高い場合、スコアを減少させる
      if(nowScore > 0 && nowScore > totalScore){
       		nowScore -= addScore;

          //0を下回った場合、強制的に0にする
          if(nowScore < 0){

            nowScore = 0;

          }
      }

    }

    ////////////////////////////////////////////////////////
	  //用途:取得したポイントを総スコアに加算する   
	  //引数:getPoint 取得したポイント
	  //返値:無し
	  ////////////////////////////////////////////////////////
    public void AddScore(int getPoint){
         
         totalScore += getPoint;

    }

    ////////////////////////////////////////////////////////
    //用途:スコアが0より上かつ減算が発生した場合
    //     取得したポイントを総スコアから減算する
    //引数:getMinusPoint 取得したポイント
    //返値:無し
    ////////////////////////////////////////////////////////
    public void MinusScore(int getMinusPoint){
         
        if(totalScore > 0){
          totalScore -= getMinusPoint;
        }

    }
}
