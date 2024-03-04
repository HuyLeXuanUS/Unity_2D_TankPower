using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTimeDelay = 2.0f;
    public float spawnTimeInterval = 2.5f;
    void Start()
    {       
        InvokeRepeating("SpawnEnemy", spawnTimeDelay, spawnTimeInterval);
        Invoke("StopSpawning", 105f);
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    void StopSpawning()
    {
        CancelInvoke("SpawnEnemy");
    }
}
