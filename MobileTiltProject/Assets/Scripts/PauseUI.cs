using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public void Resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Options()
    {

    }

    public void LoadTitleScene()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
