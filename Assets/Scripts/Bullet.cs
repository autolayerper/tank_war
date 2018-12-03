using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed=10;
	public bool isPlayerBullet;

	public AudioClip hit;//子弹打到障碍的脚本，也放在这里了



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.up*speed*Time.deltaTime,Space.World);
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		switch(other.tag)
		{
			case "Player":
				//爆炸//生命-1//判断死亡//出生地重生
				if(!isPlayerBullet){
					//敌人的子弹，触发被杀
					other.SendMessage("Die_tank");
					Destroy(gameObject);
				}
			
				break;
			case "heart":
				other.SendMessage("GameOver");
				break;
			case "enemy":
				if(isPlayerBullet)
				{
					other.SendMessage("Die_tank");
					Destroy(gameObject);
				}
				
				break;
			case "wall":
				if(isPlayerBullet){
					AudioSource.PlayClipAtPoint(hit,transform.position);
				}
				
				Destroy(gameObject);
				Destroy(other.gameObject);
				break;
			case "wall2":
			if(isPlayerBullet){
					AudioSource.PlayClipAtPoint(hit,transform.position);
				}
				
				Destroy(gameObject);
				break;
			case "river":
				break;
			case "airwall":
				Destroy(gameObject);
				break;

		}
	}
}
