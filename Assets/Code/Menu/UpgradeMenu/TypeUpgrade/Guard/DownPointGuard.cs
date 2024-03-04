using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPointGuard : MonoBehaviour
{
    public GuardPointController guardPointController;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (IsMouseOverImage(mousePosition))
            {
                DownPoint();
            }
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

    void DownPoint()
    {
        if (guardPointController != null)
        {
            guardPointController.DecreaseCount();
        }
    }
}
