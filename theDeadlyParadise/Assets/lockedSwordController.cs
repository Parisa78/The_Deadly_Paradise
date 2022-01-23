using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockedSwordController : MonoBehaviour
{
    public string swordName;
    public int lockedSwordIndex;
    bool enteredZone = false;

    private void Start()
    {
        if (gameStatus.instance.unlockedSwordCount > lockedSwordIndex) //already got the sword
            Destroy(this.gameObject);
    }

    void Update()
    {
         if(enteredZone && Input.GetKeyUp(KeyCode.Return))
        {
            Destroy(this.gameObject);
            FindObjectOfType<Dialoguemanager>().StartDialogue(new Dialogue("", new string[] { $"Got The {swordName}!"}, null, AfterGotSwordDialogue));
            FindObjectOfType<lastLevelSceneController>().GotAnotherSword();
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

    private void AfterGotSwordDialogue()
    {
        if(swordName == "FireSword")
        {
            FindObjectOfType<reeve_dialogue>().GotFireSwordDialogue();
        }
        DialogueFunctions.DefaultAfterFunction();
    }
}
