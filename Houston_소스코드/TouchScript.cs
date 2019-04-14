using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchScript : MonoBehaviour {

    public bool isTextPop = true;
    IEnumerator Inappear()
    {
       yield return new WaitForSeconds(3.0f);
       GetComponent<Text>().enabled = true;
    }
    void Start ()
    {

    }
	
	void Update ()
    {
        if(isTextPop = true)
            StartCoroutine(Inappear());

    }
}
