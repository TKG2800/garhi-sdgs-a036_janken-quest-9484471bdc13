using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

////////////////////////////////////////////////////////
//Imageコンポーネントのアルファ値を操作してフェードイン/フェードアウトを行う//
////////////////////////////////////////////////////////
public class Fade : MonoBehaviour
{

	//アルファ値 定数
	const int ALPHA = 1;

	//イメージ画像のRGB値 構造体
	struct _COLOR{

    	public float R;
    	public float G;
    	public float B;

	}

    //フェードさせるオブジェクト
    [SerializeField] GameObject fadeObj;

	//addAlpha : アルファの増加値
	//takeAlpha: アルファの減少値
    [SerializeField, Range(0f, 1.0f)] float addAlpha;
	[SerializeField, Range(0f, 1.0f)] float takeAlpha;
    
    //isFadeIn  : フェードイン開始フラグ
    //isFadeOut : フェードアウト開始フラグ
	public static bool isFadeIn;
	public static bool isFadeOut;

	//初期アルファ値格納変数
	float alphaValue;
	
	//Imageコンポーネント
	Image fadeImage;

	//イメージ画像のRGB値構造体 変数
	_COLOR fadeRGB;

    // Start is called before the first frame update
    void Start()
    {
    	//Imageコンポーネント取得
        fadeImage = fadeObj.GetComponent<Image>();

        //ImageコンポーネントのRGBA値を取得
        fadeRGB.R = fadeImage.color.r;
        fadeRGB.G = fadeImage.color.g;
        fadeRGB.B = fadeImage.color.b;
        alphaValue = fadeImage.color.a;

    }

    // Update is called once per frame
    void Update()
    {
        
        //フェードイン呼び出し
    	if(isFadeIn && !isFadeOut){

    		OnFadeIn();

			//フェードインとフェードアウトが同時に行われないよう
			//強制的にFadeOutのフラグをOFF
    		isFadeOut = false;

    	}


    	//フェードアウト呼び出し
    	if(isFadeOut && !isFadeIn){

    		OnFadeOut();

			//フェードインとフェードアウトが同時に行われないよう
			//強制的にFadeInのフラグをOFF
    		isFadeIn = false;

    	}

    	//フラグエラー用
    	if(isFadeOut && isFadeIn){

    		Debug.LogError("フェードイン/フェードアウト両方のフラグがONになっています");

    	}


    }

	////////////////////////////////////////////////////////
	//用途:アルファ値を減算した後
	//     Imageコンポーネント内のColor項目に、新たにRGBA値を代入する
	//引数:無し
	//返値:無し
	////////////////////////////////////////////////////////
    void OnFadeIn(){

    	//初期アルファ値が0以下の状態
    	if(alphaValue < 0){

    		Debug.LogWarning("Colorのアルファ値が0の状態でフェードインを行っています");

    	}

    	alphaValue -= takeAlpha;

    	fadeImage.color = new Color(fadeRGB.R,fadeRGB.G,fadeRGB.B,alphaValue);

    	if(alphaValue <= 0){

    		isFadeOut = false;
            isFadeIn = false;

    	}

    }

	////////////////////////////////////////////////////////
	//用途:アルファ値を加算した後
	//     Imageコンポーネント内のColor項目に、新たにRGBA値を代入する
	//引数:無し
	//返値:無し
    ////////////////////////////////////////////////////////
    void OnFadeOut(){

    	//初期アルファ値が1(255)以上の状態
    	if(alphaValue >= ALPHA){

    		Debug.LogWarning("Colorのアルファ値が255の状態でフェードアウトを行っています");

    	}

    	alphaValue += addAlpha;

    	fadeImage.color = new Color(fadeRGB.R,fadeRGB.G,fadeRGB.B,alphaValue);

    	if(alphaValue >= ALPHA){

    		isFadeIn = false;
            isFadeOut = false;

    	}

    }
}
