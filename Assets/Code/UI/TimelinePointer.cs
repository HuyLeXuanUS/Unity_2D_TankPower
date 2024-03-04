using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimelinePointer : MonoBehaviour
{
    public Image pointer;
    public RectTransform virtualTimeline;
    public float moveDuration = 103f;
    public float delayBeforeMove = 2f; 

    void Start()
    {
        StartCoroutine(MovePointer());
    }

    IEnumerator MovePointer()
    {
        yield return new WaitForSeconds(delayBeforeMove);

        float startTime = Time.time;
        float normalizedTime = 0f;

        while (normalizedTime <= 1f)
        {
            float currentTime = Time.time;
            normalizedTime = (currentTime - startTime) / moveDuration;
            float newXPosition = Mathf.Lerp(0f, virtualTimeline.rect.width/2, normalizedTime);
            pointer.rectTransform.localPosition = new Vector3(newXPosition, 0f, 0f);

            yield return null;
        }

        pointer.rectTransform.localPosition = new Vector3(virtualTimeline.rect.width/2, 0f, 0f);
    }
}