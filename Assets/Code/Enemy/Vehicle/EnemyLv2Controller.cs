using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLv2Controller : MonoBehaviour
{
    public float speed = 4.5f;
    private bool lookRight = true;
    private Camera mainCamera;
    int checkDestroy = 0;
    public GameObject Bomb; 
    private float currentHealth = 30;
    public GameObject destroyEffect;

    void Start()
    {
        mainCamera = Camera.main;

        float spawnDirection = Random.Range(0f, 1f);

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        Vector3 spawnPosition;

        if (spawnDirection < 0.5f)
        {
            spawnPosition = new Vector3(bottomLeft.x, Random.Range(1, 3f), 0f);
            lookRight = true;
        }
        else
        {
            spawnPosition = new Vector3(topRight.x, Random.Range(1, 3f), 0f);
            lookRight = false;
        }

        if (!lookRight)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        transform.position = spawnPosition;
        StartCoroutine(SpawnBombAfterDelay());
    }

    void Update()
    {
        if (lookRight)
        {
            transform.Translate(Vector3.right * (speed + 0.75f) * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * (speed - 0.75f) * Time.deltaTime);
        }

        if (IsVisibleFromCamera())
        {
            checkDestroy = 1;
        }

        if (checkDestroy == 1)
        {
            if (!IsVisibleFromCamera())
            {
                Destroy(gameObject);
            }
        }
    }

    bool IsVisibleFromCamera()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        return GeometryUtility.TestPlanesAABB(planes, GetComponent<Renderer>().bounds);
    }

    IEnumerator SpawnBombAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject bombObject = Instantiate(Bomb, transform.position, Quaternion.identity);
        DumbbombController dumbbomb = bombObject.GetComponent<DumbbombController>();
        dumbbomb.Launch(Vector2.down, 50.0f, lookRight);
    }

    public void changeHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth <= 0)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
