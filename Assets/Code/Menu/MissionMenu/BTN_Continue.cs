using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class BTN_Continue : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (IsMouseOverImage(mousePosition))
            {
                continueGame();
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

    void continueGame()
    {
        SceneManager.LoadScene("UpgradeTank", LoadSceneMode.Single);
    }
}
