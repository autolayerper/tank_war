using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : MonoBehaviour {
	private SpriteRenderer  spriteRenderer;
	public GameObject explosionpre;

	public AudioClip dieaudio;

	public Sprite BrokenSprite; //老家被破坏的图片
	//控制GG游戏失败
	// Use this for initialization
	void Start () {
		spriteRenderer =GetComponent<SpriteRenderer>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GameOver(){
		spriteRenderer.sprite =BrokenSprite;
		Instantiate(explosionpre,transform.position,transform.rotation);
		PlayerManager.Instance.isDefeat= true;
		AudioSource.PlayClipAtPoint(dieaudio,transform.position);
	}
}
