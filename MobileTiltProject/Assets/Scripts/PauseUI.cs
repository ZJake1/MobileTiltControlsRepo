using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseUI;

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void Options()
    {

    }
}
