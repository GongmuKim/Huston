using UnityEngine;
using System.Collections;

public class ScrollObject : MonoBehaviour
{


    public int kind;
    public float speed = 10.0f;

    public Vector3 start_pos;
    public Vector3 start_scale;
    public string start_tag;
    public Quaternion start_angle;
    public int tag_int;

    public float xl;
    float xr;

    void Awake()
    {
        start_pos = transform.position;
        start_scale = transform.localScale;
        start_tag = transform.tag;
        start_angle = transform.rotation;
        tag_int = tag[3] - 48;
        xl = transform.position.x;
        if (tag == "Switch")
        {
            xl -= 0.5f * (14.363f+ 3.408889f) / 2.0f;
        }
        else xl -= 0.5f * transform.localScale.x / 2.0f;
        //Map.pos.x -= 0.5f * Map.scale.x / 2.0f;
    }

    // Use this for initialization
    void Start()
    {
        if (tag == "End0")
        {
            Debug.Log("EEEEEEEEEEEEEE");
        }
        xr= transform.position.x;
        xr += 0.5f * transform.localScale.x / 2.0f;
        //Map.pos.x -= 0.5f * Map.scale.x / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (tag == "Switch" && speed == 0)
        {
            if (transform.GetChildCount() == 2)
            {
                //Destroy(gameObject);
            }
        }

        float angle = transform.rotation.eulerAngles.z;
        transform.Translate(-1 * speed * Time.deltaTime * Mathf.Cos(angle * Mathf.Deg2Rad),
             speed * Time.deltaTime * Mathf.Sin(angle * Mathf.Deg2Rad),
            0);//방향 어떻게 돌렸든 왼쪽으로만감
        xr -= speed * Time.deltaTime;


        if (xr<-10)
        {
            Destroy(gameObject);
        }


    }
    public void Destroy_this()
    {
        Destroy(gameObject);
    }
    void OnDrawGizmos()
    {

        Gizmos.color = new Color(1, 1, 0, 1);
        Gizmos.DrawLine(new Vector3(xl, -10, 0), new Vector3(xl, 10, 0));

        Gizmos.color = new Color(0, 1, 0, 1);
        Gizmos.DrawLine(new Vector3(xr, -10, 0), new Vector3(xr, 10, 0));
    }


    public void copy(ScrollObject script)
    {
        speed = script.speed;
        tag_int = script.tag_int;
    }
}
