using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////////
//           指定した座標にエフェクトを発生させる              //
////////////////////////////////////////////////////////
public class OnEffect : MonoBehaviour
{

	//エフェクトオブジェクトリスト
    [SerializeField] List<GameObject> effectObjList;

    //表示させるエフェクトのポジション
    Vector3 effectPosition;

    //生成されたエフェクトオブジェクトリスト
    List<GameObject> spawnObjList;

    // Start is called before the first frame update
    void Start()
    {
    	//リスト内を定義しているか
    	if(effectObjList != null && effectObjList.Count < 0){

    		Debug.LogWarning("effectObjListにエフェクトが登録されていません");

    	}else{

    		for(int i = 0; i < effectObjList.Count;i++){

    			//要素数内にオブジェクトを定義しているか
    			if(effectObjList[i] != null){

    				//オブジェクト内にParticleSystemコンポが存在するか
	    			if(effectObjList[i].GetComponent<ParticleSystem>() == null){

    					Debug.LogError("effectObjList:ParticleSystemを持たないオブジェクトが" + i +"番目に登録されています");

    				}

    			}else{

    					Debug.LogError("effectObjList:" + i +"番目にエフェクトが登録されていません");

    			}

    		}

    	}

        spawnObjList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    	//パーティクルシステムコンポーネント 変数
    	ParticleSystem pa;

    	//ローカルリストを定義しているかつリスト内にオブジェクトが登録されている場合
    	if(spawnObjList?.Count > 0){
    		
    		for(int i = 0; i < spawnObjList.Count; i++){

    			pa = spawnObjList[i].GetComponent<ParticleSystem>();

    			//子オブジェクト共々、ParticleSystemの再生が終了したか
    			if(!pa.IsAlive(true)){

    				GameObject.Destroy(spawnObjList[i]);

    				//ローカルリストから生成エフェクトが登録されている要素を削除
    				spawnObjList.RemoveAt(i);

    			}

    		}

    	}
    }

    ////////////////////////////////////////////////////////
	//用途:引数から渡された座標位置に指定されたエフェクトを発生させる
	//引数:effectPo エフェクトを発生させる座標
	//     index    エフェクトリストの要素数
	//返値:無し
	////////////////////////////////////////////////////////
    public void SpawnEffect(Vector3 effectPo,int index){

    	if(index < effectObjList.Count){

    		GameObject spawnObj;

    		spawnObj = Instantiate(effectObjList[index],effectPo,Quaternion.identity) as GameObject;

    		//生成されたエフェクトをローカルリストに保存
    		spawnObjList.Add(spawnObj);

    	}else{

    		Debug.LogError("SpawnEffect:登録されていない要素数が呼ばれました");


    	}

    }
}
