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
        gameStatus.instance.justLoadedPlayerPosition = new Vector3(data.position[0], data.position[1], data.position[2]);
        gameStatus.instance.playerHP = data.hp;
        SceneManager.LoadScene(data.scene);
    }

    public void ResetandGoToGame()
    {
        data = SaveSystem.ResetData();
        GoToGame();
    }
}
