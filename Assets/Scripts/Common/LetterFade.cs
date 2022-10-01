using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//////////////////////////////////////////////
//アタッチされている文字のフェードイン・フェードアウトを繰り返す//
//////////////////////////////////////////////
public class LetterFade : MonoBehaviour
{
	//アルファ値 定数
	const int LALPHA = 1;

	//文字のRGB値 構造体
	struct _LCOLOR{

    	public float R;
    	public float G;
    	public float B;

	}

	//le_addAlpha : アルファの増加値
	//le_takeAlpha: アルファの減少値
    [SerializeField, Range(0f, 1.0f)] float le_addAlpha;
	[SerializeField, Range(0f, 1.0f)] float le_takeAlpha;
    
    //le_isFadeIn  : フェードイン開始フラグ
    //le_isFadeOut : フェードアウト開始フラグ
	bool le_isFadeIn = true;
	bool le_isFadeOut = false;

	//初期アルファ値格納変数
	float le_alphaValue;
	
	//TextMeshProコンポーネント
	TextMeshProUGUI fadeLetter;

	//文字のRGB値構造体 変数
	_LCOLOR le_fadeRGB;

    // Start is called before the first frame update
    void Start()
    {
        //TextMeshProコンポーネント取得
        fadeLetter = this.GetComponent<TextMeshProUGUI>();

        //ImageコンポーネントのRGBA値を取得
        le_fadeRGB.R = fadeLetter.color.r;
        le_fadeRGB.G = fadeLetter.color.g;
        le_fadeRGB.B = fadeLetter.color.b;
        le_alphaValue = fadeLetter.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        //フェードイン呼び出し
    	if(le_isFadeIn && !le_isFadeOut){

    		LOnFadeIn();

			if(le_alphaValue <= 0){

    			le_isFadeOut = true;
            	le_isFadeIn = false;

    		}

    	}


    	//フェードアウト呼び出し
    	if(le_isFadeOut && !le_isFadeIn){

    		LOnFadeOut();

			if(le_alphaValue >= LALPHA){

    			le_isFadeIn = true;
            	le_isFadeOut = false;

    		}

    	}
    }

    ////////////////////////////////////////////////////////
	//用途:アルファ値を減算した後
	//     TextMeshProコンポーネント内のColor項目に、新たにRGBA値を代入する
	//引数:無し
	//返値:無し
	////////////////////////////////////////////////////////
    public void LOnFadeIn(){

    	//初期アルファ値が0以下の状態
    	if(le_alphaValue < 0){

    		Debug.LogWarning("Colorのアルファ値が0の状態でフェードインを行っています");

    	}

    	le_alphaValue -= le_takeAlpha;

    	fadeLetter.color = new Color(le_fadeRGB.R,le_fadeRGB.G,le_fadeRGB.B,le_alphaValue);

    }

	////////////////////////////////////////////////////////
	//用途:アルファ値を加算した後
	//     TextMeshProコンポーネント内のColor項目に、新たにRGBA値を代入する
	//引数:無し
	//返値:無し
    ////////////////////////////////////////////////////////
    void LOnFadeOut(){

    	//初期アルファ値が1(255)以上の状態
    	if(le_alphaValue >= LALPHA){

    		Debug.LogWarning("Colorのアルファ値が255の状態でフェードアウトを行っています");

    	}

    	le_alphaValue += le_addAlpha;

    	fadeLetter.color = new Color(le_fadeRGB.R,le_fadeRGB.G,le_fadeRGB.B,le_alphaValue);

    }

}
