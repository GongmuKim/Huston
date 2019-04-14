using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeMenu : MonoBehaviour
{
    float waitTime=0;

    void Start()
    {

    }

    void Update()
    {
        waitTime += Time.deltaTime;

        if(waitTime>3 && Input.GetButton("Fire1"))
        {
            Debug.Log("Change");
            SceneManager.LoadScene("gamemenu");
        }
    }
}
