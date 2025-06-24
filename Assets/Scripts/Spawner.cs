using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject speedboostPrefab;
    public GameObject shieldPrefab;
    public Transform player; 
    private float spawnRadius = 10f;
    public float minDistance = 0.2f;
    private bool spawnShieldNext = true;

    void Start()
    {
        {
        for (int i = 0; i < 2; i++)
        {
            SpawnEnemy();
        }
        
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnBoost());
        }
        
    }
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float waitTime = Random.Range(3f, 10f);
            yield return new WaitForSeconds(waitTime);
            SpawnEnemy();
        }
    }

    IEnumerator SpawnBoost()
    {
        while (true)
        {
            float waitTime = Random.Range(45f, 60f);
            yield return new WaitForSeconds(waitTime);
            Vector2 spawnPos = GetSpawnPos();
            GameObject prefabToSpawn = spawnShieldNext ? shieldPrefab : speedboostPrefab;
            Instantiate(prefabToSpawn, new Vector3(spawnPos.x, spawnPos.y, 0f), Quaternion.identity);
            spawnShieldNext = !spawnShieldNext;
            
        }
    }



    void SpawnEnemy()
    {
        Vector2 spawnPos = GetSpawnPos();
        Instantiate(enemyPrefab, new Vector3(spawnPos.x, spawnPos.y, 0f), Quaternion.identity);
    }

    Vector2 GetSpawnPos()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        float distance = Random.Range(minDistance, spawnRadius);

        Vector2 spawnPos = (Vector2)player.position + randomDirection * distance;

        return spawnPos;

    }

}
