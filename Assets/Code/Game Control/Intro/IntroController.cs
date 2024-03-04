using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    public SpriteRenderer missionSpriteRenderer;
    public float fadeInTime = 0.5f;   
    public float visibleTime = 1.5f;
    public float fadeOutTime = 0.5f;  

    void Start()
    {
        StartCoroutine(ShowMissionSprite());
    }

    IEnumerator ShowMissionSprite()
    {
        missionSpriteRenderer.color = new Color(1f, 1f, 1f, 0f); 
        float timer = 0f;
        while (timer < fadeInTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeInTime);
            missionSpriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(visibleTime);
        timer = 0f;
        while (timer < fadeOutTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeOutTime);
            missionSpriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }
    }
}
