using UnityEngine;
using System.Collections;

public class Create_Objs : MonoBehaviour
{
    /*
    public struct obstacle
    {
        public obstacle(Vector3 _pos, float _angle, int _kind, float _speed, int _tag, Vector3 _scale)
        {
            this.pos = _pos;
            this.angle = _angle;
            this.kind = _kind;
            this.speed = _speed;
            this.tag = _tag;
            this.scale = _scale;
        }
        public Vector3 pos, scale;
        public float angle, speed;
        public int kind, tag;
    }*/
    

    public GameObject[] obs1;//오른쪽에있는공쪽 장애물
    public GameObject[] obs2;//왼쪽에있는공쪽 장애물
    public GameObject switch_gameobj;
    public GameObject[] pierce_gameobj1;
    public GameObject[] pierce_gameobj2;
    public GameObject pierce_obj_base;

    public GameObject[][] pierce_gameobjs=new GameObject[2][];
    public GameObject End_obj;
    public GameObject []stage_obj;

    public int h_stage = -1;

    public GameObject Stage;

    ArrayList Maps = new ArrayList();

    ArrayList switch_objs = new ArrayList();
    ArrayList switch_objs_in = new ArrayList();

    void Awake()
    {
        if (h_stage != -1)//-1이면지금 만드는거 테스트중
        {
            if (h_stage == 0) h_stage=PlayerPrefs.GetInt("h_scene");//0이면 스테이지 선택했던거 불러오기 아니면 직접써놓은거
            GameObject ob = (GameObject)Instantiate(stage_obj[h_stage], new Vector3(0, 0, 0), Quaternion.identity);
            ob.name = "Stage";
        }
    }

    // Use this for initialization
    void Start()
    {

        pierce_gameobjs[0] = new GameObject[2];
        pierce_gameobjs[1] = new GameObject[2];
        for (int i=0;i<pierce_gameobj1.Length;i++)
        {
            pierce_gameobjs[0][i] = pierce_gameobj1[i];
            pierce_gameobjs[1][i] = pierce_gameobj2[i];
        }

        ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();
        foreach (ScrollObject so in scrollObjects)
        {
            
            if (so.gameObject.transform.parent!=null&&so.gameObject.transform.parent.gameObject.tag == "Switch") continue;
            /*
            Vector3 vvv=so.gameObject.transform.position;
            float zzz = so.gameObject.transform.rotation.eulerAngles.z;
            Debug.Log(so.gameObject.tag);
            int ttt = so.gameObject.tag[3] - 48;
            Vector3 scsc = so.gameObject.transform.localScale;

            obstacle Map = new obstacle(so.gameObject.transform.position,
                so.gameObject.transform.rotation.eulerAngles.z, so.kind, so.speed,
                so.gameObject.tag[3] - 48, so.gameObject.transform.localScale);//MAP
                */
            if (so.gameObject.tag == "End0")
            {
                Debug.Log("END_tag");
                //Map.tag = 0;
            }

            //Map.pos.x -= 0.5f * Map.scale.x / 2.0f;

            ScrollObject sc = so.transform.GetComponent<ScrollObject>();

            if (so.tag != "Switch") Maps.Add(sc);
            else
            {
                //Map.pos.x += 0.5f * Map.scale.x / 2.0f;
                switch_objs.Add(sc);
                ArrayList pierce_objs = new ArrayList();
                int cnt = so.transform.GetChildCount();
                for (int i = 0; i < cnt; i++)
                {
                    if (so.transform.GetChild(i).gameObject.tag == "Untagged") continue;
                    PierceObject pc = so.transform.GetChild(i).gameObject.GetComponent<PierceObject>();
                    if (pc.tag == "Pierce")
                    {
                        Debug.Log("Pierce");
                        pc.tag_int = -1;
                    }



                    pierce_objs.Add(pc);
                }
                switch_objs_in.Add(pierce_objs);
            }
            
            so.Destroy_this();
            //so.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void obs_creates()
    {
        for (int i = 0; i < Maps.Count; i++)
        {
            ScrollObject Map = (ScrollObject)Maps[i];
            Map.xl -= Map.speed * Time.deltaTime;
            //Map.pos.x -= Map.speed * Time.deltaTime;
            Maps[i] = Map;

            if (Map.xl <= 10)
            {
                Map.xl += 0.5f * Map.start_scale.x / 2.0f;
                Map.start_pos.x = Map.xl;
                if (Map.tag_int == 1)
                {
                    GameObject go = (GameObject)  Instantiate(obs1[Map.kind], Map.start_pos,  Map.start_angle);
                    ScrollObject obj_script = go.GetComponent<ScrollObject>();
                    obj_script.speed = Map.speed;
                    go.transform.localScale = Map.start_scale;
                    go.transform.SetParent(Stage.transform);
                }
                else if (Map.tag_int == 2)
                {
                    GameObject go = (GameObject)Instantiate(obs2[Map.kind], Map.start_pos, Map.start_angle);
                    ScrollObject obj_script = go.GetComponent<ScrollObject>();
                    obj_script.speed = Map.speed;
                    go.transform.localScale = Map.start_scale;
                    go.transform.SetParent(Stage.transform);
                }
                else if(Map.tag_int==0)
                {

                    GameObject go = (GameObject)Instantiate(End_obj, Map.start_pos, Map.start_angle );
                    ScrollObject obj_script = go.GetComponent<ScrollObject>();
                    obj_script.speed = Map.speed;
                    go.transform.localScale = Map.start_scale;
                    go.transform.SetParent(Stage.transform);
                }
                Maps.RemoveAt(i);
                i--;
            }
        }

        for (int i = 0; i < switch_objs.Count; i++)
        {

            ScrollObject switch_obj_info = (ScrollObject)switch_objs[i];
            switch_obj_info.xl -= switch_obj_info.speed * Time.deltaTime;
            switch_objs[i] = switch_obj_info;
            if (switch_obj_info.xl <= 10)
            {
                switch_obj_info.start_pos.x= switch_obj_info.xl + 0.5f * (14.363f + 3.408889f) / 2.0f;

                //switch_obj_info.xl;
                GameObject switch_obj =
                    (GameObject)Instantiate(
                        switch_gameobj,
                        switch_obj_info.start_pos,
                         switch_obj_info.start_angle);
                switch_obj.transform.localScale = switch_obj_info.start_scale;
                switch_obj.transform.SetParent(Stage.transform);

                ScrollObject scr_sw = switch_obj.GetComponent<ScrollObject>();
                scr_sw.speed = switch_obj_info.speed;

                ArrayList pierce_objs = (ArrayList)switch_objs_in[i];
                int cnt = pierce_objs.Count;
                for (int j = 0; j < cnt; j++)
                {
                    PierceObject pp = (PierceObject)pierce_objs[j];

                    GameObject pierce_obj;
                    Debug.Log(pp.start_tag);
                    if (pp.start_tag == "Pierce")////////////////////////////////////
                    {
                        pierce_obj = (GameObject)Instantiate(pierce_obj_base, pp.start_pos, pp.start_angle);
                    }
                    else
                    {
                        pierce_obj =
                            (GameObject)Instantiate(
                                pierce_gameobjs[pp.tag_int - 1][pp.kind],
                                pp.start_pos,
                                pp.start_angle
                                );
                    }

                    pierce_obj.transform.localScale = pp.start_scale;
                    pierce_obj.transform.SetParent(switch_obj.transform);
                    pierce_obj.transform.localPosition = pp.start_pos;
                    
                    PierceObject pierce_script = pierce_obj.GetComponent<PierceObject>();
                    //pierce_script = pp;
                    pierce_script.copy(pp);

                }
                switch_objs.RemoveAt(i);
                switch_objs_in.RemoveAt(i);
                i--;
                ////////////////if


            }


        }
    }
}
