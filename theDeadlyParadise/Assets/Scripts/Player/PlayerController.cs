using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0f, 1f)] public float moveAmount;
    [Range(0f, 10f)] public float jumpAmount;
    [Range(0f, 1f)] public float fallLongAmount;
    [Range(0f, 1f)] public float fallShortAmount;
    private Vector3 position_before_jump; /// <summary>
    public HealthBar healthBar;
    public GameObject[] swords;
    public int hitAmount;
    public Animator animator;
    public bool canReadInput;
    public enum Direction
    {
        //Up,
        //Down,
        Right,
        Left
    }
    public Direction direction;
    // change: public
    public int unlockedSwordCount;
    public int swordIdx;
    /// just y needed
    /// </summary>
    bool jump;
    bool jumpHeld;
    bool on_ground;
    bool can_jump;

    private Rigidbody2D rb;
    private bool sp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        swordIdx = 0;
        direction = Direction.Right;
        sp = false;
        canReadInput = true;
    }
    void Start()
    {
        canReadInput = true;
        //for changing scene 
        ChangingSceneSettings();
        //end
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        healthBar.SetMaxHealth(100);
        healthBar.SetHealth(gameStatus.instance.playerHP);
        unlockedSwordCount = gameStatus.instance.unlockedSwordCount;
        rb.velocity = Vector2.zero;
        jump = false;
        jumpHeld = false;
        on_ground = true;
        if(SceneManager.GetActiveScene().name == "OakScene")
            enterOakScene();
        else
            can_jump = false;
    }

    private void Update()
    {
        if (canReadInput)
        {
            if (can_jump && on_ground && Input.GetKeyDown(KeyCode.Space))
            {
                jump = true; //??
                on_ground = false;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                changeSword();
            }
        }

        jumpHeld = (!on_ground && Input.GetKey(KeyCode.Space)) ? true : false;
        
        //if((Input.GetKey(KeyCode.W) && !can_jump) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
        //    || Input.GetKey(KeyCode.A))
        //{
        //    animator.SetFloat("Speed", 1);
        //}
        //else
        //{
        //    animator.SetFloat("Speed", 0);
        //}
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        sp = false;
        Direction tempDir = direction;
        //if a dialogue is being played, the default behavior is to not move (no input reading!)
        if (canReadInput)
        {
            if (Input.GetKey(KeyCode.I))
            {
                swords[swordIdx].SetActive(true);
                swords[swordIdx].GetComponent<SwordMovements>().DefenceMode();
                animator.SetBool("Speed", false);
                return;
            }

            if (Input.GetKey(KeyCode.J))
            {
                swords[swordIdx].SetActive(true);
                swords[swordIdx].GetComponent<SwordMovements>().Attack();
                animator.SetBool("IsAttacking", true);
                animator.SetBool("Speed", false);
                return;
            }
            else
            {
                animator.SetBool("IsAttacking", false);
            }


            if (Input.GetKey(KeyCode.W) && !can_jump)
            {
                transform.position += new Vector3(0, moveAmount, 0);
                sp = true;
                //tempDir = Direction.Up;
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(0, -moveAmount, 0);
                sp = true;
                //tempDir = Direction.Down;
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(moveAmount, 0, 0);
                sp = true;
                tempDir = Direction.Right;
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-moveAmount, 0, 0);
                sp = true;
                tempDir = Direction.Left;
            }

            

            if (jump)
            {
                //position_before_jump = transform.position;
                rb.velocity = Vector2.up * jumpAmount;
                jump = false;
            }

            if (jumpHeld && rb.velocity.y > 0)
            {
                rb.velocity += Vector2.up * Time.fixedDeltaTime * Physics2D.gravity.y * (fallLongAmount - 1);
            }

            else if (!jumpHeld && rb.velocity.y > 0)
            {
                rb.velocity += Vector2.up * Time.fixedDeltaTime * Physics2D.gravity.y * (fallShortAmount - 1);

                //if (transform.position.y <= position_before_jump.y)
                //{
                //    Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaah");
                //    rb.velocity = Vector2.zero;
                //    on_ground = true;
                //    transform.position = new Vector3(transform.position.x, position_before_jump.y, transform.position.z);
                //}

            }
        }
        

        direction = tempDir;
        ChangeDirection(direction);
        animator.SetBool("Speed", sp);
        ////////////// jump
        //can_jump ro ezafe konim

        //if (jump)
        //{
        //    position_before_jump = transform.position;
        //    rb.velocity = Vector2.up * jumpAmount;
        //    jump = false;
        //}

        //if (jumpHeld && rb.velocity.y > 0)
        //{
        //    rb.velocity += Vector2.up * Time.fixedDeltaTime * Physics2D.gravity.y * (fallLongAmount - 1);
        //}

        //else if (!jumpHeld && rb.velocity.y > 0)
        //{
        //    rb.velocity += Vector2.up * Time.fixedDeltaTime * Physics2D.gravity.y * (fallShortAmount - 1);

        //    if(transform.position.y<= position_before_jump.y)
        //    {
        //        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaah");
        //        rb.velocity = Vector2.zero;
        //        on_ground = true;
        //        transform.position = new Vector3(transform.position.x,position_before_jump.y,transform.position.z);
        //    }

        //}
    }

    private void ChangeDirection(Direction direction)
    {
        if (direction == Direction.Left)
        {
            var t = transform.localRotation;
            t.y = 180;
            transform.localRotation = t;
        }
        if (direction == Direction.Right)
        {
            var t = transform.localRotation;
            t.y = 0;
            transform.localRotation = t;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (can_jump && collision.gameObject.CompareTag(Tags.Ground.ToString()))
        {
            on_ground = true;
            //rb.velocity = Vector2.zero;
        }
    }

    public void changeSword()
    {
        swords[swordIdx].SetActive(false);
        if (++swordIdx >= gameStatus.instance.unlockedSwordCount)
            swordIdx = 0;
        swords[swordIdx].SetActive(true);
        StartCoroutine(ShowSword());
    }

    IEnumerator ShowSword()
    {
        yield return new WaitForSeconds(1);
        if(!swords[swordIdx].GetComponent<SwordMovements>().isAttacking
            && !swords[swordIdx].GetComponent<SwordMovements>().isInDefenceMode)
            swords[swordIdx].SetActive(false);
    }

    public void enterOakScene()
    {
        can_jump = true;
        rb.gravityScale = 1;
        Debug.Log("enter oak scene");
    }

    public void ChangeHealth(int amount)
    {
        healthBar.ChangeHealth(amount);
    }

    public void ChangingSceneSettings()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Village1":
                if (gameStatus.instance.prevScene == "Village2")
                {
                    transform.position = FindObjectOfType<scene_transmissin_controler>().transform.position+ new Vector3(0,0.8f,0);
                }
                break;

            case "Village2":
                if (gameStatus.instance.prevScene == "Village3")
                {
                    transform.position = GameObject.Find("transmission_scene3").transform.position + new Vector3(0, -1.5f, 0);
                }
                break;

            case "Village3":
                if (gameStatus.instance.prevScene == "Village4")
                {
                    transform.position = GameObject.Find("transmission_scene_4").transform.position + new Vector3(0, 0.8f, 0);
                }
                break;
            case "Village4":
                if (gameStatus.instance.prevScene == "VillagePortals")
                {
                    transform.position = GameObject.Find("transmission_scene_portals").transform.position + new Vector3(-0.8f,0, 0);
                }
                break;
            case "VillagePortals":
                if (gameStatus.instance.shardsCount == 0)
                {
                    FindObjectOfType<Dialoguemanager>().StartDialogue(new Dialogue("Ace", new string[] { "Oh no! The Master Stone is in pieces!",
                    "The monsters must have broken it and took the shards!", "I should get them all back."}));
                }
                else if(gameStatus.instance.shardsCount == 4)
                {
                    FindObjectOfType<Dialoguemanager>().StartDialogue(new Dialogue("", new string[] { 
                        "Now that we have all the shards, let's give them to old man Reeve.\nI don't know the magic needed to fix them."}));
                }
                break;
        }
    }

}
