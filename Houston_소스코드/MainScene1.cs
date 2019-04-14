using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainScene : MonoBehaviour {
 
    public GameObject StartScene;
    private Text PressStart_BT;
    private Text UnfinishText;
    private Text TouchAnd_ST;
    private GameObject StartImage;
    private bool doingSetup;
    public GameObject End;
    void initGame()
    {
        doingSetup = true;
        StartScene = GameObject.Find("MainScene");
        PressStart_BT = GameObject.Find("MainText").GetComponent<Text>();
        UnfinishText = GameObject.Find("unfinishText").GetComponent<Text>();
        if(Input.GetButton("TochAndStart"))
        {
            new WaitForSeconds(2);
            TouchAnd_ST = GameObject.Find("TouchStart").GetComponent<Text>();
            End = GameObject.Find("End");
        }
        
    }
	void Start ()
    {

	
	}
	
	
	void Update ()
    {
	
	}
}
