using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour
{
    public GameObject enemy;
    private int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("enemy").Length;
        StartCoroutine(SpawnEnemyWithDelay());
    }

    IEnumerator SpawnEnemyWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            if (enemyCount < 5)
            {
                spawnEnemy();
            }
        }
    }

    private void spawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0);
        GameObject enemySpawn = Instantiate(enemy, spawnPosition, transform.rotation);
        enemyCount++;
    }

    public void minusEnemy()
    {
        enemyCount--;
    }
}
