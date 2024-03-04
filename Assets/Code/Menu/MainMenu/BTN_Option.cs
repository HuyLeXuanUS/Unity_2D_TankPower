using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BTN_Option : MonoBehaviour
{
    public GameObject effectPrefab;
    public GameObject optionMenu;
    
    public GameObject mainMenu;  
    void Start()
    {
        if (effectPrefab != null)
        {
            effectPrefab.SetActive(false);
        }
        if (optionMenu != null)
        {
            optionMenu.SetActive(false); 
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (IsMouseOverImage(mousePosition))
            {
                effectPrefab.SetActive(false);
                loadOption();
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
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider != null && collider.OverlapPoint(position))
        {
            return true;
        }
        return false;
    }

    void loadOption()
    {
        if (optionMenu != null)
        {
            MainMenu mainMenuScript = mainMenu.GetComponent<MainMenu>();
            mainMenuScript.HideButton();
            optionMenu.SetActive(true);
        }
    }
}
