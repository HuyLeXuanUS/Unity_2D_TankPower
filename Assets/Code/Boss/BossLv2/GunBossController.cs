using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GunBossController : MonoBehaviour
{
    public GameObject bombfrag;
    private float currentHealth = 1000f;

    // Effect destroy
    public GameObject destroyEffect;

    // Get Hull object
    public GameObject hull;

    void Start()
    {
        InvokeRepeating("SpawnBomb", 110f, 3.5f);
    }

    void SpawnBomb()
    {
        Vector3 offset1 = new Vector3(-1f, 0.8f, 0);
        Vector3 offset2 = new Vector3(-0.5f, 0.8f, 0);

        GameObject bombfragObject1 = Instantiate(bombfrag, transform.position + offset1, Quaternion.identity);
        GameObject bombfragObject2 = Instantiate(bombfrag, transform.position + offset2, Quaternion.identity);

        BombfragController bombfragController1 = bombfragObject1.GetComponent<BombfragController>();
        BombfragController bombfragController2 = bombfragObject2.GetComponent<BombfragController>();

        bombfragController1.BossLaunch(Vector2.up, 8f);
        bombfragController2.BossLaunch(Vector2.up, 8f);
    }

    public void changeHealth(float amount)
    {
        if (currentHealth > 0)
        {
            currentHealth += amount;
            BossLv2Controller bossLv2Controller = hull.GetComponent<BossLv2Controller>();

            if (currentHealth <= 0)
            {
                bossLv2Controller.changeHealth(amount + currentHealth);
                bossLv2Controller.destroyLaucher();
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                bossLv2Controller.changeHealth(amount);
            }
        }
    }

    public float getHealth()
    {
        return currentHealth;
    }
}
