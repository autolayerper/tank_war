using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
	//装初始化地图所需物体的数组
	//0老家/1墙/2障碍/3出生效果/4河流/5草/6空气墙
	public GameObject[] item;
	private List<Vector3> itemPosition =new List<Vector3>();
	// Use this for initialization
	void Awake()
	{	
		//老家
		CreateHome();
		CreateAirwall();
		CreateBorn();
		CreateMap();//其他的地图随机形成
		InvokeRepeating("CreateEnemy",4,5);
		//延时调用

	}
	
	//创建物体
	private void CreateItem(GameObject temp,Vector3 pos,Quaternion qua ){
		GameObject temp2 =Instantiate(temp,pos,qua) as GameObject;
		temp2.transform.SetParent(gameObject.transform);
		itemPosition.Add(pos);
	}

	//初始化老家
	private void CreateHome(){
		CreateItem(item[0],new Vector3(0,-8,0),Quaternion.identity);

		CreateItem(item[1],new Vector3(-1,-8,0),Quaternion.identity);
		CreateItem(item[1],new Vector3(1,-8,0),Quaternion.identity);
		for(int i=-1;i<2;i++){
			CreateItem(item[1],new Vector3(i,-7,0),Quaternion.identity);
		}
	}
	
	//实例化外墙空气墙
	private void CreateAirwall()
	{
		for(int i=-9;i<10;i++){
			CreateItem(item[6],new Vector3(11,i,0),Quaternion.identity);
			CreateItem(item[6],new Vector3(-11,i,0),Quaternion.identity);
		}
		for(int i=-10;i<11;i++){
			CreateItem(item[6],new Vector3(i,-9,0),Quaternion.identity);
			CreateItem(item[6],new Vector3(i,9,0),Quaternion.identity);
		}
	}
	//产生随机位置
	private Vector3 RandomPos(){
		//x=+-10,y=+-8 空出
		//x=+-11,y=+-9空气墙
		while(true){
			Vector3 createposition =new Vector3(Random.Range(-9,10),Random.Range(-7,8),0);
			if(!iscreate(createposition))
			{
				return createposition;
			}
			
		}
	}

	//判断该位置是否已经初始化
	private bool iscreate(Vector3 temp3){
		//判断该位置是否已经初始化
		for(int i=0;i<itemPosition.Count;i++){
			if(itemPosition[i]==temp3){
				return true;
			}
		}
		return false;
	}
	private void CreateMap(){
		for(int i=0;i<50;i++){
			CreateItem(item[1],RandomPos(),Quaternion.identity);
		}
		for(int i=0;i<25;i++){
			CreateItem(item[2],RandomPos(),Quaternion.identity);
		}
		for(int i=0;i<20;i++){
			CreateItem(item[4],RandomPos(),Quaternion.identity);
		}
		for(int i=0;i<50;i++){
			CreateItem(item[5],RandomPos(),Quaternion.identity);
		}
	}
	//初始化玩家和三个敌人
	private void CreateBorn(){
		GameObject go =Instantiate(item[3],new Vector3(-2,-8,0),Quaternion.identity);
		
		go.GetComponent<borns>().createPlayer =true;

		CreateItem(item[3],new Vector3(-10,8,0),Quaternion.identity);
		CreateItem(item[3],new Vector3(0,8,0),Quaternion.identity);
		CreateItem(item[3],new Vector3(10,8,0),Quaternion.identity);
	}

	//随机的方法产生敌人
	private void CreateEnemy(){
		int num = Random.Range(0,3);
		switch (num){
			case 0:
				CreateItem(item[3],new Vector3(-10,8,0),Quaternion.identity);
				break;
			case 1:
				CreateItem(item[3],new Vector3(0,8,0),Quaternion.identity);
				break;
			default:
				CreateItem(item[3],new Vector3(10,8,0),Quaternion.identity);
				break;
		}
	}
}
