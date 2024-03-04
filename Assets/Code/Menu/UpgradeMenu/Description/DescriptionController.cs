using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionController : MonoBehaviour
{
    public UnityEngine.UI.Image[] imageCount;
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

    public void ChangeCount(int value)
    {
        count = value;
        if (count >= 0 && count < imageCount.Length)
        {
            for (int i = 0; i < imageCount.Length; i++)
            {
                imageCount[i].gameObject.SetActive(i == count);
            }
        }
    }
}
