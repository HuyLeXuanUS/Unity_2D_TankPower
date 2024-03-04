using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    void Update()
    {
        if (GameController.Instance.isPause())
        {
            return;
        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        angle = RemapAngle(angle);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    float RemapAngle(float angle)
    {
        if (angle < -90f && angle >= -180f)
        {
            return -90f;
        }
        else if (angle < -180f)
        {
            return 90f;
        }
        else
        {
            return angle;
        }
    }
}
