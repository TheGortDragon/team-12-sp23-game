using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour {
    private GameObject player;
    private Bounce bounce;
    public float outOfBounds;
    void Start() {
        player = GameObject.Find("Player");
        bounce = player.GetComponent<Bounce>();
    }

    // Update is called once per frame
    void Update() {
        if (player.transform.position.y < outOfBounds) {
            bounce.resetPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != "Player") {
            Debug.Log("ded");
            bounce.resetPlayer();
        }
    }
}
