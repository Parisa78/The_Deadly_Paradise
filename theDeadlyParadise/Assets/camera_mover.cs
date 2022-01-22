using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_mover : MonoBehaviour
{
    public string direction;
    private float camHeight;
    private float camWidth;
    Camera cam;
    PlayerController player;
    bool _triggered;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_triggered)
        {
            return;
        }
        if (collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            _triggered = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.Player.ToString()))
        {
            switch (direction)
            {
                case "v":
                    if (player.transform.position.y > transform.position.y && cam.transform.position.y < transform.position.y)
                        cam.transform.position += Vector3.up * camHeight;
                    else if (player.transform.position.y < transform.position.y && cam.transform.position.y > transform.position.y)
                        cam.transform.position += Vector3.down * camHeight;
                    break;

                case "h":
                    if (player.transform.position.x > transform.position.x && cam.transform.position.x < transform.position.x)
                        cam.transform.position += Vector3.right * camWidth;
                    else if (player.transform.position.x < transform.position.x && cam.transform.position.x > transform.position.x)
                        cam.transform.position += Vector3.left * camWidth;
                    break;
            }
        }
    }
}
