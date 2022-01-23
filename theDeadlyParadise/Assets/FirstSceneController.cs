using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneController : MonoBehaviour
{
    Dialoguemanager dm;
    PlayerController player;

    public void ShowYourTrueSelf()
    {
        player.transform.position = FindObjectOfType<reeve_dialogue>().transform.position + Vector3.left;
        player.swords[player.swordIdx].GetComponent<SwordMovements>().Attack();
        FindObjectOfType<reeve_dialogue>().gameObject.transform.Rotate(new Vector3(0, 0, -90f));
        dm.StartDialogue(new Dialogue("Cyrus", new string[] { "That's too late, old man!", "Thanks for fixing this stone for me.",
        "I was tired of the boring life I had before, always being weaker than my brother.\nHe had all the fun adventures for himself.",
        "That's why I contacted the monsters to come here!",
        "With the power of this stone, I can do whatever I want now!",
        "Thanks to you, Reeve.",
        "And of course, thanks to you, the player outside this world!"},null,ChangeToBadEnding));
    }

    void ChangeToBadEnding()
    {
        SceneManager.LoadScene("bad_ending_menu");
    }

    // Start is called before the first frame update
    void Start()
    {
        dm = FindObjectOfType<Dialoguemanager>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
