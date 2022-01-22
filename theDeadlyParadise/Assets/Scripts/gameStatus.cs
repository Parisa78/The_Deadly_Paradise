using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStatus : MonoBehaviour
{
    public static gameStatus instance = null;

    // For sake of example, assume -1 indicates first scene
    public string prevScene;
    public GameObject UiCanvas;
    public int unlockedSwordCount;
    public int shardsCount;
    public int playerHP;

    void Awake()
    {
        //load UI
        Instantiate(UiCanvas);
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

        //get loaded game status
        //Remove the next line when this class gets the loaded data
        unlockedSwordCount = 4;
        shardsCount = 3;
    }
}
