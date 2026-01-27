using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public GameObject[] objects;
    public float timeSwitch = 5f;
    public float fadeDuration = 1f;

    private int currentIndex = 0;

    void Start()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == 0);
            SetAlpha(objects[i], i == 0 ? 1f : 0f);
        }

        StartCoroutine(Cycle());
    }

    IEnumerator Cycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeSwitch);

            GameObject current = objects[currentIndex];
            int nextIndex = (currentIndex + 1) % objects.Length;
            GameObject next = objects[nextIndex];

            next.SetActive(true);

            yield return StartCoroutine(Fade(current, 1f, 0f));
            yield return StartCoroutine(Fade(next, 0f, 1f));

            current.SetActive(false);
            currentIndex = nextIndex;
        }
    }

    IEnumerator Fade(GameObject obj, float from, float to)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(from, to, t / fadeDuration);
            SetAlpha(obj, a);
            yield return null;
        }

        SetAlpha(obj, to);
    }

    void SetAlpha(GameObject obj, float alpha)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        Color c = sr.color;
        c.a = alpha;
        sr.color = c;
    }
}
