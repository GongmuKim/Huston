using UnityEngine;
using System.Collections;

public class Shoot_Triger : MonoBehaviour 
{
	public GameObject shot;
	public Transform firePosition;
	float speed = 5;
	float tilt = 5;

	void Start () 
	{
	
	}
	void Update ()
	{
		/*if(Input.GetButtonDown("Fire1") == true)
		{
			Instantiate(shot,firePosition.position,firePosition.rotation);
		}*/
	}
    public void Firerate()
    {
        Instantiate(shot, firePosition.position, firePosition.rotation);
        ControllPlayer.GetFire = true;
    }
}
