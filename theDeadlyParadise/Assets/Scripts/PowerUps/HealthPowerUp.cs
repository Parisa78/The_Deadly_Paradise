using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{

    private bool ifcontinue;
    void Start()
    {
        ifcontinue = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player.ToString()) && !ifcontinue)
        {
            ifcontinue = true;
            int change = collision.gameObject.GetComponent<PlayerController>().HealthPowerUPAmount();
            collision.gameObject.GetComponent<PlayerController>().ChangeHealth(change);
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
