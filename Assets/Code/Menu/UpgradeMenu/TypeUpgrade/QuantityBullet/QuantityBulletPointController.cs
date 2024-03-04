using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantityBulletPointController : MonoBehaviour
{
    public UnityEngine.UI.Image[] imageCount;
    public GameObject pointUpgrade;
    private int count = 0;

    void Start()
    {
        for (int i = 0; i < imageCount.Length; i++)
        {
            if(i == count)
            {
                imageCount[i].gameObject.SetActive(true);
            }
            else
            {
                imageCount[i].gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < imageCount.Length; i++)
        {
            if(i == count)
            {
                imageCount[i].gameObject.SetActive(true);
            }
            else
            {
                imageCount[i].gameObject.SetActive(false);
            }
        }
    }

    public void IncreaseCount()
    {
        int check = pointUpgrade.GetComponent<PointUpgradeController>().getPoints();
        if(count >= 3 || check == 0)
        {
            return;
        }
        count++;
        pointUpgrade.GetComponent<PointUpgradeController>().DecreaseCount();
        if (count >= 0 && count < imageCount.Length)
        {
            for (int i = 0; i < imageCount.Length; i++)
            {
                imageCount[i].gameObject.SetActive(i == count);
            }
        }
    }

    public void DecreaseCount()
    {
        if(count <= 0)
        {
            return;
        }
        count--;
        pointUpgrade.GetComponent<PointUpgradeController>().IncreaseCount();
        if (count >= 0 && count < imageCount.Length)
        {
            for (int i = 0; i < imageCount.Length; i++)
            {
                imageCount[i].gameObject.SetActive(i == count);
            }
        }
    }

    public int getCount()
    {
        return count;
    }
}
