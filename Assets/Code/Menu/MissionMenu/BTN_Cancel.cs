using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTN_Cancel : MonoBehaviour
{
    public GameObject missionMenu;
    public GameObject mainMenu;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (IsMouseOverImage(mousePosition))
            {
                Cancel();
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

    void Cancel()
    {
        missionMenu.SetActive(false);
        MainMenu mainMenuScript = mainMenu.GetComponent<MainMenu>();
        mainMenuScript.ShowButton();
    }
}
