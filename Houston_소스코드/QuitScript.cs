using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class QuitScript : MonoBehaviour
{
    //public Text textIsTriger;
    int textTriger = 0;
	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<Text>().enabled = true;
            if (textTriger > 0)
                Application.Quit();
            textTriger += 1;
        }
    }
}
