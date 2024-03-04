using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class BTN_PlayAgain : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (IsMouseOverImage(mousePosition))
            {    
                playAgain();
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

    void playAgain()
    {
        GameController.Instance.resumeGame();
        GameController.Instance.setSaveCountItem(0);
        if (GameController.Instance.getCurrentLevel() == 1)
        {
            SceneManager.LoadScene("Level_1");
        }
        else
        {
            SceneManager.LoadScene("UpgradeTank");
        }
    }
}
