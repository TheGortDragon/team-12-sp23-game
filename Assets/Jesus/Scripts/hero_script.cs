using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero_script : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0f);
        rb.AddForce(movement * speed);
    }
}
