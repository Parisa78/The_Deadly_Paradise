using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class cyrusInMirrorController : MonoBehaviour
{
    SpriteRenderer sp;
    PlayerController player;
    public bool isFound;
    private Dialoguemanager dialoguemanager;
    public float moveAmount;

    //for pathfinding
    public float nextWaypointDistance = 0.1f;
    Path path;
    int currentWayPoint = 0;
    Seeker seeker;
    Rigidbody2D rb;
    bool goNearPlayer = false;
    public Vector3 playerPosition;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerController>();
        dialoguemanager = FindObjectOfType<Dialoguemanager>();
        seeker = GetComponent<Seeker>();
        isFound = false;
        InvokeRepeating("UpdatePath", 0f, 0.1f);
        moveAmount = player.moveAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFound)
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
        else
        {
            playerPosition = player.transform.position;
        }
        
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
        isFound = true;
        dialoguemanager.StartDialogue(new Dialogue("Cyrus", new string[] { "Hehe! You found me!", "I was bored a little, so I decided to just wander around these cool domains!"
        ,"Huh? How did I come here??", "Well, there was nothing in my way to stop me!", "You're telling me to go home? It's dangerous?\nBut I like wandering...",
            "Ok. I'll go. but only if you play a game with me!\nWhat, you need to entertain me if you don't let me hang around these places!", "What game? How about a simple game of tag?",
            "I'm it. you need to escape me now! haha!", "I'll wait for 3 seconds..."},null,StartCountDown));
        Destroy(GameObject.Find("destroyableWall"));
    }

    public void StartCountDown()
    {
        sp.sortingOrder = 10;
        var pos = transform.position;
        pos.y = player.transform.position.y;
        transform.position = pos;
        FindObjectOfType<PlayerController>().canReadInput = true; //player can start moving again
        StartCoroutine("CountDown");
    }

    IEnumerator CountDown()
    {
        for (int i = 1; i < 4; i++)
        {
            yield return new WaitForSeconds(1);
            dialoguemanager.StartDialogue(new Dialogue("Cyrus", new string[] { i.ToString() + "!" }, () => { }, () => { })); //dialogue text change into sth scarier each time!
        }
        dialoguemanager.StartDialogue(new Dialogue("Cyrus", new string[] { "3!! Here comes Cyrus!!!" }, () => { }, () => { }));
        goNearPlayer = true;
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(transform.position, player.transform.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (goNearPlayer)
        {
            if (path == null)
                return;
            if (currentWayPoint >= path.vectorPath.Count)
            {
                return;
            }
            Vector3 direction = (path.vectorPath[currentWayPoint] - transform.position).normalized;
            transform.position += direction * moveAmount;

            sp.flipX = direction.x < 0;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWayPoint]);

            if (distance < nextWaypointDistance)
            {
                currentWayPoint++;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player.ToString()) && goNearPlayer)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("dead_menu");
        }
    }
}
