using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTriggerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "VillagePortals")
            {
                if (gameStatus.instance.shardsCount == 0)
                {
                    FindObjectOfType<Dialoguemanager>().StartDialogue(new Dialogue("Ace", new string[] { "Oh no! The Master Stone is in pieces!",
                    "The monsters must have broken it and took the shards!", "I should get them all back."}));
                }
                else if (gameStatus.instance.shardsCount == 4)
                {
                    FindObjectOfType<Dialoguemanager>().StartDialogue(new Dialogue("", new string[] {
                        "Now that we have all the shards, let's give them to old man Reeve.\nI don't know the magic needed to fix them."}));
                }
            }
            else //village1
            {
                if (gameStatus.instance.prevScene == null || gameStatus.instance.prevScene == "") //if it's the first time player is in this scene
                    FindObjectOfType<Dialoguemanager>().StartDialogue(new Dialogue("Reeve", new string[] {"Hello Ace-chan!! ",
                "We need your HELP \n Monsters are ATTACKING us! \n Use your sword and take them out.",
                "what!! you don't know how to use sword! \n that is OK \n i will help you.",
                "Read the docs!!!!!!!!! \nI don't remember it myself!!"}));
            }
            Destroy(this.gameObject);
        
            }
    }
}
