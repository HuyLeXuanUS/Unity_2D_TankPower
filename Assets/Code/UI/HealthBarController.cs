using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public static HealthBarController instance { get; private set; }
    public Image mask;
    float originalSize;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    void Update()
    {
        
    }

    public void setHealth(float health)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * health);
    }
}
