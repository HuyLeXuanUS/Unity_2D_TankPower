using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_Level : MonoBehaviour
{
    public GameObject pointUpgradeGuard;
    public GameObject pointUpgradeDamageBullet;
    public GameObject pointUpgradeDamageBeamFire;
    public GameObject pointUpgradeQuantityBullet;
    public GameObject pointUpgradeSpeedTank;
    public GameObject pointUpgradeSpeedBullet;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (IsMouseOverImage(mousePosition))
            {
                loadLevel();
            }
        }
    }

    void loadLevel()
    {
        GameController.Instance.setUpgradeGuard(pointUpgradeGuard.GetComponent<GuardPointController>().getCount());
        GameController.Instance.setUpgradeDamageBullet(pointUpgradeDamageBullet.GetComponent<DamageBulletPointController>().getCount());
        GameController.Instance.setUpgradeDamageBeamFire(pointUpgradeDamageBeamFire.GetComponent<DamageBeamFirePointController>().getCount());
        GameController.Instance.setUpgradeQuantityBullet(pointUpgradeQuantityBullet.GetComponent<QuantityBulletPointController>().getCount());
        GameController.Instance.setUpgradeSpeedTank(pointUpgradeSpeedTank.GetComponent<SpeedTankPointController>().getCount());
        GameController.Instance.setUpgradeSpeedBullet(pointUpgradeSpeedBullet.GetComponent<SpeedBulletPointController>().getCount());
        
        String currentLevelString = "Level_" + GameController.Instance.getCurrentLevel();
        SceneManager.LoadScene(currentLevelString, LoadSceneMode.Single);
    }

    bool IsMouseOverImage(Vector2 position)
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider != null && collider.OverlapPoint(position))
        {
            return true;
        }
        return false;
    }
}
