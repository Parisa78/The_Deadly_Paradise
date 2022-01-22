using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class JinxController : BossEnemyController
{
    public enum Actions
    {
        thunders,
        balls,
        holes,
        summonEnemy,
        Idle
    }
    public GameObject balls;
    public GameObject thunderDanger;
    public GameObject holes;
    public GameObject thunder;
    public float thunderRange;
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
       //StartCoroutine(MoveToTheOtherSide());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextAction()
    {
        switch (currentAttack)
        {
            case Actions.thunders:
                currentAttack = Actions.summonEnemy;
                StartCoroutine(ThunderAttacks());
                break;
            case Actions.summonEnemy:
                currentAttack = Actions.Idle;
                StartCoroutine(SummonEnenmys());
                break;

            case Actions.Idle:
                currentAttack = Actions.holes;
                StartCoroutine(StandIdle());
                break;

            case Actions.holes:
                currentAttack = Actions.balls;
                StartCoroutine(HolesAttacks());
                break;

            case Actions.balls:
                currentAttack = Actions.thunders;
                StartCoroutine(BallAttacks());
                break;
        }
    }

    IEnumerator BallAttacks()
    {
        base.Hit();
        var go = GameObject.Instantiate(balls);
        //.GetComponent<BallSubWeaponController>().isGoingLeft = GetComponent<SpriteRenderer>().flipX;
        go.transform.position = this.transform.position + new Vector3(0, 0.25f, 0);
        //anim.Play("Base Layer.PlantAttack", 0, 0.25f);
        go.GetComponent<BallSubWeaponController>().isGoingLeft = GetComponent<SpriteRenderer>().flipX;
        isAttacking = false;
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 2.0f));
        NextAction();
    }

    IEnumerator HolesAttacks()
    {
        //base.Hit();
        var go = GameObject.Instantiate(balls);
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 2.0f));
        NextAction();
    }


    IEnumerator ThunderAttacks()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 2.0f));
        var pastRootsPos = new List<float>();
        for (int i = 0; i < UnityEngine.Random.Range(5, 7); i++)
        {
            float rootPos;
            do
            {
                rootPos = UnityEngine.Random.Range(-1 * cameraPositionx / 2, cameraPositionx / 2);
            }
            while (!CheckThunderPositionIsValid(rootPos, pastRootsPos));
            pastRootsPos.Add(rootPos);
        }
        foreach (float rootPos in pastRootsPos)
        {
            GameObject go = GameObject.Instantiate(thunderDanger);
            go.transform.position = new Vector3(rootPos, ground_y, 0);
            StartCoroutine(go.GetComponent<OakRootsDanger>().StartRootsDanger());
        }
        //yield return new WaitForSeconds(1.1f);
        //foreach (float rootPos in pastRootsPos)
        //{
        //    GameObject go = GameObject.Instantiate(sproutingRoots);
        //    go.transform.position = new Vector3(rootPos, ground_y, 0);
        //    StartCoroutine(go.GetComponent<OakSproutingRoot>().Sprout());
        //}
        yield return new WaitForSeconds(2.0f);
        NextAction();
    }

    IEnumerator StandIdle()
    {
        Debug.Log("standing still");
        yield return new WaitForSeconds(UnityEngine.Random.Range(4.0f, 6.0f));
        NextAction();
    }

    IEnumerator SummonEnenmys()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 2.0f));
        for (int i = 0; i < UnityEngine.Random.Range(7, 10); i++)
        {

        }
        NextAction();
    }

    private bool CheckThunderPositionIsValid(float rootPos, List<float> pastRootsPos)
    {
        for (int j = 0; j < pastRootsPos.Count; j++)
        {
            //if new root is in a previous root's range, get another position
            if (rootPos < pastRootsPos[j] + thunderRange && rootPos > pastRootsPos[j] - thunderRange)
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
            if (!player.swords[player.swordIdx].GetComponent<SwordMovements>().isInDefenceMode)
                collision.gameObject.GetComponent<PlayerController>().ChangeHealth(-1 * config.attackAmount);
        }
    }
}
