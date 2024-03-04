using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLv3Controller : MonoBehaviour
{

    private BoxCollider2D boxCollider2D;
    public float downwardSpeed = 2f;
    public float figureEightSpeed = 1f;
    public float transitionSpeed = 2f; 
    private bool startFigureEight = false;
    private bool startMove = false;

    private float baseHealth = 20000f;
    private float currentHealth = 20000f;

    public GameObject destroyEffect1;
    public GameObject destroyEffect2;
    private float destroyEffectDelay = 0f;
    private float destroyEffectTime = 5f;
    private bool destroyEffectStarted = false;
    public GameObject UiHealthBar;
    public UnityEngine.UI.Image lightImage;
    private float timerLightEffect = 2f;

    // Enemy weapon
    public GameObject hellfire;
    public GameObject bombfragPrefab;

    void Awake()
    {
        lightImage.color = new Color(lightImage.color.r, lightImage.color.g, lightImage.color.b, 0);
    }

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
        UiHealthBar.SetActive(false);
        Invoke("StartFigureEight", 108f);
        InvokeRepeating("SpawnHellfire", 112f, 4f);
        InvokeRepeating("SpawnBomb", 113f, 6f);
    }

    void Update()
    {
        if (startMove)
        {
            UiHealthBar.SetActive(true);
            transform.Translate(Vector3.down * downwardSpeed * Time.deltaTime);
            if (transform.position.y <= 3f && !startFigureEight)
            {
                startFigureEight = true;
            }

            if (startFigureEight)
            {
                boxCollider2D.enabled = true;
                float x = Mathf.Sin(Time.time * figureEightSpeed) * 2f + 155f;
                float y = Mathf.Cos(Time.time * figureEightSpeed * 2f) * 1f + 3f; 

                Vector3 targetPosition = new Vector3(x, y, 0f);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
            }
        }

        if (destroyEffectDelay > 0)
        {
            destroyEffectDelay -= Time.deltaTime;
        }
        else
        {
            destroyEffectDelay = 0.15f;
        }

        if (destroyEffectStarted == true && destroyEffectTime > 0)
        {
            destroyEffectTime -= Time.deltaTime;
            if (destroyEffectDelay <= 0)
            {
                SpawnDestroyEffect();
                destroyEffectDelay = 0.15f;
            }
        }
        else if (destroyEffectStarted == true && destroyEffectTime <= 0)
        {
            UiHealthBar.SetActive(false);
            GetComponent<Renderer>().sortingOrder = 0;
            
            foreach (Transform child in transform)
            {
                Renderer childRenderer = child.GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    childRenderer.sortingOrder = 0;
                }
            }

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

     void StartFigureEight()
    {
        startMove = true;
    }

    public void changeHealth(float amount)
    {
        if (currentHealth > 0)
        {
            currentHealth += amount;
            BossHealthBarController.instance.setHealth(currentHealth / baseHealth);
            if (currentHealth <= 0)
            {
                stopSpawn();
                boxCollider2D.enabled = false;
                destroyEffectStarted = true;
            }
        }
    }

    void SpawnDestroyEffect()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-3.5f, 3.5f), transform.position.y + Random.Range(-1f, 1f), 0f);
        Instantiate(destroyEffect1, spawnPosition, Quaternion.identity);
        Instantiate(destroyEffect2, spawnPosition, Quaternion.identity);
    }

    void SpawnHellfire()
    {
        Vector3 offset1 = new Vector3(-3, 0f, 0f);
        Vector3 offset2 = new Vector3(3, 0f, 0f);

        GameObject hellfireObject1 = Instantiate(hellfire, transform.position + offset1, Quaternion.identity);
        HellfireController hellfireController1 = hellfireObject1.GetComponent<HellfireController>();
        hellfireController1.Launch(Vector2.up, 8f, true);

        GameObject hellfireObject2 = Instantiate(hellfire, transform.position + offset2, Quaternion.identity);
        HellfireController hellfireController2 = hellfireObject2.GetComponent<HellfireController>();
        hellfireController2.Launch(Vector2.up, 8f, false);
    }

    void SpawnBomb()
    {
        int numberOfBombFrags = 5;
        float angleStep = 25f;

        for (int i = 0; i < numberOfBombFrags; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle - 50f) * Vector2.down;

            GameObject bombfrag = Instantiate(bombfragPrefab, transform.position, Quaternion.identity);
            BombfragController bombfragController = bombfrag.GetComponent<BombfragController>();
            bombfragController.Launch(direction.normalized, 200.0f);
        }
    }

    void stopSpawn()
    {
        CancelInvoke("SpawnHellfire");
        CancelInvoke("SpawnBomb");
    }
}
