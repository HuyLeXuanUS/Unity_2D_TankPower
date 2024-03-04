using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHullController : MonoBehaviour
{
    private float currentHealth = 6000f;
    public GameObject hellfire;
    private bool lookLeft = true;

    // Get Hull object
    public GameObject hull;

    // Check Armor main hull
    private bool armor = true;

    void Start()
    {
        InvokeRepeating("SpawnBomb", 112f, 7.5f);
    }

    void SpawnBomb()
    {
        Vector3 offset1 = new Vector3(-3.5f, -1.5f, 0);
        Vector3 offset2 = new Vector3(3.5f, -1.5f, 0);

        GameObject hellfireObject1 = Instantiate(hellfire, transform.position + offset1, Quaternion.identity);
        HellfireController hellfireController1 = hellfireObject1.GetComponent<HellfireController>();
        hellfireController1.Launch(Vector2.up, 8f, lookLeft);

        GameObject hellfireObject2 = Instantiate(hellfire, transform.position + offset2, Quaternion.identity);
        HellfireController hellfireController2 = hellfireObject2.GetComponent<HellfireController>();
        hellfireController2.Launch(Vector2.up, 8f, lookLeft);
    }

    public void changeLookLeft()
    {
        lookLeft = false;
    }

    public void changeHealth(float amount)
    {
        if (armor)
        {
            amount = -1;
        }

        if (currentHealth > 0)
        {
            currentHealth += amount;
            BossLv2Controller bossLv2Controller = hull.GetComponent<BossLv2Controller>();

            if (currentHealth <= 0)
            {
                bossLv2Controller.changeHealth(amount + currentHealth);
                bossLv2Controller.destroyLaucher();
                GetComponent<BoxCollider2D>().enabled = false;
                CancelInvoke("SpawnBomb");
            }
            else
            {
                bossLv2Controller.changeHealth(amount);
            }
        }
    }

    public void changeArmor()
    {
        armor = false;
    }
}
