using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

////////////////////////////////////////////////////////
//         　  Image画像をゲージに見立て					  //
//			　 画像の長さの増減を変更する                  //
////////////////////////////////////////////////////////
public class Gauge : MonoBehaviour
{

	const float MAXFILL = 1.0f;								//ゲージの最大値
	const float EMPFILL = 0.0f;								//ゲージの最小値

	[SerializeField] GameObject gaugeObj;					//ゲージオブジェクト

	[SerializeField, Range(0f, 1.0f)] float totalValue;		//総ゲージ量
	[SerializeField] float totalTime;   		            //総タイム
    
	public bool isAddGauge;									//ゲージ増加開始フラグ
	public bool isTakeGauge;								//ゲージ減少開始フラグ

	int isGaugeFlg = 0;										//前回のフラグの状態
															//0:どちらもfalse
															//1:isAddGaugeがtrue
															//2:isTakeGaugeがtrue

	float timeSpeed;										//ゲージの加減速

	Image gaugeImage; 										//イメージコンポ

    // Start is called before the first frame update
    void Start()
    {
     	   gaugeImage = gaugeObj.GetComponent<Image>();

     	   //指定されたゲージの長さを代入
     	   gaugeImage.fillAmount = totalValue;

     	   //ゲージの増減スピードを算出
     	   timeSpeed = totalValue / totalTime;

     	   //初回のみフラグチェック
     	   if(isAddGauge && isTakeGauge){

     	   		Debug.LogError("isAddGauge・isTakeGaugeともにフラグがtrueになっています");

     	   }

     	   //Imageコンポーネントがアタッチされたゲームオブジェクトかどうか
     	   /*if(gaugeObj.GetComponent<Image>() == null){

    			Debug.LogError("gaugeObj:Imageコンポーネントがアタッチされていないオブジェクトが登録されています");

    		}*/
    }

    // Update is called once per frame
    void Update()
    {

    	//増減ともにフラグがtrueの場合
    	if(isAddGauge && isTakeGauge){

    		//前回、減少フラグがtrueだったの場合
    		if(isGaugeFlg == 1){

    			isAddGauge = false;

    		}else if(isGaugeFlg == 2){

    			isTakeGauge = false;

    		}

    	}

    	//増加フラグがtrueの場合
    	if(isAddGauge){

    		isGaugeFlg = 1;

    	}else if(isTakeGauge){

    		isGaugeFlg = 2;

    	}

    	//ゲージ減少
        if(isTakeGauge && !isAddGauge){

        	GaugeTakeFill();

        }

    	//ゲージ増加
        if(isAddGauge && !isTakeGauge){

        	GaugeAddFill();

        }

    }

    /////////////////////////////////////////////////////////////////
	//用途:1秒ごとのゲージ増加量を総ゲージ量に加算し、ImageコンポのFillAmountに代入
	//引数:無し
	//返値:無し
	/////////////////////////////////////////////////////////////////
    void GaugeAddFill(){

    	totalValue += timeSpeed * Time.deltaTime;

    	gaugeImage.fillAmount = totalValue;

    	//FillAmountが最大の場合、フラグをすべてOFF
        if(gaugeImage.fillAmount >= MAXFILL){

        	isAddGauge = false;
        	isTakeGauge = false;
        	totalValue = MAXFILL;

        }

    }

    //////////////////////////////////////////////////////////////////
	//用途:1秒ごとのゲージ減少量を総ゲージ量から減算し、ImageコンポのFillAmountに代入
	//引数:無し
	//返値:無し
	//////////////////////////////////////////////////////////////////
    void GaugeTakeFill(){

    	totalValue -= timeSpeed * Time.deltaTime;

    	gaugeImage.fillAmount = totalValue;

    	//FillAmountが最小の場合、フラグをすべてOFF
        if(gaugeImage.fillAmount <= EMPFILL){

    		isAddGauge = false;
    		isTakeGauge = false;
    		totalValue = EMPFILL;

    	}

    }

    //////////////////////////////////////////////////////////////////
	//用途:現在の秒数を計算し、返値として引き渡します
	//引数:無し
	//返値:displayTime 現在の秒数
	//////////////////////////////////////////////////////////////////
    public float OnDisplayTime(){

    	float displayTime;

    	displayTime = totalValue * totalTime;

    	return displayTime;

    }
}
