using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour 
{
	public GameObject shot;
	public Transform firePosition;
    public float Target_point;
	float speed = 5;
    float move_speed = 1;
	float tilt = 5;
    bool Moveup = false;
    bool Movedown = false;

    public void shootTRY()
    {
        Instantiate(shot, firePosition.position, firePosition.rotation);
    }
	void Start () 
	{
	
	}
	void Update ()
	{

    }
}
