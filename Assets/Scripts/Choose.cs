using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//选择是单人模式还是双人模式
public class Choose : MonoBehaviour {

	// Use this for initialization
	private int choice =1;
	public Transform pos1;
	public Transform pos2;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W))
		 {
			 choice =1;
			 transform.position = pos1.position;
		}
		else if(Input .GetKeyDown(KeyCode.S))
		{
			choice =2;
			transform.position =pos2.position;
		}
		if(choice==1 && Input.GetKeyDown(KeyCode.Space))
		{
			SceneManager.LoadScene(1);
		}
	}
}
