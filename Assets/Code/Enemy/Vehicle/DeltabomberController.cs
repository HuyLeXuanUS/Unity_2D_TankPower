using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltabomberController : MonoBehaviour
{
    public float speed = 5.5f;
    private bool lookRight = true;
    private Camera mainCamera;
    int checkDestroy = 0;
    public GameObject bombfrag; 
    private float currentHealth = 20;
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
            spawnPosition = new Vector3(bottomLeft.x, Random.Range(1.5f, 3.5f), 0f);
            lookRight = true;
        }
        else
        {
            spawnPosition = new Vector3(topRight.x, Random.Range(1.5f, 3.5f), 0f);
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
        StartCoroutine(SpawnBombAfterDelay1());
        StartCoroutine(SpawnBombAfterDelay2());
        StartCoroutine(SpawnBombAfterDelay3());
        StartCoroutine(SpawnBombAfterDelay4());
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
        GameObject bombObject = Instantiate(bombfrag, transform.position, Quaternion.identity);
        BombfragController bombfragController = bombObject.GetComponent<BombfragController>();

        GameObject target = GameObject.FindGameObjectWithTag("Tank");
        Vector2 direction = target.transform.position - transform.position;
        bombfragController.Launch(direction.normalized, 200.0f);
    }

    IEnumerator SpawnBombAfterDelay1()
    {
        yield return new WaitForSeconds(0.75f);
        GameObject bombObject = Instantiate(bombfrag, transform.position, Quaternion.identity);
        BombfragController bombfragController = bombObject.GetComponent<BombfragController>();

        GameObject target = GameObject.FindGameObjectWithTag("Tank");
        Vector2 direction = target.transform.position - transform.position;
        bombfragController.Launch(direction.normalized, 200.0f);
    }

    IEnumerator SpawnBombAfterDelay2()
    {
        yield return new WaitForSeconds(1f);
        GameObject bombObject = Instantiate(bombfrag, transform.position, Quaternion.identity);
        BombfragController bombfragController = bombObject.GetComponent<BombfragController>();

        GameObject target = GameObject.FindGameObjectWithTag("Tank");
        Vector2 direction = target.transform.position - transform.position;
        bombfragController.Launch(direction.normalized, 200.0f);
    }
    IEnumerator SpawnBombAfterDelay3()
    {
        yield return new WaitForSeconds(1.25f);
        GameObject bombObject = Instantiate(bombfrag, transform.position, Quaternion.identity);
        BombfragController bombfragController = bombObject.GetComponent<BombfragController>();

        GameObject target = GameObject.FindGameObjectWithTag("Tank");
        Vector2 direction = target.transform.position - transform.position;
        bombfragController.Launch(direction.normalized, 200.0f);
    }

    IEnumerator SpawnBombAfterDelay4()
    {
        yield return new WaitForSeconds(1.50f);
        GameObject bombObject = Instantiate(bombfrag, transform.position, Quaternion.identity);
        BombfragController bombfragController = bombObject.GetComponent<BombfragController>();

        GameObject target = GameObject.FindGameObjectWithTag("Tank");
        Vector2 direction = target.transform.position - transform.position;
        bombfragController.Launch(direction.normalized, 200.0f);
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
