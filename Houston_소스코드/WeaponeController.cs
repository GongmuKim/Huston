using UnityEngine;
using System.Collections;

public class WeaponeController : MonoBehaviour 
{

	public GameObject shot;
	public Transform Fireposition;
	void Start () 
	{
		InvokeRepeating ("Fire", 1, 2);
	}
	void Fire()
	{
		Instantiate (shot,Fireposition.position,Fireposition.rotation);
	}
}
