using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    public string NextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            if (NextLevel == "Forest1" && gameStatus.instance.shardsCount != 1) //if it's the first time they're using a portal or they have gone through this, they should have gotten the shard
                return;
            else if (NextLevel == "JinxScene" && gameStatus.instance.shardsCount != 2) //has already done this
                return;
            Debug.Log("entering new level");
            gameStatus.instance.prevScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(NextLevel);
        }
    }
}
