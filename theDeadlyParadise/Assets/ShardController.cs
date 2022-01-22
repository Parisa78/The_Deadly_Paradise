using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardController : MonoBehaviour
{
    bool enteredZone = false;
    public int shardNumber;
    // Start is called before the first frame update
    void Start()
    {
        if (shardNumber == gameStatus.instance.shardsCount)
            Destroy(this.gameObject); //already got the shard
    }

    // Update is called once per frame
    void Update()
    {
        if (enteredZone && Input.GetKeyUp(KeyCode.Return))
        {
            Destroy(this.gameObject);
            FindObjectOfType<Dialoguemanager>().StartDialogue(new Dialogue("", new string[] { $"Got Another Shard!" }));
            if (FindObjectOfType<lastLevelSceneController>())
                FindObjectOfType<lastLevelSceneController>().GotAnotherShard();
            else
                gameStatus.instance.shardsCount++;
            Debug.Log(gameStatus.instance.unlockedSwordCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            enteredZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            enteredZone = false;
        }
    }
}
