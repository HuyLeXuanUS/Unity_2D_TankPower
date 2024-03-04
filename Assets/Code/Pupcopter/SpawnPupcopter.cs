using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPupcopter : MonoBehaviour
{
    public GameObject pupcopterPrefab;
    void Start()
    {       
        InvokeRepeating("SpawnPupcopterPrefab", 5f, 15.0f);
        Invoke("StopSpawning", 105f);
    }

    void SpawnPupcopterPrefab()
    {
        Instantiate(pupcopterPrefab, transform.position, Quaternion.identity);
    }

    void StopSpawning()
    {
        CancelInvoke("SpawnPupcopterPrefab");
    }
}
