using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndingCredits : MonoBehaviour
{
    [SerializeField] private RectTransform textTransform;

    [SerializeField] private float scrollSpeed = 50f;

    [SerializeField] private float endY = 2000f;


    private void Update()
    {
        textTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
    }
}