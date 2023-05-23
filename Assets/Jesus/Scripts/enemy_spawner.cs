using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour
{
    public GameObject enemy;

    [SerializeField]
    private int enemyCount;

    [SerializeField]
    private float waitTime = 0;

    [SerializeField]
    private float maxEnemies = 0;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // enemyCount = GameObject.FindGameObjectsWithTag("enemy").Length;
        StartCoroutine(SpawnEnemyWithDelay());
    }

    void Update(){
        enemyCount = spawnedEnemies.Count;
    }

    IEnumerator SpawnEnemyWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            if (spawnedEnemies.Count < maxEnemies)
            {
                spawnEnemy();
            }
        }
    }

    private void spawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 0);
        GameObject enemySpawn = Instantiate(enemy, spawnPosition, transform.rotation);
        spawnedEnemies.Add(enemySpawn);
        // enemyCount++;
    }

    public void minusEnemy()
    {
        // enemyCount--;
        spawnedEnemies.Remove(enemy);
    }
}
