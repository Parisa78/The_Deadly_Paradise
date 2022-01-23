using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reeve_dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    private Dialogue dialogue;
    private Dialogue dialogueOnEnter;
    private bool playerIsNear;
    private bool gonnaBeDeadNow;
    public GameObject masterStone;
    void Start()
    {
        playerIsNear = false;
        if(SceneManager.GetActiveScene().name == "Village3")
        {
            if (gameStatus.instance.unlockedSwordCount < 2) //if the player is in scene Village3 and hasn't got the sword yet
            {
                dialogue = new Dialogue("Reeve", new string[] { "Now that you learned how to fight, \n you must continue on and save us all!",
            "Please Go to the statue and get the fire sword! It might help you on the way!!",
            "Also, Don't forget to save your journey by praying to the Angel Mary statue!"+
            " \n It will also max out your HP and fill you with DETERMINATION!!"});
            }
            else if (gameStatus.instance.shardsCount == 4)
            {
                Destroy(this.gameObject);//shouldn't be in village3 if cyrus is coming for him
            }
        }
        if(dialogue != null)
            FindObjectOfType<Dialoguemanager>().StartDialogue(dialogue);
        //dialogue.sentences 
    }

    public void GotFireSwordDialogue()
    {
        FindObjectOfType<Dialoguemanager>().StartDialogue(new Dialogue("Reeve", new string[] { "Nice!!\nNow you have a cool fire sword too!",
        "Press L to switch swords!\nEasy!!", "Some monsters or objects only react to one kind of swords!","Well, I guess now you know everything you need to know.",
        "Continue on and save us all!!"}));
    }

    private void Update()
    {
        if(playerIsNear && Input.GetKeyUp(KeyCode.Return) && gameStatus.instance.shardsCount == 4 && !gonnaBeDeadNow)
        {
            FindObjectOfType<Dialoguemanager>().StartDialogue(new Dialogue("Reeve", new string[] { "Hey Ace!", "What?! The Master Stone is in pieces?!"
            ,"Give me the shards and I'll fix it!"}, null, FixShards));
            gonnaBeDeadNow = true;
        }
    }
    private void FixShards()
    {
        var pos = transform.position;
        pos.x += 2;
        transform.position = pos;
        masterStone.SetActive(true);
        FindObjectOfType<Dialoguemanager>().StartDialogue(new Dialogue("Reeve", new string[] { "Here it is!", "Ummm! Is that really you, Ace?!", "You look different...!"
            , "Cyrus! Why did you steal your brothers stuff?!", "You're acting strange!"}, null, ()=> { FindObjectOfType<FirstSceneController>().ShowYourTrueSelf(); }));
    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            playerIsNear = false;
        }
    }
}
