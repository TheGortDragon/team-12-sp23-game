using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class square_enemy : MonoBehaviour
{
    Rigidbody2D square;

    public GameObject main;
    public GameObject hero;
    public GameObject square_enemy_GO;
    public Vector2 movement;
    public Transform[] waypoints;
    [SerializeField]
    public float threshold = .05f;
    public enemy_spawner spawner;

    public float speed = 5f;
    public float distance;
    private float leftBound;
    private float rightBound;
    private int direction = 1;
    private bool goingDown = false;
    private Rigidbody2D rb;
    private int currentWaypoint = 0;

    [SerializeField]
    private float unitsToHero = 0;

    void Start()
    {
        waypoints = GameObject
            .FindGameObjectsWithTag("respawn_waypoint")
            .Select(go => go.transform)
            .ToArray();
        main = GameObject.FindGameObjectWithTag("MainCamera");
        spawner = GameObject.FindObjectOfType<enemy_spawner>();

        rb = GetComponent<Rigidbody2D>();

        hero = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(MoveToNextWaypoint());
    }

    void Update()
    {
        if (transform.position.y < -100)
        {
            spawner.minusEnemy();
            Destroy(gameObject);
        }
    }

    IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            if (goingDown)
            {
                goDown();
                yield break; // exit the coroutine
            }
            Vector2 target = waypoints[currentWaypoint].position;

            // calculate direction to target
            Vector2 direction = target - rb.position;

            // move towards target
            rb.MovePosition(rb.position + direction.normalized * speed * Time.fixedDeltaTime);

            // check if reached target
            if (Vector2.Distance(rb.position, target) < threshold)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            }

            float heroX = hero.transform.position.x;
            float squareX = square_enemy_GO.transform.position.x;

            if (Mathf.Abs(heroX - squareX) < unitsToHero)
            {
                goingDown = true;
            }

            if (goingDown)
            {
                goDown();
            }

            yield return new WaitForFixedUpdate();
        }
    }

    // }

    private void goDown()
    {
        // Debug.Log("same x value");
        rb.velocity = Vector2.zero;
        rb.mass = .25f;
        rb.gravityScale = 150f;
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
}
