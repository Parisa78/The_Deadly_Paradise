using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OakController : BossEnemyController
{
    public enum Actions
    {
        RootsAttack,
        MoveToTheOtherSide,
        Idle
    }
    private Actions currentAttack;
    float cameraPositionx;
    float cameraPositiony;
    float distanceX;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        effectiveSwords = new string[] { Tags.FireSword.ToString() };
        currentAttack = Actions.MoveToTheOtherSide;
        cameraPositionx = 1f / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).x - 0.5f);
        cameraPositiony = 1f / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).y - 0.5f);
        distanceX = transform.position.x;
        StartCoroutine(MoveToTheOtherSide());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextAction()
    {
        switch (currentAttack)
        {
            case Actions.RootsAttack:
                currentAttack = Actions.MoveToTheOtherSide;
                StartCoroutine(MoveToTheOtherSide());

                break;
            case Actions.MoveToTheOtherSide:
                currentAttack = Actions.Idle;
                break;
            case Actions.Idle:
                currentAttack = Actions.RootsAttack;
                break;
        }
    }

    IEnumerator MoveToTheOtherSide()
    {
        float dest;
        BoxCollider2D bx = GetComponent<BoxCollider2D>();
        bx.isTrigger = true;
        if (transform.position.x > 0)
        {
            dest = -1 * distanceX;
        }
        else dest = distanceX;
        for(int i = 0; i<200; i++)
        {
            transform.position += new Vector3(dest / 100f, 0, 0);
            yield return new WaitForSeconds(0.005f);
        }
        transform.position = new Vector3(dest, transform.position.y, transform.position.z);
        bx.isTrigger = false;
    }


}
