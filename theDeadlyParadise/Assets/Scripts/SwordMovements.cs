using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovements : MonoBehaviour
{
    public Vector3 rotationAngle;
    public float rotationSpeed;

    public bool isInDefenceMode;
    public bool isAttacking;
    void Start()
    {
        isAttacking = false;
        isInDefenceMode = false;
    }

    private void Update()
    {
        if (isInDefenceMode)
        {
            if (!Input.GetKey(KeyCode.I))
            {
                isInDefenceMode = false;
                this.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAttacking)
        {
            transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
            
            if(transform.eulerAngles.z <= 315 && transform.eulerAngles.z > 90)
            {
                isAttacking = false;
                this.gameObject.SetActive(false);
            }
        }
    }

    public void DefenceMode()
    {
        isAttacking = false;
        isInDefenceMode = true;
        transform.eulerAngles = new Vector3(0, 0, 90f);
    }

    public void Attack()
    {
        if (isAttacking) return;
        transform.eulerAngles = new Vector3(0, 0, 90f);
        isInDefenceMode = false;
        isAttacking = true;
    }

}
