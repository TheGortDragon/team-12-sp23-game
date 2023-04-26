using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {
    private Rigidbody2D rb2;
    public float jumpBonus;
    public float jumpHeight;
    private float jumpHoldDuration;
    private bool jumpBoost;

    private float angle;

    // Start is called before the first frame update
    void Start() {
        rb2 = GetComponent<Rigidbody2D>();
        jumpHoldDuration = 0;
        angle = 0;
    }

    void Update() {
        jumpBoost = jumpHoldDuration > 0;
        if (jumpBoost) {
            jumpHoldDuration -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D)) {
            angle -= 10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) {
            angle += 10 * Time.deltaTime;
        }
    }

    void FixedUpdate() {
        if (jumpBoost) {
            rb2.AddForce(transform.up * jumpBonus);
            rb2.rotation = angle;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (Input.GetKey(KeyCode.Space)) {
            jumpHoldDuration = 2;
        }
        rb2.AddForce(transform.up * jumpHeight);
        rb2.rotation = angle;
    }
}
