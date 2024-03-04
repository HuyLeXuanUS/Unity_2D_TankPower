using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTN_NoQuitGame : MonoBehaviour
{
    public GameObject quitGameMenu;
    public GameObject menuInGame;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (IsMouseOverImage(mousePosition))
            {
                showOptionMenu();
                quitGameMenu.SetActive(false);
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
        menuInGame.SetActive(true);
    }
}
