using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0f, 1f)] public float moveAmount;
    [Range(0f, 5f)] public float jumpAmount;
    [Range(0f, 1f)] public float fallLongAmount;
    [Range(0f, 1f)] public float fallShortAmount;
    private Vector3 position_before_jump; /// <summary>
    /// just y needed
    /// </summary>
    bool jump;
    bool jumpHeld;
    bool on_ground;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.velocity = Vector2.zero;
        jump = false;
        jumpHeld = false;
        on_ground = true;
    }

    //private void Update()
    //{
    //    if (on_ground && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Debug.Log("juuuuump here");
    //        jump = true; //??
    //        on_ground = false;
    //    }
    //    jumpHeld = (!on_ground && Input.GetKey(KeyCode.Space)) ? true : false;
    //}
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, moveAmount, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -moveAmount, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(moveAmount,0 , 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-moveAmount, 0, 0);
        }
        ////////////// jump
        

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
}
