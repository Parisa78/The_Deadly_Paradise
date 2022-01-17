using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reeve_dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    private Dialogue dialogue;
    private Dialogue dialogueOnEnter;
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Village1")
            dialogue = new Dialogue("Reeve",new string[] {"Hello Ace-chan!! ",
            "We need your HELP \n Monsters are ATTACKING us! \n Use your sword and take them out.",
            "what!! you don't know how to use sword! \n that is OK \n i will help you.",
            "use dokme ha !!!!!!!!! ",
            "I want to help you with your training"});
        else if(gameStatus.instance.unlockedSwordCount < 2) //if the player is in scene Village3 and hasn't got the sword yet
        {
            dialogue = new Dialogue("Reeve", new string[] { "Now that you learned how to fight, \n you must continue on and save us all!",
            "Please Go to the statue and get the fire sword! It might help you on the way!!",
            "Also, Don't forget to save your journey by praying to the Angel Mary statue!"+
            " \n It will also max out your HP and fill you with DETERMINATION!!"});
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
