using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//serializer

public delegate void DialogueFunction();
public class Dialogue 
{
    public string name;
    public string[] sentences;
    public DialogueFunction beforeDialogueFunction;
    public DialogueFunction afterDialogueFunction;

    public Dialogue(string name, string[] sentences, DialogueFunction before = null, DialogueFunction after=null)
    {
        this.name = name;
        this.sentences = sentences;

        if (before == null)
            beforeDialogueFunction = DialogueFunctions.DefaultBeforeFunction;
        else
            beforeDialogueFunction = before;

        if (after == null)
            afterDialogueFunction = DialogueFunctions.DefaultAfterFunction;
        else
            afterDialogueFunction = after;
    }

    
}

public class DialogueFunctions : MonoBehaviour
{
    //Couldn't call FindObjectOfType in a class that didn't inherit from MonoBehavior, that's why this class was made

    //in default before and after dialogue functions, player can't move until the dialogue is finished
    public static void DefaultBeforeFunction()
    {
        FindObjectOfType<PlayerController>().canReadInput = false;
    }

    public static void DefaultAfterFunction()
    {
        FindObjectOfType<PlayerController>().canReadInput = true;
    }
}