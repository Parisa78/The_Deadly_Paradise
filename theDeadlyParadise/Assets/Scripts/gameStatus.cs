using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameStatus : MonoBehaviour
{
    public static gameStatus instance = null;

    // For sake of example, assume -1 indicates first scene
    public string prevScene;
    public GameObject UiCanvas;
    public int unlockedSwordCount;
    public int shardsCount;
    public int playerHP;
    public float[] justLoadedPlayerPosition;
    public float[] camPosition;
    void Awake()
    {
        //load UI
        if (!SceneManager.GetActiveScene().name.Contains("menu"))
        {
            var go = Instantiate(UiCanvas);
        }
        // If we don't have an instance set - set it now
        if (!instance)
            instance = this;
        // Otherwise, its a double, we dont need it - destroy
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        //these won't be loaded if the game is loaded by the reset or continue buttons. these are for testing individual scenes.
        justLoadedPlayerPosition = null;
        camPosition = null;
        playerHP = 100;
        unlockedSwordCount = 1;
        shardsCount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !SceneManager.GetActiveScene().name.Contains("menu"))
        {
            Debug.Log(UiCanvas.transform.Find("pausedMenu"));
            FindObjectOfType<Dialoguemanager>().transform.Find("pausedMenu").gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
