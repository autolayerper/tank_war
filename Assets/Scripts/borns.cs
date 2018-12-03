using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class borns : MonoBehaviour {
	public GameObject Playerpre;
	public GameObject[] enemypre;
	//敌人数组
	public bool createPlayer;//判断是玩家还是敌人
	// Use this for initialization
	void Start () {
		Invoke("BornTank",1f);//延时调用
		Destroy(gameObject,1f);//延时销毁
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void BornTank(){
		if(createPlayer){
			Instantiate(Playerpre,transform.position,transform.rotation);
		}
		else 
		{
			int num =Random.Range(0,3);
			Instantiate(enemypre[num],transform.position,transform.rotation);
		
		}

	}
}
