using UnityEngine;
using System.Collections;

public class Switch_Gizmos : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0.5f, 0.5f, 1);
        Vector3 LU = transform.position + new Vector3(0, 5, 0);
        Vector3 LD = transform.position + new Vector3(0, -5, 0);
        Vector3 RU = transform.position + new Vector3(14.364f, 5, 0);
        Vector3 RD = transform.position + new Vector3(14.364f, -5, 0);

        //340.8889
        Vector3 LLU = transform.position + new Vector3(-3.408889f, 5, 0);
        Vector3 LLD = transform.position + new Vector3(-3.408889f, -5, 0);


        Gizmos.DrawLine(LU,LD);
        Gizmos.DrawLine(LD, RD);
        Gizmos.DrawLine(RD, RU);
        Gizmos.DrawLine(RU, LU);

        Gizmos.DrawLine(LU, LLU);
        Gizmos.DrawLine(LD, LLD);
        Gizmos.DrawLine(LLU, LLD);
        

    }
}
