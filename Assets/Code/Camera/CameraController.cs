using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 1.5f;
    private bool moveCamera = false;

    public GameObject boss;
    private float timerMoveCamera = 10f;

    public GameObject outtro;
    private float fadeInTime = 0.5f;
    public GameObject nextLevelButton;

    void Start()
    {
        Invoke("StartCamera", 2f);
        Invoke("StopCamera", 105.0f);

        outtro.SetActive(false);
        nextLevelButton.SetActive(false);
    }

    void Update()
    {
        if (moveCamera)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (boss == null)
        {
            timerMoveCamera -= Time.deltaTime;
            if (timerMoveCamera >= 0)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else
            {
                outtro.SetActive(true);
                StartCoroutine(ShowOuttro());
            }
        }

    }

    void StartCamera()
    {
        moveCamera = true;
    }

    void StopCamera()
    {
        moveCamera = false;
    }

    IEnumerator ShowOuttro()
    {
        outtro.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        float timer = 0f;
        while (timer < fadeInTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeInTime);
            outtro.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        nextLevelButton.SetActive(true);
    }
}
