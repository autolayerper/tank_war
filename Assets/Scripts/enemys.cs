using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemys : MonoBehaviour {
	//属性值
	public float speed;
	private Vector3 bulletEulereagel;//要欧拉角和四元数转换
	private float v=-1;
	private float h;

	//引用
	private SpriteRenderer sr;
	public Sprite[] tanksprite;
	public GameObject bullet;
	public GameObject explosionPre;//爆炸预制体

	//计时器
	private float wait_time=1 ;
	private float time_turn=3.5f;//敌人自动转向的计时器

	void Start () {
		sr =GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
		//能否发动攻击攻击的时间间隔
		IsCD();
	}
	private void FixedUpdate() {
		Move();
	
	}
	//坦克攻击
	private void Attack(){
			Instantiate(bullet,transform.position,Quaternion.Euler(bulletEulereagel));
			wait_time =0;
	}
	private void Move(){
		//管理v.h的值
		
		if(time_turn >4)
		{
			int num =Random.Range(0,8); //多给向下赋值
			if(num>5){
				v =-1;
				h=0;
			}
			else if(num==0){
				v = 1;
				h = 0;
			}
			else if(num==2||num==3)
			{
				 h =-1;
				 v =0;
			}
			else if(num==4||num==5){
				h=1;
				v=0;
			}
			time_turn =0;
		}
		else {
			time_turn+=Time.fixedDeltaTime;
		}
		//
		transform.Translate(Vector3.right*h*speed*Time.fixedDeltaTime,Space.World);
		if(h>0){
			sr.sprite = tanksprite[1];
			bulletEulereagel =new Vector3(0,0,-90);
		}
		else if(h<0){
			sr.sprite =tanksprite[3];
			bulletEulereagel =new Vector3(0,0,90);
		}
		if(h!=0){
			return ;
		}
		transform.Translate(Vector3.up*v*speed*Time.fixedDeltaTime,Space.World);
		if(v>0){
			sr.sprite = tanksprite[0];
			bulletEulereagel =new Vector3(0,0,0);
		}
		else if(v<0){
			sr.sprite =tanksprite[2];
			bulletEulereagel =new Vector3(0,0,-180);
		}
	}

	private void Die_tank()
	{	
		
		//爆炸
		Instantiate(explosionPre,transform.position,transform.rotation);
		//死亡
		Destroy(gameObject);
		//分数++
		PlayerManager.Instance.Playerscore+=100;
		//判断死亡//出生地重生
	}

	private void IsCD(){
		//判断CD时间是否已经过了//在fixedupdate中调用
		if(wait_time>=3){
			Attack();
		}
		else{
			wait_time+=Time.deltaTime;
		}
	}
	//如果自己碰到，立即转向，不要跟傻子一样
	private void OnCollisionEnter2D(Collision2D other)
	 {	//
		if(other.gameObject.tag=="enemy"){
			time_turn =4;
		}
	}
}
