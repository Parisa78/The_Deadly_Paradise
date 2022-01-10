using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialoguemanager : MonoBehaviour
{
    // Start is called before the first frame update

    private Queue<string> sentences;
    private TextMeshProUGUI nameTxt;
    private TextMeshProUGUI dialogueTxt;
    private GameObject DialogueBox;
    private bool dialogueisactive;
    void Start()
    {
        DialogueBox = GameObject.Find("Dialogue_Box");
        dialogueTxt = GameObject.Find("dialogue").GetComponent<TextMeshProUGUI>();
        nameTxt = GameObject.Find("dialogue_name").GetComponent<TextMeshProUGUI>();
        DialogueBox.SetActive(false);
        sentences = new Queue<string>();
        dialogueisactive = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueisactive = true;
        DialogueBox.SetActive(true);
        sentences.Clear();
        nameTxt.text = dialogue.name;
        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence=sentences.Dequeue();
        dialogueTxt.text = sentence;
    }

    public void EndDialogue()
    {
        DialogueBox.SetActive(false);
        dialogueisactive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && dialogueisactive)
        {
            DisplayNextSentence();
        }
    }
}
