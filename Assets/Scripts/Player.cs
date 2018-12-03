using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	//属性值
	public float speed;
	private Vector3 bulletEulereagel;//要欧拉角和四元数转换
	public float wait_time =0.4f;//间隔时间 cd
	private float defendtime =3.0f;//无敌时间
	private bool isdefend =true;	//是否无敌//出生时无敌
	
	//引用
	private SpriteRenderer sr;
	public Sprite[] tanksprite;
	public GameObject bullet;
	public GameObject defendpre;//无敌特效

	public GameObject explosionPre;//爆炸预制体

	public AudioSource moveaudio; //拿到音效组件
	public AudioClip[] moveaudioclip; //拿到音效素材
	// Use this for initialization
	void Start () {
		sr =GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private void FixedUpdate() {
		if(PlayerManager.Instance.isDefeat){
			return;
		}
		Move();
		//是否无敌
		Isdefend();
		//能否发动攻击
		IsCD();
	
	}
	//坦克攻击
	private void Attack(){
		if(Input.GetKey(KeyCode.Space)){
			//按下空格键
			Instantiate(bullet,transform.position,Quaternion.Euler(bulletEulereagel));
			wait_time =0;
		}
	}
	private void Move(){
		//获得了移动输入
		//而且移动音效也在这里除了
			float h =Input.GetAxisRaw("Horizontal");
		transform.Translate(Vector3.right*h*speed*Time.fixedDeltaTime,Space.World);
		if(h>0){
			sr.sprite = tanksprite[1];
			bulletEulereagel =new Vector3(0,0,-90);
		}
		else if(h<0){
			sr.sprite =tanksprite[3];
			bulletEulereagel =new Vector3(0,0,90);
		}
		if(Mathf.Abs(h)>0.05){
			moveaudio.clip =moveaudioclip[1];
			
			if(moveaudio.isPlaying){
				moveaudio.Play();
			}
		}
		if(h!=0){
			return ;
		}
		float v =Input.GetAxisRaw("Vertical");
		transform.Translate(Vector3.up*v*speed*Time.fixedDeltaTime,Space.World);
		if(v>0){
			sr.sprite = tanksprite[0];
			bulletEulereagel =new Vector3(0,0,0);
		}
		else if(v<0){
			sr.sprite =tanksprite[2];
			bulletEulereagel =new Vector3(0,0,-180);
		}
		if(Mathf.Abs(v)>0.05){
			moveaudio.clip =moveaudioclip[1];
			
			if(moveaudio.isPlaying){
				moveaudio.Play();
			}
		}
		else
		{
			moveaudio.clip =moveaudioclip[0];
			
			if(moveaudio.isPlaying){
				moveaudio.Play();
			}
		}
	}

	private void Die_tank()
	{	
		//无敌
		if(isdefend){
			return ;
		}
		//爆炸
		Instantiate(explosionPre,transform.position,transform.rotation);
		//死亡
		Destroy(gameObject);
		//生命-1
		PlayerManager.Instance.isDead=true;
		//判断死亡//出生地重生
	}
	private void Isdefend(){
		//判断是否处于无敌状态//在 update中调用
		if(isdefend){
			defendpre.SetActive(true);
			defendtime -=Time.deltaTime;
			if(defendtime<=0){
				isdefend=false;
				defendpre.SetActive(false);
			}
		}
	}
	private void IsCD(){
		//判断CD时间是否已经过了//在fixedupdate中调用
		if(wait_time>=0.4){
			Attack();
		}
		else{
			wait_time+=Time.deltaTime;
		}
	}
}
