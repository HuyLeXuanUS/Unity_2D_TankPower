using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupcopterController : MonoBehaviour
{
    public float speed = 5f;
    private bool lookRight = true;
    private Camera mainCamera;
    int checkDestroy = 0;
    public GameObject itemArmor;
    public GameObject itemUpdamage;
    public GameObject itemCountBullet;
    public GameObject itemCrates1;
    public GameObject itemCrates2;
    public GameObject itemCrates3;
    public GameObject itemCrates4;

    void Start()
    {
        mainCamera = Camera.main;

        float spawnDirection = Random.Range(0f, 1f);

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        Vector3 spawnPosition;

        if (spawnDirection < 0.5f)
        {
            spawnPosition = new Vector3(bottomLeft.x, Random.Range(2, 3f), 0f);
            lookRight = true;
        }
        else
        {
            spawnPosition = new Vector3(topRight.x, Random.Range(2, 3f), 0f);
            lookRight = false;
        }

        if (!lookRight)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        transform.position = spawnPosition;
        StartCoroutine(SpawnItemAfterDelay());
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

    IEnumerator SpawnItemAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        float randomItem = Random.Range(0f, 1f);


        if (randomItem < 0.25)
        {
            GameObject itemObject = Instantiate(itemArmor, transform.position, Quaternion.identity);
            ItemArmorController itemArmorController = itemObject.GetComponent<ItemArmorController>();
            itemArmorController.Launch(Vector2.down, 100f, lookRight);
        }
        else if (randomItem < 0.5)
        {
            GameObject itemObject = Instantiate(itemUpdamage, transform.position, Quaternion.identity);
            ItemUpDamageController itemUpdamageController = itemObject.GetComponent<ItemUpDamageController>();
            itemUpdamageController.Launch(Vector2.down, 100f, lookRight);
        }
        else if (randomItem < 0.75)
        {
            GameObject itemObject = Instantiate(itemCountBullet, transform.position, Quaternion.identity);
            ItemCountBulletController itemCountBulletController = itemObject.GetComponent<ItemCountBulletController>();
            itemCountBulletController .Launch(Vector2.down, 100f, lookRight);
        }
        else 
        {
            GameObject tankObject = GameObject.FindWithTag("Tank");
            TankController tankController = tankObject.GetComponent<TankController>();
            int countItemCrates = tankController.getCountItemBeamfire();

            if (countItemCrates == 0)
            {
                GameObject itemObject = Instantiate(itemCrates1, transform.position, Quaternion.identity);
                Crates1Controller itemCratesController = itemObject.GetComponent<Crates1Controller>();
                itemCratesController.Launch(Vector2.down, 100f, lookRight);
            }
            else if (countItemCrates == 1)
            {
                GameObject itemObject = Instantiate(itemCrates2, transform.position, Quaternion.identity);
                Crates2Controller itemCratesController = itemObject.GetComponent<Crates2Controller>();
                itemCratesController.Launch(Vector2.down, 100f, lookRight);
            }
            else if (countItemCrates == 2)
            {
                GameObject itemObject = Instantiate(itemCrates3, transform.position, Quaternion.identity);
                Crates3Controller itemCratesController = itemObject.GetComponent<Crates3Controller>();
                itemCratesController.Launch(Vector2.down, 100f, lookRight);
            }
            else if (countItemCrates == 3)
            {
                GameObject itemObject = Instantiate(itemCrates4, transform.position, Quaternion.identity);
                Crates4Controller itemCratesController = itemObject.GetComponent<Crates4Controller>();
                itemCratesController.Launch(Vector2.down, 100f, lookRight);
            }
        }
    }
}
