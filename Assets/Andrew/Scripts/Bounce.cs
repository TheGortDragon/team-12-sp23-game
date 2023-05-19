using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {
    private Animator anim;
    private Rigidbody2D rb2;
    private AudioSource Asource;

    public float jumpBonus;
    public float jumpHeight;
    private float jumpHoldDuration;
    private bool jumpBoost;
    GameObject winCon;

    private float angle;
    public static bool playerRespawned;

    // Start is called before the first frame update
    void Start() {
        winCon = GameObject.FindGameObjectWithTag("Win_flag");
        rb2 = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        Asource = GetComponent<AudioSource>();
        jumpHoldDuration = 0;
        angle = 0;
        playerRespawned = true;
    }

    void Update() {
        Debug.Log(rb2.velocity);
        if (Mathf.Abs(rb2.velocity.x) > .01f) {
            playerRespawned = false;
        }
        jumpBoost = jumpHoldDuration > 0;
        if (jumpBoost) {
            jumpHoldDuration -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) {
            angle -= 20 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) {
            angle += 20 * Time.deltaTime;
        }

    }

    void FixedUpdate() {
        if (jumpBoost) {
            rb2.AddForce(transform.up * jumpBonus);
        }
        rb2.rotation = angle;
        anim.SetBool("boost", jumpBoost);
        anim.SetFloat("yVelocity", rb2.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (Input.GetKey(KeyCode.Space)) {
            jumpHoldDuration = 2;
        }
        Asource.Play();
        rb2.AddForce(transform.up * jumpHeight);
        StopAllCoroutines();
        anim.SetBool("ground", true);
        StartCoroutine(playAnim());
    }

    private IEnumerator playAnim() {
        yield return new WaitForSeconds(.2f);
        anim.SetBool("ground", false);
    }

    public void resetPlayer() {
        transform.position = new Vector3(0, -.8f, 0);
        rb2.velocity = Vector2.zero;
        angle = 0;
        playerRespawned = true;
    }
}
