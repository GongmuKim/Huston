using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ControllPlayer : MonoBehaviour
{
    public static bool GetFire;
    public Canvas Input_butten;
    public float speed=1;
    bool moveup = false;
    bool movedown = false;
    public float target_point;
    public bool non_dead;
    public Text textinfo;
    public ParticleSystem particle;
    public ParticleSystem particle_dead;

    public SpriteRenderer render;
    int Tcount = 0;
    bool isdead;
    public bool Isdead()
    {
        return isdead;
    }

    bool isclear;
    public bool Isclear
    {
        get
        {
            return isclear;
        }
        set
        {
            isclear = Isclear;
        }
    }
    

    // Use this for initialization
    void Start () {
        if (non_dead)
        {
            CircleCollider2D col2 = GetComponent<CircleCollider2D>();
            col2.isTrigger = true;
        }

        target_point = transform.position.y;
        isdead = false;
        render = GetComponent<SpriteRenderer>();

        //rbody = GetComponent<Rigidbody2D>();
        
                
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        int cnt = Input.touchCount;
        Touch[] myTouches = Input.touches;//멀티터치 부분
        if (Application.platform == RuntimePlatform.WindowsEditor && Input.GetButton("Fire1"))
        {
            cnt = 1;
            Debug.Log("Touch");
        }
        Tcount = 0;
        for (int i = 0; i < cnt; i++)//터치 갯수만큼 for문 돌아감
        {
            
            if (Application.platform == RuntimePlatform.WindowsEditor || Input.GetTouch(i).phase == TouchPhase.Began ||
                Input.GetTouch(i).phase == TouchPhase.Moved || Input.GetTouch(i).phase == TouchPhase.Stationary)
            {

                Vector3 pos; //= Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                if(Application.platform == RuntimePlatform.WindowsEditor)
                    pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                else
                    pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

                if (pos.x < 0)
                {
                    ++Tcount;
                    if (Tcount > 1)
                    {
                        target_point = transform.position.y;
                        moveup = false; movedown = false;
                        break;
                    }
                    //transform.position = new Vector3(transform.position.x, target_point, transform.position.z);

                    if (in_rect(pos) == true)
                    {
                        
                        target_point = pos.y;
                        if (target_point > 3.7f)
                        {
                            target_point = 3.7f;
                        }
                        else if (target_point < -3.7f)
                        {
                            target_point = -3.7f;
                        }
                    }
                }

                if (transform.position.y < target_point)
                {
                    moveup = true; movedown = false;
                }
                else if (transform.position.y > target_point)
                {
                    moveup = false; movedown = true;
                }
                if (Mathf.Abs(transform.position.y - target_point) < 1)
                {
                    moveup = false; movedown = false;
                    transform.position = new Vector3(transform.position.x, target_point, transform.position.z);
                }
            }
        }
        //textinfo.text = Tcount.ToString();//모니터 디버깅
        if (moveup)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            if (target_point < transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, target_point, transform.position.z);
                moveup = false;
            }
        }
        else if (movedown)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            if (target_point > transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, target_point, transform.position.z);
                movedown = false;
            }
        }
    }
    bool in_rect(Vector3 pos)
    {
        if (pos.x < (-7.8) - 7.0 || pos.x > (-7.8) + 7.0) return false; // 왼쪽 플레이어 터치범위 조정
        
        return true;
    }

    void OnCollisionEnter2D(Collision2D other)//그냥충돌
    {
        //isdead = true;
        Debug.Log(this.tag+"   "+other.gameObject.tag);
        if (this.tag=="Player1"&&other.gameObject.tag == "Map1")
        {
            Debug.Log("DEAD");
            if (non_dead == false) isdead = true;
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)//트리거
    {
        if (this.tag == "Player1" && other.tag == "Map2")
        {
            Debug.Log("DEAD");
            if (non_dead == false) isdead = true;
        }

        if (this.tag == "Player2" && other.tag == "Map2")
        {
            //if(non_dead==false)isdead = true;

        }
        if(this.tag=="Player1" && other.tag=="Switch")
        {
            other.gameObject.transform.position = new Vector3(gameObject.transform.position.x+0.3f,0,0);
            ScrollObject sc= other.gameObject.GetComponent<ScrollObject>();
            sc.speed = 0;
            for(int i=0;i<other.gameObject.transform.childCount;i++)
            {
                GameObject pierce_obj= (GameObject)other.gameObject.transform.GetChild(i).gameObject;
                if (pierce_obj.tag == "Untagged") continue;
                PierceObject pierce_script = pierce_obj.GetComponent<PierceObject>();
                pierce_script.start_pierce();
            }
        }
        if(this.tag=="Player1"&&other.tag=="End0")
        {
            isclear = true;
        }
    }
}
