using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsTriggerController : MonoBehaviour
{
    public GameObject lightsOffCanvas;
    public GameObject shard;
    public GameObject cyrus;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            StartCoroutine("TurnTheLightsOff");
        }
    }

    private IEnumerator TurnTheLightsOff()
    {
        Destroy(cyrus);
        lightsOffCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);
        lightsOffCanvas.SetActive(false);
        shard.SetActive(true);
        FindObjectOfType<Dialoguemanager>().StartDialogue(new Dialogue("Cyrus",new string[] { "Oooh you won!\nGood for you!!", "I'm going back myself!", "Don't worry about me!"}));
    }
}
