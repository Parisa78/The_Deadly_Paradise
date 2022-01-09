using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class OakController : BossEnemyController
{
    public enum Actions
    {
        RootsAttack,
        RootsMove,
        MoveToTheOtherSide,
        Idle
    }
    public GameObject rootsDanger;
    public GameObject sproutingRoots;
    public GameObject movingRoots;
    public float rootRange;
    private Actions currentAttack;
    float cameraPositionx;
    float cameraPositiony;
    float distanceX;
    float ground_y;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        effectiveSwords = new string[] { Tags.FireSword.ToString() };
        currentAttack = Actions.Idle;
        cameraPositionx = 1f / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).x - 0.5f);
        cameraPositiony = 1f / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).y - 0.5f);
        ground_y = GameObject.FindWithTag(Tags.Ground.ToString()).transform.position.y;
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
                currentAttack = Actions.RootsMove;
                StartCoroutine(RootsMove());

                break;
            case Actions.MoveToTheOtherSide:
                currentAttack = Actions.Idle;
                StartCoroutine(StandIdle());
                break;

            case Actions.Idle:
                currentAttack = Actions.RootsAttack;
                StartCoroutine(RootsAttack());
                break;

            case Actions.RootsMove:
                currentAttack = Actions.MoveToTheOtherSide;
                StartCoroutine(MoveToTheOtherSide());
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
        NextAction();
    }

    IEnumerator RootsAttack()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 2.0f));
        var pastRootsPos = new List<float>();
        for(int i = 0; i < UnityEngine.Random.Range(5, 7); i++)
        {
            float rootPos;
            do
            {
                rootPos = UnityEngine.Random.Range(-1 * cameraPositionx / 2, cameraPositionx / 2);
            }
            while (!CheckRootPositionIsValid(rootPos, pastRootsPos));
            pastRootsPos.Add(rootPos);
        }
        foreach (float rootPos in pastRootsPos)
        {
            GameObject go = GameObject.Instantiate(rootsDanger);
            go.transform.position = new Vector3(rootPos, ground_y, 0);
            StartCoroutine(go.GetComponent<OakRootsDanger>().StartRootsDanger());
        }
        yield return new WaitForSeconds(1.1f);
        foreach (float rootPos in pastRootsPos)
        {
            GameObject go = GameObject.Instantiate(sproutingRoots);
            go.transform.position = new Vector3(rootPos, ground_y, 0);
            StartCoroutine(go.GetComponent<OakSproutingRoot>().Sprout());
        }
        yield return new WaitForSeconds(2.0f);
        NextAction();
    }

    IEnumerator StandIdle()
    {
        Debug.Log("standing still");
        yield return new WaitForSeconds(UnityEngine.Random.Range(4.0f, 6.0f));
        NextAction();
    }

    IEnumerator RootsMove()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 2.0f));
        for (int i=0; i<UnityEngine.Random.Range(7,10); i++)
        {
            GameObject go = GameObject.Instantiate(movingRoots);
            go.transform.position = new Vector3(cameraPositionx/2, ground_y, 0);
            go = GameObject.Instantiate(movingRoots);
            go.transform.position = new Vector3(-1 * cameraPositionx / 2, ground_y, 0);
            yield return new WaitForSeconds(1.35f);
        }
        NextAction();
    }

    private bool CheckRootPositionIsValid(float rootPos, List<float> pastRootsPos)
    {
        for (int j = 0; j < pastRootsPos.Count; j++)
        {
            //if new root is in a previous root's range, get another position
            if (rootPos < pastRootsPos[j] + rootRange && rootPos > pastRootsPos[j] - rootRange)
            {
                return false;
            }
        }
        return true;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if(! player.swords[player.swordIdx].GetComponent<SwordMovements>().isInDefenceMode)
                collision.gameObject.GetComponent<PlayerController>().ChangeHealth(-1 * config.attackAmount);
        }
    }
}
