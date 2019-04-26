using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Score pop up support
/// </summary>
public class FloatingScore : MonoBehaviour
{
    Text pickUpScore;

    public float fadeTime = 2.0f;
    public float speed = 2.0f;

    void Start()
    {
        pickUpScore = this.GetComponent<Text>();
        StartCoroutine(Fade());
    }

    public IEnumerator Fade()
    {

        float fadeSpeed = (float)1.0 / fadeTime;
        Color c = pickUpScore.color;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime * fadeSpeed)
        {
            c.a = Mathf.Lerp(1, 0, t);
            pickUpScore.color = c;
            yield return true;
        }

        Destroy(this.gameObject);
    }

    void Update()
    {
        this.transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

}
