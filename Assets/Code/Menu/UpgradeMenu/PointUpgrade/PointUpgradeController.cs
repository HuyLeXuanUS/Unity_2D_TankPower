using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUpgradeController : MonoBehaviour
{
    private int points = 0;

    void Start()
    {
        points = GameController.Instance.getPointUpgrade();
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 35;
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(500, 650, 100, 20), "Upgrade Points: " + points, style);
    }

    public void IncreaseCount()
    {
        points++;
    }

    public void DecreaseCount()
    {
        if (points > 0)
        {
            points--;
        }
    }

    public int getPoints()
    {
        return points;
    }
}
