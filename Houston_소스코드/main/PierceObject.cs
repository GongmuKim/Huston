using UnityEngine;
using System.Collections;

public class PierceObject : MonoBehaviour {
    
    public int tag_int;

    public float stay = 1;
    public float stay_first = 0;
    float go_speed = 10;
    float out_speed = 10;

    public float go_time;
    public float out_time;

    public Vector3 start_pos;
    public Vector3 start_scale;
    public string start_tag;
    public Quaternion start_angle;
    public bool y_freeze = true;

    public Vector2 goal = new Vector2(0, 0);
    Vector2 direction = new Vector2(0, 0);

    public GameObject goal_obj;
    

    enum State
    {
        go,
        stay,
        moveout
    }
    State state;
    public int kind;

    IEnumerator state_go()
    {

        yield return new WaitForSeconds(stay_first);
        state = State.go;
        StartCoroutine(pierce_end());
    }

    public void start_pierce()
    {
        StartCoroutine(state_go());
    }

    public enum Cases
    {
        once,
        come_back
    }
    public Cases cases;

	// Use this for initialization
    void Awake()
    {
        if(goal_obj!=null)
        {
            goal.x = goal_obj.transform.position.x - transform.parent.position.x;
            goal.y = goal_obj.transform.position.y;
        }
        Debug.Log("Awake goal x:" + goal.x + "  y:" + goal.y);
        start_pos = transform.localPosition;
        start_scale = transform.localScale;
        start_tag = transform.tag;
        start_angle = transform.rotation;
        tag_int = tag[3] - 48;
        
    }

	void Start () {

        
        state = State.stay;
        direction.x = goal.x - transform.localPosition.x;
        direction.y = goal.y - transform.localPosition.y;

        float dist= Mathf.Sqrt((direction.x*direction.x) + (direction.y * direction.y));
        go_speed = dist/go_time;
        out_speed = dist / out_time;
    }
	
	// Update is called once per frame
	void Update () {
	    switch(state)
        {
            case State.go:
                pierce();
                break;
            case State.stay:
                break;
            case State.moveout:
                move_out();
                break;

        }
	}
    void pierce()
    {
        transform.Translate(new Vector3(Mathf.Abs(go_speed * Time.deltaTime),0 , 0));

        /*
        Vector3 pos = transform.localPosition;
        if(direction.x>=0&&direction.y>=0&&
            pos.x>=goal.x&&pos.y>=goal.y)
        {
            pierce_end();
        }
        else if(direction.x<=0&&direction.y>=0&&
            pos.x<=goal.x&&pos.y>=goal.y)
        {
            pierce_end();
        }
        else if(direction.x<=0&&direction.y<=0&&
            pos.x<=goal.x&&pos.y<=goal.y)
        {
            pierce_end();
        }
        else if(direction.x>=0&&direction.y<=0&&
            pos.x>=goal.x&&pos.y<=goal.y)
        {
            pierce_end();
        }*/

    }
    IEnumerator pierce_end()
    {

        yield return new WaitForSeconds(go_time);
        transform.localPosition = new Vector3(goal.x, goal.y, transform.position.z);
        state = State.stay;

        if (cases == Cases.once) DestroyObject(gameObject);
        else StartCoroutine(stay_obj());
    }
    void move_out()
    {
        transform.Translate(new Vector3(-Mathf.Abs(out_speed * Time.deltaTime), 0, 0));
        Vector3 pos = transform.position;
        if(pos.x<-9||pos.x>9||
            pos.y<-5||pos.y>5)
        {
            DestroyObject(gameObject);
            //state = State.stay;
        }

    }

    IEnumerator stay_obj()
    {
        yield return new WaitForSeconds(stay);
        state = State.moveout;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
       ;
        if (goal_obj != null)
        {
            if(y_freeze) goal_obj.transform.localPosition = new Vector3(goal_obj.transform.localPosition.x,0,1);
            goal.x = goal_obj.transform.position.x - transform.parent.position.x;
            goal.y = goal_obj.transform.position.y;
            Gizmos.DrawLine(transform.position,
                new Vector3(goal_obj.transform.position.x, goal_obj.transform.position.y, 1));
        }
        //Gizmos.DrawLine(transform.position, new Vector3(transform.parent.transform.position.x+goal.x,goal.y,1));
    }


    public void copy(PierceObject script)
    {
        goal = script.goal;
        cases = script.cases;
        go_time = script.go_time;
        out_time = script.out_time;
        tag_int = script.tag_int;
        stay_first = script.stay_first;
    }
}
