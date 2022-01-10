using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reeve_dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    private Dialogue dialogue;
    void Start()
    {
        dialogue = new Dialogue("Reeve",new string[] {"Hello Ace-chan!! ",
            "We need your HELP \n Monsters are ATTACKING us! \n Use your sword and take them out.",
            "what!! you don't know how to use sword! \n that is OK \n i will help you.",
            "use dokme ha !!!!!!!!! ",
            "I want to help you with your training", });
        FindObjectOfType<Dialoguemanager>().StartDialogue(dialogue);
        //dialogue.sentences 
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
