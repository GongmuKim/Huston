using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menumain : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainFrontScene");
        }
    }
    public void go_main(int h)
    {
        PlayerPrefs.SetInt("h_scene", h);
        PlayerPrefs.Save();
        SceneManager.LoadScene("main");
    }
}
