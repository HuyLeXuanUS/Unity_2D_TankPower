using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next_Level : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (IsMouseOverImage(mousePosition))
            {
                loadUpgradeScene();
            }
        }
    }

    void loadUpgradeScene()
    {
        GameController.Instance.increaseLevel();
        GameController.Instance.setStartUpgrade();
        String currentLevelString = "UpgradeTank";
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentLevelString, LoadSceneMode.Single);
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
