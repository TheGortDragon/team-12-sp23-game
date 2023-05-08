using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class square_enemy_variant : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // if (transform.position.x < -180 || transform.position.x > 180)
        // {
        //     Destroy(gameObject);
        // }



        if (gameObject.transform.position.y < -100f)
        {
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
        if (collision.gameObject.CompareTag("enemy"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
    }
    // private void OnBecameInvisible()
    // {
    //     // isVisible = false;
    //     Destroy(gameObject);
    // }
}
