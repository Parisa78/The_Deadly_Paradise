using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI ContinueText;
    private SaveData data;
    private void Awake()
    {
        data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            ContinueText.text = "New Game";
            SaveSystem.ResetData(); //get a brand new game
            data = SaveSystem.LoadPlayer();
        }
        
    }

    public void GoToGame()
    {
        gameStatus.instance.shardsCount = data.shardsCount;
        gameStatus.instance.unlockedSwordCount = data.unlockedSwordCount;
        gameStatus.instance.justLoadedPlayerPosition = data.position;
        gameStatus.instance.playerHP = data.hp;
        gameStatus.instance.camPosition = data.cameraPosition;
        SceneManager.LoadScene(data.scene);
    }

    public void ResetandGoToGame()
    {
        data = SaveSystem.ResetData();
        GoToGame();
    }
}
