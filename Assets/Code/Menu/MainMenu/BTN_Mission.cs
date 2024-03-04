using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BTN_Mission : MonoBehaviour
{
    public GameObject effectPrefab;
    public GameObject missionMenu;
    public GameObject mainMenu;

    void Start()
    {
        if (effectPrefab != null)
        {
            effectPrefab.SetActive(false);
        }
        if (missionMenu != null)
        {
            missionMenu.SetActive(false);
        }
        GameController.Instance.loadGame();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (IsMouseOverImage(mousePosition))
            {
                if (GameController.Instance.getCurrentLevel() == 1)
                {
                    SceneManager.LoadScene("Level_1", LoadSceneMode.Single);
                }
                else
                {
                    effectPrefab.SetActive(false);
                    loadMission();
                }
            }
        }
    }

    void OnMouseEnter()
    {
        if (effectPrefab != null)
        {
            effectPrefab.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        if (effectPrefab != null)
        {
            effectPrefab.SetActive(false);
        }
    }

    bool IsMouseOverImage(Vector2 position)
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider != null && collider.OverlapPoint(position))
        {
            return true;
        }
        return false;
    }

    void loadMission()
    {
        if (missionMenu != null)
        {
            MainMenu mainMenuScript = mainMenu.GetComponent<MainMenu>();
            mainMenuScript.HideButton();
            missionMenu.SetActive(true);
        }
    }
}
