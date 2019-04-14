using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Mainskr: MonoBehaviour {
 
    public GameObject StartScene;
    public GameObject Touch_ST;
    private Text PressStart_BT;
    private Text UnfinishText;
    private Text TouchAnd_ST;
    private GameObject StartImage;
    private bool doingSetup;
    public GameObject End;

    /*IEnumerator initGame()
    {
        doingSetup = true;
        StartScene = GameObject.Find("MainScene");
        PressStart_BT = GameObject.Find("MainText").GetComponent<Text>();
        UnfinishText = GameObject.Find("unfinishText").GetComponent<Text>();
        yield return new WaitForSeconds(2);
        Instantiate(Touch_ST);
        TouchAnd_ST = GameObject.Find("TouchStart").GetComponent<Text>();
        if (Input.GetButton("TochAndStart"))
        {
            End = GameObject.Find("End");
        }
        
    }*/
	void Start ()
    {

	
	}
	
	
	void Update ()
    {
	
	}
}
