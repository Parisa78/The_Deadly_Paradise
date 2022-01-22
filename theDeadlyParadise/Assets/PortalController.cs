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
            if (NextLevel == "Forest1" && gameStatus.instance.shardsCount == 0) //if it's the first time they're using a portal, they should have gotten the shard
                return;
            Debug.Log("entering new level");
            gameStatus.instance.prevScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(NextLevel);
        }
    }
}
