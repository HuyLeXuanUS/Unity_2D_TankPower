using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEngine.UI;

public class TankController : MonoBehaviour
{
    // Camera
    private Camera mainCamera;
    private float cameraHalfWidth;

    // Tank
    public float baseSpeed = 5.0f;
    private float speed;

    private float baseHealth = 100.0f;
    private float currentHealth;

    private float baseArmor = 0f;
    private float virualArmor = 0f;
    private float maxArmor = 100f;

    private float virualArmorStart = 30f;
    private float timeAmorStart = 5f;

    private Rigidbody2D rbTank;
    Animator animatorTank;

    private int countLife = 2;
    public UnityEngine.UI.Image[] imagesLife;

    // Moving
    float horizontalInput = 0;
    Vector2 lookDirection = new Vector2(1, 0);

    // Bullet
    public GameObject bulletPrefab;
    private int countBullet = 1;
    private float BulletperSecond = 8f;
    private float damageBullet = 100.0f;
    private float damageBeamfire = 50.0f;
    private Vector2 lookDirectionBullet = new Vector2(0, 0);
    private float timerBullet = 0.0f;
    private float speedBullet = 800.0f;

    // Beamfire
    public GameObject beamfirePrefab;
    private float BeamfireperSecond = 1000f;
    private float timerBeamfire = 0.0f;
    private float speedBeamfire = 1500.0f;
    private int countItemBeamfire = 0;
    private float timerCheckBeamfire = 0.0f;
    public UnityEngine.UI.Image[] imagesLaser;

    // Check weapon
    private bool isBeamfire = false;

    // Sprite
    public SpriteRenderer spriteArmor;

    // Effect
    public GameObject destroyEffect;

    // Upgrade point
    private int upgradeGuard = 0;
    private int upgradeDamageBullet = 0;
    private int upgradeDamageBeamFire = 0;
    private int upgradeQuantityBullet = 0;
    private int upgradeSpeedTank = 0;
    private int upgradeSpeedBullet = 0;

    // Auido----------------
    AudioSource audioSource;
    public AudioClip audioShot;

    // Play again UI
    public GameObject playAgainUI;
    public GameObject menuButton;

    void Awake()
    {
        // Get upgrade point
        upgradeGuard = GameController.Instance.getUpgradeGuard();
        upgradeDamageBullet = GameController.Instance.getUpgradeDamageBullet();
        upgradeDamageBeamFire = GameController.Instance.getUpgradeDamageBeamFire();
        upgradeQuantityBullet = GameController.Instance.getUpgradeQuantityBullet();
        upgradeSpeedTank = GameController.Instance.getUpgradeSpeedTank();
        upgradeSpeedBullet = GameController.Instance.getUpgradeSpeedBullet();

        countItemBeamfire = GameController.Instance.getSaveCountItem();
    }

    void Start()
    {
        rbTank = GetComponent<Rigidbody2D>();
        animatorTank = GetComponent<Animator>();
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        currentHealth = baseHealth;
        virualArmor = virualArmorStart;

        // Set upgrade point
        baseArmor += upgradeGuard * 5;
        damageBullet += upgradeDamageBullet * 5;
        damageBeamfire += upgradeDamageBeamFire * 5;
        countBullet += upgradeQuantityBullet;
        speed += upgradeSpeedTank;
        BulletperSecond += upgradeSpeedBullet * 2f;

        audioSource = GetComponent<AudioSource>();
        playAgainUI.SetActive(false);
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0);

        if(!Mathf.Approximately(moveDirection.x, 0))
        {
            lookDirection.Set(moveDirection.x, 0);
            lookDirection.Normalize();
        }

        if (lookDirection.x < 0)
        {
            speed = baseSpeed - 0.75f;
        }
        else
        {
            speed = baseSpeed + 0.75f;
        }

        animatorTank.SetFloat("Look X", lookDirection.x);
        animatorTank.SetFloat("Speed", moveDirection.magnitude);

        timerBullet -= Time.deltaTime;
        timerBeamfire -= Time.deltaTime;

        if (timerCheckBeamfire >= 0)
        {
            isBeamfire = true;
            timerCheckBeamfire -= Time.deltaTime;

            if (timerCheckBeamfire <= 3.75)
            {
                imagesLaser[0].color = new Color(imagesLaser[0].color.r, imagesLaser[0].color.g, imagesLaser[0].color.b, timerCheckBeamfire / 3.75f);
            }
            else if (timerCheckBeamfire <= 7.5)
            {
                imagesLaser[1].color = new Color(imagesLaser[1].color.r, imagesLaser[1].color.g, imagesLaser[1].color.b, (timerCheckBeamfire - 3.75f) / 3.75f);
            }
            else if (timerCheckBeamfire <= 11.25)
            {
                imagesLaser[2].color = new Color(imagesLaser[2].color.r, imagesLaser[2].color.g, imagesLaser[2].color.b, (timerCheckBeamfire - 7.5f) / 3.75f);
            }
            else
            {
                imagesLaser[3].color = new Color(imagesLaser[3].color.r, imagesLaser[3].color.g, imagesLaser[3].color.b, (timerCheckBeamfire - 11.25f) / 3.75f);
            }
        }
        else
        {
            isBeamfire = false;
            for (int i = imagesLaser.Length - 1; i >= countItemBeamfire; i--)
            {
                imagesLaser[i].color = new Color(imagesLaser[i].color.r, imagesLaser[i].color.g, imagesLaser[i].color.b, 0f);
            }
            for (int i = 0; i < countItemBeamfire; i++)
            {
                imagesLaser[i].color = new Color(imagesLaser[i].color.r, imagesLaser[i].color.g, imagesLaser[i].color.b, 1f);
            }
        }

        for (int i = 0; i < imagesLife.Length; i++)
        {
            if (i < countLife)
            {
                imagesLife[i].color = new Color(imagesLife[i].color.r, imagesLife[i].color.g, imagesLife[i].color.b, 1f);
            }
            else
            {
                imagesLife[i].color = new Color(imagesLife[i].color.r, imagesLife[i].color.g, imagesLife[i].color.b, 0f);
            }
        }

        if (Input.GetMouseButton(0)) 
        {
            if (!isBeamfire)
            {
                float speedShot = 1.0f / BulletperSecond;
                if (timerBullet <= 0)
                {
                    audioSource.PlayOneShot(audioShot);
                    timerBullet = speedShot;
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 objectPosition = transform.position;
                    lookDirectionBullet = mousePosition - objectPosition;

                    if(lookDirectionBullet.y < 0)
                    {
                        lookDirectionBullet.Set(lookDirectionBullet.x, 0);
                    }

                    lookDirectionBullet.Normalize();
                    Launch(countBullet, damageBullet, lookDirectionBullet);
                }
            }
            else
            {
                float speedShot = 1.0f / BeamfireperSecond;
                if (timerBeamfire <= 0)
                {
                    timerBeamfire = speedShot;
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 objectPosition = transform.position;
                    lookDirectionBullet = mousePosition - objectPosition;

                    if(lookDirectionBullet.y < 0)
                    {
                        lookDirectionBullet.Set(lookDirectionBullet.x, 0);
                    }

                    lookDirectionBullet.Normalize();
                    LaunchBeamfire();
                }
            }
        }

        if (virualArmor > 0)
        {
            spriteArmor.enabled = true;
        }
        else
        {
            spriteArmor.enabled = false;
        }

        if (timeAmorStart > 0)
        {
            timeAmorStart -= Time.deltaTime;
            if (timeAmorStart <= 0)
            {
                virualArmor -= virualArmorStart;
            }
        }
        GameController.Instance.setSaveCountItem(countItemBeamfire);
    }

    void FixedUpdate()
    {
        MoveTank();
    }

    void Launch(int count, float damage, Vector2 lookDirectionBullet)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        angle = RemapAngle(angle);

        if (count == 1)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle)));
            BulletController bullet = bulletObject.GetComponent<BulletController>();
            bullet.Launch(lookDirectionBullet, speedBullet, damage);
        }
        else if (count == 2)
        {
            GameObject bulletObject1 = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle + 5f)));
            GameObject bulletObject2 = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle - 5f)));

            BulletController bullet1 = bulletObject1.GetComponent<BulletController>();
            BulletController bullet2 = bulletObject2.GetComponent<BulletController>();

            Quaternion rotationPositive = Quaternion.Euler(0.0f, 0.0f, 5f);
            Quaternion rotationNegative = Quaternion.Euler(0.0f, 0.0f, -5f);

            Vector2 newLookDirectionPositive = rotationPositive * lookDirectionBullet;
            Vector2 newLookDirectionNegative = rotationNegative * lookDirectionBullet;

            bullet1.Launch(newLookDirectionPositive, speedBullet, damage);
            bullet2.Launch(newLookDirectionNegative, speedBullet, damage);
        }
        else if (count == 3)
        {
            GameObject bulletObject1 = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle + 8f)));
            GameObject bulletObject2 = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle)));
            GameObject bulletObject3 = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle - 8f)));

            BulletController bullet1 = bulletObject1.GetComponent<BulletController>();
            BulletController bullet2 = bulletObject2.GetComponent<BulletController>();
            BulletController bullet3 = bulletObject3.GetComponent<BulletController>();

            Quaternion rotationPositive = Quaternion.Euler(0.0f, 0.0f, 8f);
            Quaternion rotationNegative = Quaternion.Euler(0.0f, 0.0f, -8f);

            Vector2 newLookDirectionPositive = rotationPositive * lookDirectionBullet;
            Vector2 newLookDirectionNegative = rotationNegative * lookDirectionBullet;

            bullet1.Launch(newLookDirectionPositive, speedBullet, damage);
            bullet2.Launch(lookDirectionBullet, speedBullet, damage);
            bullet3.Launch(newLookDirectionNegative, speedBullet, damage);
        }
        else if (count == 4)
        {
            GameObject bulletObject1 = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle - 15)));
            GameObject bulletObject2 = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle - 5)));
            GameObject bulletObject3 = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle + 5)));
            GameObject bulletObject4 = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle + 15f)));

            BulletController bullet1 = bulletObject1.GetComponent<BulletController>();
            BulletController bullet2 = bulletObject2.GetComponent<BulletController>();
            BulletController bullet3 = bulletObject3.GetComponent<BulletController>();
            BulletController bullet4 = bulletObject4.GetComponent<BulletController>();

            Quaternion rotation1 = Quaternion.Euler(0.0f, 0.0f, -15f);
            Quaternion rotation2 = Quaternion.Euler(0.0f, 0.0f, -5f);
            Quaternion rotation3 = Quaternion.Euler(0.0f, 0.0f, 5f);
            Quaternion rotation4 = Quaternion.Euler(0.0f, 0.0f, 15f);


            Vector2 newLookDirection1 = rotation1 * lookDirectionBullet;
            Vector2 newLookDirection2 = rotation2 * lookDirectionBullet;
            Vector2 newLookDirection3 = rotation3 * lookDirectionBullet;
            Vector2 newLookDirection4 = rotation4 * lookDirectionBullet;

            bullet1.Launch(newLookDirection1, speedBullet, damage);
            bullet2.Launch(newLookDirection2, speedBullet, damage);
            bullet3.Launch(newLookDirection3, speedBullet, damage);
            bullet4.Launch(newLookDirection4, speedBullet, damage);
        }
        else if (count == 5)
        {
            GameObject bulletObject1 = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle - 16)));
            GameObject bulletObject2 = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle - 8)));
            GameObject bulletObject3 = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle)));
            GameObject bulletObject4 = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle + 8)));
            GameObject bulletObject5 = Instantiate(bulletPrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle + 16)));

            BulletController bullet1 = bulletObject1.GetComponent<BulletController>();
            BulletController bullet2 = bulletObject2.GetComponent<BulletController>();
            BulletController bullet3 = bulletObject3.GetComponent<BulletController>();
            BulletController bullet4 = bulletObject4.GetComponent<BulletController>();
            BulletController bullet5 = bulletObject5.GetComponent<BulletController>();

            Quaternion rotation1 = Quaternion.Euler(0.0f, 0.0f, -16f);
            Quaternion rotation2 = Quaternion.Euler(0.0f, 0.0f, -8f);
            Quaternion rotation3 = Quaternion.Euler(0.0f, 0.0f, 8f);
            Quaternion rotation4 = Quaternion.Euler(0.0f, 0.0f, 16f);


            Vector2 newLookDirection1 = rotation1 * lookDirectionBullet;
            Vector2 newLookDirection2 = rotation2 * lookDirectionBullet;
            Vector2 newLookDirection3 = rotation3 * lookDirectionBullet;
            Vector2 newLookDirection4 = rotation4 * lookDirectionBullet;

            bullet1.Launch(newLookDirection1, speedBullet, damage);
            bullet2.Launch(newLookDirection2, speedBullet, damage);
            bullet3.Launch(newLookDirection3, speedBullet, damage);
            bullet4.Launch(newLookDirection4, speedBullet, damage);
            bullet5.Launch(lookDirectionBullet, speedBullet, damage);
        }
    }

    void LaunchBeamfire()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        angle = RemapAngle(angle);

        GameObject beamfireObject = Instantiate(beamfirePrefab, rbTank.position + lookDirectionBullet.normalized * 0.8f + Vector2.up * 0.4f, Quaternion.Euler(new Vector3(0, 0, angle)));
        BeamfireController beamfire = beamfireObject.GetComponent<BeamfireController>();
        beamfire.Launch(lookDirectionBullet, speedBeamfire, damageBeamfire);
    }

    public int getCountItemBeamfire()
    {
        return countItemBeamfire;
    }
 
    float RemapAngle(float angle)
    {
        if (angle < -90f && angle >= -180f)
        {
            return -90f;
        }
        else if (angle < -180f)
        {
            return 90f;
        }
        else
        {
            return angle;
        }
    }

    void MoveTank()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        float maxX = mainCamera.transform.position.x + cameraHalfWidth;
        float minX = mainCamera.transform.position.x - cameraHalfWidth;

        float newObjectX = Mathf.Clamp(transform.position.x + horizontalInput * speed * Time.deltaTime, minX, maxX);
        transform.position = new Vector3(newObjectX, transform.position.y, transform.position.z);
    }

    public void changeHealth(float amount)
    {
        float damage = amount - baseArmor;
        if (virualArmor > 0)
        {
            virualArmor -= damage;
            if (virualArmor < 0)
            {
                currentHealth += virualArmor;
                virualArmor = 0;
            }
        }
        else
        {
            currentHealth -= damage;
        }

        HealthBarController.instance.setHealth(currentHealth / baseHealth);

        if (currentHealth <= 0)
        {
            if (countLife > 0)
            {

                Instantiate(destroyEffect, transform.position, Quaternion.identity);               
                countLife -= 1;
                currentHealth = baseHealth;
                HealthBarController.instance.setHealth(currentHealth / baseHealth);

                virualArmor = virualArmorStart;
                timeAmorStart = 5f;
            }
            else
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
                GameController.Instance.pauseGame();
                playAgainUI.SetActive(true);
                menuButton.SetActive(false);
                Destroy(gameObject);
                // End level
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ItemArmorController itemArmorController = other.GetComponent<ItemArmorController>();
        ItemUpDamageController itemUpDamageController = other.GetComponent<ItemUpDamageController>();
        ItemCountBulletController itemCountBulletController = other.GetComponent<ItemCountBulletController>();

        Crates1Controller crates1Controller = other.GetComponent<Crates1Controller>();
        Crates2Controller crates2Controller = other.GetComponent<Crates2Controller>();
        Crates3Controller crates3Controller = other.GetComponent<Crates3Controller>();
        Crates4Controller crates4Controller = other.GetComponent<Crates4Controller>();

        if (itemArmorController != null)
        {
            getArmor(itemArmorController.armor);
            Destroy(other.gameObject);
        }

        if (itemUpDamageController != null)
        {
            damageBullet += itemUpDamageController.damage;
            Destroy(other.gameObject);
        }

        if (itemCountBulletController != null)
        {
            countBullet += 1;
            if (countBullet > 5)
            {
                damageBullet += 5f;
                countBullet = 5;
            }
            Destroy(other.gameObject);
        }

        if (crates1Controller != null)
        {
            countItemBeamfire = 1;
            Destroy(other.gameObject);
        }

        if (crates2Controller != null)
        {
            countItemBeamfire = 2;
            Destroy(other.gameObject);
        }

        if (crates3Controller != null)
        {
            countItemBeamfire = 3;
            Destroy(other.gameObject);
        }

        if (crates4Controller != null)
        {
            countItemBeamfire = 0;
            timerCheckBeamfire = 15f;
            Destroy(other.gameObject);
        }
    }

    private void getArmor(float amount)
    {
        virualArmor += amount;
        if (virualArmor > maxArmor)
        {
            virualArmor = maxArmor;
        }
    }
}
