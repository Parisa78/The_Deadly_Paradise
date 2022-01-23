using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadEndingController : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void GoToGame()
    {
        var data = SaveSystem.LoadPlayer();
        gameStatus.instance.shardsCount = data.shardsCount;
        gameStatus.instance.unlockedSwordCount = data.unlockedSwordCount;
        gameStatus.instance.justLoadedPlayerPosition = data.position;
        gameStatus.instance.playerHP = data.hp;
        gameStatus.instance.camPosition = data.cameraPosition;
        SceneManager.LoadScene(data.scene);
    }
}
