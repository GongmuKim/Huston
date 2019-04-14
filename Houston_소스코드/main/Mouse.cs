using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

    public SpriteRenderer mouse;
    public SpriteRenderer mouse_s;
    public GameObject ppp;

    // Use this for initialization
    void Start ()
    {
        // Mouse Lock
        //Cursor.lockState = CursorLockMode.Locked;
        // Cursor visible
        //Cursor.visible = false;

        mouse_s.enabled = false;
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        transform.position = pos;

        if (Input.GetButton("Fire1"))
        {
            mouse.enabled = false;
            mouse_s.enabled = true;
            //Instantiate(ppp, pos, Quaternion.identity);
        }
        else
        {

            mouse.enabled = true;
            mouse_s.enabled = false;
        }

    }
}
