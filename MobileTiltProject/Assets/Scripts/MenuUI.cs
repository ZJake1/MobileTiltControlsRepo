using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject fade;

    int nextScene = -1;

    void Start()
    {
        fade.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
        FadeOut();
    }

    void Update()
    {
        if (nextScene >= 0 && fade.GetComponent<CanvasRenderer>().GetAlpha() >= 1)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(nextScene);
        }
    }

    void FadeIn()
    {
        fade.GetComponent<Image>().CrossFadeAlpha(1, 1, true);
    }

    void FadeOut()
    {
        fade.GetComponent<Image>().CrossFadeAlpha(0, 1, true);
    }

    bool CheckAlpha()
    {
        if (fade.GetComponent<CanvasRenderer>().GetAlpha() >= 1 || fade.GetComponent<CanvasRenderer>().GetAlpha() <= 0)
        {
            return true;
        }
        return false;
    }

    public void LoadScene(int scene)
    {
        if (CheckAlpha())
        {
            FadeIn();
            nextScene = scene;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
