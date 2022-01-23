using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{

    public void ContinueLevel()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
