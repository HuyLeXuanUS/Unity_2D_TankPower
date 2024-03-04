using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPointSpeedBullet : MonoBehaviour
{
    public SpeedBulletPointController speedBulletPointController;

    void Update()
    {
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
        if (speedBulletPointController != null)
        {
            speedBulletPointController.DecreaseCount();
        }
    }
}
