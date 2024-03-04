using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonMenuInGame : MonoBehaviour
{
    public GameObject buttonMenuInGame;
    public GameObject menuInGame;

    public GameObject menuOption;
    public GameObject menuQuitGame;

    void Start()
    {
        if (buttonMenuInGame != null)
        {
            buttonMenuInGame.SetActive(true);
        }
        if (menuInGame != null)
        {
            menuInGame.SetActive(false);
        }
        if (menuOption != null)
        {
            menuOption.SetActive(false);
        }
        if (menuQuitGame != null)
        {
            menuQuitGame.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (IsMouseOverImage(mousePosition))
            {
                loadMenuIngame();
                buttonMenuInGame.SetActive(false);
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

    void loadMenuIngame()
    {
        GameController.Instance.pauseGame();
        menuInGame.SetActive(true);
    }
}
