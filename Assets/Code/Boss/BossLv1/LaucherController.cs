using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaucherController : MonoBehaviour
{
    public GameObject hellfire;
    public bool leftLaucher = true;
    void Start()
    {
        InvokeRepeating("SpawnBomb", 112f, 4f);
    }

    void SpawnBomb()
    {
        GameObject hellfireObject = Instantiate(hellfire, transform.position, Quaternion.identity);
        HellfireController hellfireController = hellfireObject.GetComponent<HellfireController>();
        hellfireController.Launch(Vector2.up, 8f, leftLaucher);
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnBomb");
    }
}
