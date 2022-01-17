using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_transmissin_controler : MonoBehaviour
{
    public string nextScene;
    //private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player.ToString()) && PlayerCanGo())
        {
            Debug.Log("enter new scene");
            gameStatus.instance.prevScene=SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(nextScene);
            //player.transform.position=this.
        }
    }

    private bool PlayerCanGo()
    {
        if(SceneManager.GetActiveScene().name == "Village3" && nextScene == "Village4")
        {
            //if the player has the firesword, they can continue
            if(gameStatus.instance.unlockedSwordCount == 1)
            {
                FindObjectOfType<Dialoguemanager>().StartDialogue(new Dialogue("Reeve", new string[] { "Hey!\nYou forgot your sword!!"}));
                return false;
            }
        }
        return true;
    }
}
