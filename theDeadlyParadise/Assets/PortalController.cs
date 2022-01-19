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
            Debug.Log("entering new level");
            gameStatus.instance.prevScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(NextLevel);
        }
    }
}
