using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLv2Controller : MonoBehaviour
{
    // Health
    private float baseHealth = 10000f;
    private float currentHealth = 10000f;
    
    // UI
    public GameObject UiHealthBar;
    public UnityEngine.UI.Image lightImage;
    private float timerLightEffect = 2f;

    // Count laucher
    private int laucherCount = 4;

    // Move left
    private float moveLeftSpeed = 1.34f;
    private float timerLeftSpeed = 6f;
    private bool moveLeft = false;
    private bool flagCheckIsMoveLeft = false;

    // Get Gun Boss
    public GameObject gunBoss1;
    public GameObject gunBoss2;
    public GameObject gunBoss3;
    public GameObject gunBoss4;

    // Get Main Hull
    public GameObject mainHull;

    // Effect destroy
    public GameObject destroyEffect1;
    public GameObject destroyEffect2;
    private bool destroyEffectStarted = false;
    private float destroyEffectDelay = 0f;
    private float destroyEffectTime = 5f;

    // Box collider
    private BoxCollider2D boxCollider2D;

    void Awake()
    {
        lightImage.color = new Color(lightImage.color.r, lightImage.color.g, lightImage.color.b, 0);
    }
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;

        gunBoss1.GetComponent<BoxCollider2D>().enabled = false;
        gunBoss2.GetComponent<BoxCollider2D>().enabled = false;

        mainHull.GetComponent<BoxCollider2D>().enabled = false;

        gunBoss3.SetActive(false);
        gunBoss4.SetActive(false);

        UiHealthBar.SetActive(false);
        Invoke("StartActive", 108f);
    }

    void Update()
    {
        if (laucherCount == 2 && !flagCheckIsMoveLeft)
        {
            flagCheckIsMoveLeft = true;
            mainHull.GetComponent<MainHullController>().changeLookLeft();
            gunBoss3.SetActive(true);
            gunBoss4.SetActive(true);
            moveLeft = true;
        }

        if (laucherCount == 0)
        {
            mainHull.GetComponent<MainHullController>().changeArmor();
        }

        if (moveLeft)
        {
            timerLeftSpeed -= Time.deltaTime;
            if (timerLeftSpeed >= 0)
            {
                transform.Translate(Vector3.left * moveLeftSpeed * Time.deltaTime);
            }
            else
            {
                moveLeft = false;
            }
        }

        if (destroyEffectDelay > 0)
        {
            destroyEffectDelay -= Time.deltaTime;
        }
        else
        {
            destroyEffectDelay = 0.1f;
        }

        if (destroyEffectStarted == true && destroyEffectTime > 0)
        {
            destroyEffectTime -= Time.deltaTime;
            if (destroyEffectDelay <= 0)
            {
                SpawnDestroyEffect();
                destroyEffectDelay = 0.1f;
            }
        }
        else if (destroyEffectStarted == true && destroyEffectTime <= 0)
        {
            UiHealthBar.SetActive(false);
            GetComponent<Renderer>().sortingOrder = 0;
            mainHull.GetComponent<Renderer>().sortingOrder = 0;

            if (timerLightEffect > 0)
            {
                timerLightEffect -= Time.deltaTime;
                lightImage.color = new Color(lightImage.color.r, lightImage.color.g, lightImage.color.b, timerLightEffect/2f);
            }
            else
            {
                lightImage.color = new Color(lightImage.color.r, lightImage.color.g, lightImage.color.b, 0);
                destroyEffectStarted = false;
                Destroy(gameObject);
            }
        }
    }

    public void changeHealth(float amount)
    {
        if (currentHealth > 0)
        {
            currentHealth += amount;
            BossHealthBarController.instance.setHealth(currentHealth / baseHealth);
            if (currentHealth <= 0)
            {
                boxCollider2D.enabled = false;
                destroyEffectStarted = true;
            }
        }
    }

    public void destroyLaucher()
    {
        laucherCount--;
    }

    void StartActive()
    {
        UiHealthBar.SetActive(true);
        gunBoss1.GetComponent<BoxCollider2D>().enabled = true;
        gunBoss2.GetComponent<BoxCollider2D>().enabled = true;
        mainHull.GetComponent<BoxCollider2D>().enabled = true;
    }

    void SpawnDestroyEffect()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-4f, 8f), transform.position.y + Random.Range(-1f, 2f), 0f);
        Instantiate(destroyEffect1, spawnPosition, Quaternion.identity);
        Instantiate(destroyEffect2, spawnPosition, Quaternion.identity);
    }
}
