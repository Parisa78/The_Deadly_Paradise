using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cyrusInMirrorController : MonoBehaviour
{
    SpriteRenderer sp;
    PlayerController player;
    public bool isFound;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerController>();
        isFound = false;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        pos.x = player.transform.position.x;
        if (pos.x > 13.05f)
        {
            pos.x = 13.05f;
            transform.position = pos;
            return;
        }
        transform.position = pos;
        ChangeDirection(player.direction);
    }

    private void ChangeDirection(PlayerController.Direction direction)
    {
        if (direction == PlayerController.Direction.Left)
        {
            var t = transform.localRotation;
            t.y = 180;
            transform.localRotation = t;
        }
        if (direction == PlayerController.Direction.Right)
        {
            var t = transform.localRotation;
            t.y = 0;
            transform.localRotation = t;
        }
    }

    public void Found()
    {

    }
}
