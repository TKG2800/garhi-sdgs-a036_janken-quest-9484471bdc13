using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

////////////////////////
//    シーン切り替え      //
////////////////////////
public class SceneChanger : MonoBehaviour
{


    public static string pSceneName = "Game";	//移動先のシーン名
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    ////////////////////////////////////////////////////////
	//用途:パブリック変数で指定したシーン名へシーン移動を行う
	//引数:無し
	//返値:無し
	////////////////////////////////////////////////////////
    public static void SceneChange(){

    	SceneManager.LoadScene(pSceneName);

    }

    ////////////////////////////////////////////////////////
	//用途:引数で指定したシーン名へシーン移動を行う
	//引数:sceneName シーン名
	//返値:無し
	////////////////////////////////////////////////////////
    public static void CallSceneChange(){

    	SceneManager.LoadScene("Result");

    }

    public void push_btn_end()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#elif UNITY_ANDROID
        Application.runInBackground = false;
        Application.Quit();
#elif UNITY_IPHONE
        Application.Quit();
#endif
    }

    public void ShowHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");

    }

    
}
