using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour {
	//爆炸特效
	public float times=0.167f;
	void Start () {
		Destroy(gameObject,times);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
