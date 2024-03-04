using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTN_OptineIngame : MonoBehaviour
{
    public GameObject menuOption;
    public GameObject menuInGame;

    void Start()
    {
        if (menuOption != null)
        {
            menuOption.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (IsMouseOverImage(mousePosition))
            {
                showOptionMenu();
                menuInGame.SetActive(false);
            }
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

    void showOptionMenu()
    {
        menuOption.SetActive(true);
    }
}
