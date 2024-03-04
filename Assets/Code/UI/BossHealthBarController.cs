using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BossHealthBarController : MonoBehaviour
{
    public static BossHealthBarController instance { get; private set; }
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

    public void setHealth(float health)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * health);
    }
}
