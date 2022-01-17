using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovements : MonoBehaviour
{
    public Vector3 rotationAngle;
    public float rotationSpeed;
    public PlayerController player;

    public bool isInDefenceMode;
    public bool isAttacking;
    private (int, int)[] rotationAngles = new (int, int)[]
    {
        (90, 315),
        (90, 225)
    };

    public (int start, int end) currentRotationVal;

    private void Awake()
    {
        currentRotationVal = rotationAngles[0];
    }

    void Start()
    {
        isAttacking = false;
        isInDefenceMode = false;
        player = FindObjectOfType<PlayerController>();
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
        currentRotationVal = player.direction == PlayerController.Direction.Right?
            rotationAngles[0] : rotationAngles[1];

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAttacking)
        {
            if(player.direction == PlayerController.Direction.Right)
            {
                transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);

                if (transform.eulerAngles.z <= currentRotationVal.end && transform.eulerAngles.z > currentRotationVal.start)
                {
                    StopAttacking();
                }
            }
            else
            {
                transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime * -1);
                if (transform.eulerAngles.z < currentRotationVal.start || transform.eulerAngles.z >= currentRotationVal.end)
                {
                    StopAttacking();
                }
            }
        }
    }

    public void DefenceMode()
    {
        isAttacking = false;
        isInDefenceMode = true;
        transform.eulerAngles = new Vector3(0, 0, currentRotationVal.start);
    }

    public void Attack()
    {
        if (isAttacking) return;
        transform.eulerAngles = new Vector3(0, 0, currentRotationVal.start);
        isInDefenceMode = false;
        isAttacking = true;
    }

    public void StopAttacking()
    {
        isAttacking = false;
        this.gameObject.SetActive(false);
    }

}
