using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kill : MonoBehaviour
{
    private GameObject player;
    private Bounce bounce;
    public float outOfBounds;
    public GameObject winCon;
    public TimeManagerScript timeManager;
    //public AudioSource winGame;
    public AudioSource loseGame;
    [SerializeField]
    private float distanceToWin;

    void Start()
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManagerScript>();
        player = GameObject.Find("Player");
        bounce = player.GetComponent<Bounce>();
        winCon = GameObject.FindGameObjectWithTag("Win_flag");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player position: " + player.transform.position);
        //Debug.Log("Win_con position: " + winCon.transform.position);

        if (player.transform.position.y < outOfBounds)
        {
            bounce.resetPlayer();
            timeManager.timeStart();
            playLoseSound();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            bounce.resetPlayer();
            timeManager.timeStart();
        }

        float distanceToWinCon = Vector3.Distance(
            player.transform.position,
            winCon.transform.position
        );
        if (distanceToWinCon <= distanceToWin)
        {
            //Debug.Log("close to flag");
           // player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            //timeManager.timeStop();
            
            //StartCoroutine(GameObject.Find("Fade").GetComponent<DrawMap>().FadeOut());
            //timeManager.timeStart();

            //level end
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.tag == "Player") {
        //     Debug.Log("ded");
        //     bounce.resetPlayer();
        // }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Win_flag") {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            StartCoroutine(GameObject.Find("Fade").GetComponent<DrawMap>().FadeOut());
            timeManager.timeStop();
        }
    }

    public void playLoseSound()
    {
        loseGame.Play();
    }
}
