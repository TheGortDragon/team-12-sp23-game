using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrawMap : MonoBehaviour {
    private RectTransform rect;
    private int speed;

    void Start() {
        rect = GetComponent<RectTransform>();
        StartCoroutine(FadeIn());
        speed = 20;
    }

    public IEnumerator FadeIn() {
        rect.localPosition = Vector3.zero;
        rect.sizeDelta = new Vector2(Screen.width, Screen.height);
        GetComponent<AudioSource>().Play();
        for (int i = 0; i < Screen.width; i+= speed) {
            rect.localPosition += Vector3.right * speed;
            yield return null;
        }
        rect.localPosition = Vector3.right * (Screen.width * .98f);
    }

    public IEnumerator FadeOut() {
        rect.localPosition = Vector3.right * Screen.width;
        rect.sizeDelta = new Vector2(Screen.width, Screen.height);
        GetComponent<AudioSource>().Play();
        for (int i = Screen.width; i > 0; i -= speed) {
            rect.localPosition -= Vector3.right * speed;
            yield return null;
        }
        rect.localPosition = Vector3.zero;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
