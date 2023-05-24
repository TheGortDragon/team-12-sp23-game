using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrawMap : MonoBehaviour {
    private RectTransform rect;
    private int speed;

    public AudioSource theme;
    public AudioSource win;

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
        yield return new WaitForSeconds(2f);
        float volume = theme.volume;
        theme.volume = 0;
        theme.Play();
        for (float i = 0; i < volume; i += volume / 200) {
            theme.volume = i;
            yield return null;
        }
        theme.volume = volume;
    }

    public IEnumerator FadeOut() {
        theme.Stop();
        win.Play();
        float length = win.clip.length;
        yield return new WaitForSeconds(length);
        rect.localPosition = Vector3.right * Screen.width;
        rect.sizeDelta = new Vector2(Screen.width, Screen.height);
        GetComponent<AudioSource>().Play();
        for (int i = Screen.width; i > 0; i -= speed) {
            rect.localPosition -= Vector3.right * speed;
            yield return null;
        }
        rect.localPosition = Vector3.zero;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("WinScreen", LoadSceneMode.Single);
    }
}
