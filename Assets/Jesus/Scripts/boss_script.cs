using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_script : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2.0f;
    public float threshold = 0.05f;
    public float waitTime = 5.0f;

    private int currentWaypoint = 0;

    public GameObject objectToShoot;
    public float shootForce = 5f;
    public float shootInterval = 1f;

    public Transform leftBarrel;
    public Transform rightBarrel;

    public AudioSource enemyHit;

    // private float lastShootTime = 0f;

    public float pushForce = 10f;

    void Start()
    {
        StartCoroutine(MoveToNextWaypoint());
    }

    IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                waypoints[currentWaypoint].position,
                speed * Time.deltaTime
            );

            if (
                Vector3.Distance(transform.position, waypoints[currentWaypoint].position)
                < threshold
            )
            {
                if (currentWaypoint == waypoints.Length - 1)
                {
                    yield return new WaitForSeconds(waitTime);
                    currentWaypoint = 0;

                    // GameObject newObject = Instantiate(
                    //     objectToShoot,
                    //     transform.position,
                    //     Quaternion.identity
                    // );

                    // Vector2 fourtyFive = new Vector2(1, 1);


                    // GameObject newObject1 = Instantiate(
                    //     objectToShoot,
                    //     transform.position,
                    //     transform.rotation
                    // );

                    // Vector2 fourtyFive2 = new Vector2(-1, 1);



                    GameObject newObject1 = Instantiate(
                        objectToShoot,
                        rightBarrel.position,
                        rightBarrel.rotation
                    );

                    newObject1
                        .GetComponent<Rigidbody2D>()
                        .AddForce(rightBarrel.transform.right * shootForce, ForceMode2D.Impulse);

                    GameObject newObject2 = Instantiate(
                        objectToShoot,
                        leftBarrel.position,
                        leftBarrel.rotation
                    );

                    newObject2
                        .GetComponent<Rigidbody2D>()
                        .AddForce(leftBarrel.transform.right * shootForce, ForceMode2D.Impulse);
                }
                else
                {
                    currentWaypoint++;
                }
            }

            yield return null;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            // Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 bossVelocity = GetComponent<Rigidbody2D>().velocity;
            playerRigidbody.AddForce(-bossVelocity * pushForce, ForceMode2D.Impulse);
            playEnemySound();
        }
    }

    public void playEnemySound()
    {
        enemyHit.Play();
    }
}
