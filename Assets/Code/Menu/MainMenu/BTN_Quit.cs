using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTN_Quit : MonoBehaviour
{
    public GameObject effectPrefab;  
    void Start()
    {
        if (effectPrefab != null)
        {
            effectPrefab.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (IsMouseOverImage(mousePosition))
            {
                QuitGame();
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

    void QuitGame()
    {
        GameController.Instance.saveGame();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
