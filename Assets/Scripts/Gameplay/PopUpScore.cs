using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Points (+ or - ) pop up on UI upon collision
/// </summary>
public class PopUpScore : MonoBehaviour
{
    [SerializeField]
    Text score;

    public float fadeDuration = 2.0f;
    public float speed = 2.0f;

    void Start()
    {
        //print("POPUP");
        float add = Score.getCurrent;
        score.text = add.ToString();
        //score.text = "POP";
        StartCoroutine(Fade());
    }

    public IEnumerator Fade()
    {

        float fadeSpeed = (float)1.0 / fadeDuration;
        Color c = score.color;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime * fadeSpeed)
        {
            c.a = Mathf.Lerp(1, 0, t);
            score.color = c;
            yield return true;
        }

        Destroy(this.gameObject);
    }

    void Update()
    {
        this.transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

}
