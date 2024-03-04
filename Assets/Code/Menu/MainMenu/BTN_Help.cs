using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTN_Help : MonoBehaviour
{
    public GameObject effectPrefab; 
    public GameObject helpMenu;
    public GameObject mainMenu;

    void Start()
    {
        if (effectPrefab != null)
        {
            effectPrefab.SetActive(false);
        }
        if (helpMenu != null)
        {
            helpMenu.SetActive(false); 
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
                loadHelp();
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

    void loadHelp()
    {
        if (helpMenu != null)
        {
            MainMenu mainMenuScript = mainMenu.GetComponent<MainMenu>();
            mainMenuScript.HideButton();
            helpMenu.SetActive(true);
        }
    }
}
