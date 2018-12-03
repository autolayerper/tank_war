using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour {

	//属性值
	public int LifeValue=3;
	public int Playerscore=0;
    public bool isDead;//判断是否死亡
    public bool isDefeat;

    //引用
    public GameObject born;
    public Text PlayerScoretext;
    public Text Playerlifetext; 
    public GameObject GameoverUI;
	//单例模式
	private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    // Use this for initialization
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Instance =this;
        
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(isDefeat){
            GameoverUI.SetActive(true);

            Invoke("ReturnToMainMenu",3);//3秒后返回主菜单

            return;
        }
		if(isDead)
        {
            Recover();
        }
        PlayerScoretext.text=Playerscore.ToString();
        Playerlifetext.text =LifeValue.ToString();
	}

    private void Recover()
    {
        if(LifeValue<=0)
        {
            isDead =true;

            Invoke("ReturnToMainMenu",3);//3秒后返回主菜单
        }
        else
        {
            LifeValue--;
            GameObject go = Instantiate(born,new Vector3(-2,-8,0),Quaternion.identity);
            go.GetComponent<borns>().createPlayer =true;
            isDead =false;
        }
    }
    private void ReturnToMainMenu()
    {
        //重新 调用 开始面板
        SceneManager.LoadScene(0);
    }
}
