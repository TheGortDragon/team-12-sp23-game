using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    private GameObject player;
    private Bounce bounce;
    public float outOfBounds;
    public GameObject winCon;
    [SerializeField]
    private float distanceToWin;

    void Start()
    {
        player = GameObject.Find("Player");
        bounce = player.GetComponent<Bounce>();
        winCon = GameObject.FindGameObjectWithTag("Win_flag");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player position: " + player.transform.position);
        Debug.Log("Win_con position: " + winCon.transform.position);

        if (player.transform.position.y < outOfBounds)
        {
            bounce.resetPlayer();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            bounce.resetPlayer();
        }

        float distanceToWinCon = Vector3.Distance(
            player.transform.position,
            winCon.transform.position
        );
        if (distanceToWinCon <= distanceToWin)
        {
            Debug.Log("close to flag");
            bounce.resetPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.tag == "Player") {
        //     Debug.Log("ded");
        //     bounce.resetPlayer();
        // }
    }
}
