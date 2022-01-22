using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    // Start is called before the first frame update

    private bool ifcontinue;
    void Start()
    {
        ifcontinue = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player.ToString()) && !ifcontinue)
        {
            ifcontinue = true;
            collision.gameObject.GetComponent<PlayerController>().CallChangeSpeed();
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
