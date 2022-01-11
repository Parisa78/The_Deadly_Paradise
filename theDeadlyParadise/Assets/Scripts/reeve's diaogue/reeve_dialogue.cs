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
        else
        {
            dialogue = new Dialogue("Reeve", new string[] { "Now that you learned how to fight, \n you must continue on and save us all!",
            "Please Go to the statue and get the fire sword! It might help you on the way!!",
            "Also, Don't forget to save your journey by praying to the Angel Mary statue!"+
            " \n It will also max out your HP and fill you with DETERMINATION!!"});
        }
        FindObjectOfType<Dialoguemanager>().StartDialogue(dialogue);
        //dialogue.sentences 
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
