using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(0);
    }
}
