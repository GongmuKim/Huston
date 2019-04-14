using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{

	public float speed = 5;
    public GameObject Parti;
    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log(coll.tag);
        if(coll.gameObject.tag == "Map2")
        {
            GameObject go = (GameObject)Instantiate(Parti,coll.gameObject.transform.position, Quaternion.identity);
            go.GetComponent<ParticleSystem>().Play();
            Destroy(coll.gameObject);
            Destroy(this.gameObject);
        }
    }
	void Start () 
	{

	}
    void Update()
    {
            transform.Translate(speed*Time.deltaTime, 0, 0);
    }
}
