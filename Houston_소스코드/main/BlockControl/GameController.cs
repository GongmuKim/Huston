using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    

    enum State
    {
        Play,
        GameOver,
        Pause,
        Clear
    }
    State state;
    
    public ControllPlayer player1;
    public ControllPlayer player2;
    public Create_Objs cre;

    public GameObject pause_obj;

    public GameObject []Lines;

    public void go_menu()
    {
        Debug.Log("GO_MENU");
        SceneManager.LoadScene("gamemenu");
    }

    public void go_replay()
    {
        SceneManager.LoadScene("main");
    }
    public void go_next()
    {
        PlayerPrefs.SetInt("h_scene", PlayerPrefs.GetInt("h_scene")+1  );
        PlayerPrefs.Save();
        SceneManager.LoadScene("main");
    }

    void Awake()
    {
        pause_obj.GetComponent<Canvas>().enabled = false;
    }

    // Use this for initialization
    void Start () {
        state = State.Play;
        Time.timeScale = 1f;
    }
	
	// Update is called once per frame
	void Update () {
        switch(state)
        {
            case State.Play:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Time.timeScale = 0;
                    pause_obj.GetComponent<Canvas>().enabled = true;
                    state = State.Pause;
                }
                 Playing();
                if (player1.Isdead()||player2.Isdead())
                {
                    GameOver();
                }
                if(player1.Isclear)
                {
                    GameClear();
                }
                break;
            case State.Pause:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Time.timeScale = 1;
                    pause_obj.GetComponent<Canvas>().enabled = false;
                    state = State.Play;
                }
                break;
        }	
	}
    void Playing()
    {
        cre.obs_creates();
    }
    void GameOver()
    {
        state = State.GameOver;

        ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();
        foreach (ScrollObject so in scrollObjects)
        {
            so.enabled = false;
            so.StopAllCoroutines();
        }


        PierceObject[] pierceObjects = GameObject.FindObjectsOfType<PierceObject>();
        foreach (PierceObject so2 in pierceObjects)
        {
            so2.enabled = false;
            so2.StopAllCoroutines();
        }
        //player1.particle.emit = false;
        //player1.particle.worldVelocity = new Vector3(1, 1, 1);

        ParticleSystem.EmissionModule em = player1.particle.emission;
        em.enabled = false;

       
        player1.particle_dead.Play();
        player1.render.enabled = false;

        player2.particle_dead.Play();
        player2.render.enabled = false;


        player1.enabled = false;
        player2.enabled = false;

        StartCoroutine(Replay());
    }

    void GameClear()
    {
        state = State.Clear;

        ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();
        foreach (ScrollObject so in scrollObjects) so.enabled = false;


        PierceObject[] pierceObjects = GameObject.FindObjectsOfType<PierceObject>();
        foreach (PierceObject so2 in pierceObjects) so2.enabled = false;

        player1.GetComponent<Rigidbody2D>().velocity = new Vector3(10,0,0);

        player1.GetComponent<ControllPlayer>().enabled = false;
        player2.GetComponent<ControllPlayer>().enabled = false;

        StartCoroutine(Clear_popup());
    }

    IEnumerator Replay()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("main");
    }

    IEnumerator Clear_popup()
    {
        yield return new WaitForSeconds(2);
        pause_obj.GetComponent<Canvas>().enabled = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0f, 0f, 0.5f);

        Gizmos.DrawLine(new Vector3(-20, -5, 0), new Vector3(1000, -5, 0));
        Gizmos.DrawLine(new Vector3(-20, 5, 0), new Vector3(1000, 5, 0));


        Gizmos.color = new Color(0, 0f, 1f, 1f);
        for (int i = 0; i < Lines.Length; i++)
        {
            Gizmos.DrawLine(new Vector3(-20, Lines[i].transform.position.y, 0), new Vector3(1000, Lines[i].transform.position.y, 0));
        }


    }
}
