using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lastLevelSceneController : MonoBehaviour
{
    public int mustReachSwordCount;
    public int mustReachShardCount;

    public void GotAnotherSword()
    {
        gameStatus.instance.unlockedSwordCount++;
        if (gameStatus.instance.unlockedSwordCount >= mustReachSwordCount
            && gameStatus.instance.shardsCount >= mustReachShardCount)
        {
            gameStatus.instance.playerHP = 100;

            gameStatus.instance.prevScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("VillagePortals");
        }
    }

    public void GotAnotherShard()
    {
        gameStatus.instance.shardsCount++;
        if (gameStatus.instance.unlockedSwordCount >= mustReachSwordCount
            && gameStatus.instance.shardsCount >= mustReachShardCount)
        {
            gameStatus.instance.playerHP = 100;
            gameStatus.instance.prevScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("VillagePortals");
        }
    }
}
