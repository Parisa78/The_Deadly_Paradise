using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStatue : MonoBehaviour
{
    PlayerController player;
    private Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        dialogue = new Dialogue("", new string[] { "Your data is saved!"},()=> { },null);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return) && Vector3.Distance(transform.position, player.transform.position) < 0.5)
        {
            gameStatus.instance.playerHP = 100;
            player.healthBar.SetHealth(100);
            SaveSystem.SaveData(player,
                Camera.main.transform.position);
            FindObjectOfType<Dialoguemanager>().StartDialogue(dialogue);
        }
    }
}
