using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class JinxController : BossEnemyController
{
    public Animator animator;
    public enum Actions
    {
        thunders,
        balls,
        holes,
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
    float ground_up_y;
    float ground_down_y;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        effectiveSwords = new string[] { Tags.ElectroSword.ToString() };
        currentAttack = Actions.balls;
        cameraPositionx = 1f / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).x - 0.5f);
        cameraPositiony = 1f / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).y - 0.5f);
        Bounds boxBounds = GameObject.FindWithTag(Tags.Ground.ToString()).GetComponent<BoxCollider2D>().bounds;
        ground_up_y = boxBounds.center.y + boxBounds.extents.y;
        ground_down_y = boxBounds.center.y - boxBounds.extents.y;
        distanceX = transform.position.x;
       StartCoroutine(BallAttacks());
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void NextAction()
    {
        switch (currentAttack)
        {
            case Actions.thunders:
                animator.SetBool("idl", true);
                currentAttack = Actions.Idle;
                StartCoroutine(StandIdle());
                animator.SetBool("idl", true);
                break;

            case Actions.Idle:
                animator.SetBool("idl", false);
                currentAttack = Actions.holes;
                StartCoroutine(HolesAttacks());
                animator.SetBool("idl", false);
                break;

            case Actions.holes:
                currentAttack = Actions.balls;
                StartCoroutine(BallAttacks());
                break;

            case Actions.balls:
                currentAttack = Actions.thunders;
                StartCoroutine(ThunderAttacks());

                break;
        }
    }

    IEnumerator BallAttacks()
    {
        var go = GameObject.Instantiate(balls);
        go.transform.position = this.transform.position + new Vector3(0, 0.25f, 0);

        if (transform.position.x > 0)
        {
            go.GetComponent<BallSubWeaponController>().isGoingLeft = true;
        }
        else
        {
            go.GetComponent<BallSubWeaponController>().isGoingLeft = false;
        }
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 2.0f));
        NextAction();
    }

    IEnumerator HolesAttacks()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 2.0f));
        var pastRootsPos = new List<Vector3>();
        for (int i = 0; i < UnityEngine.Random.Range(1, 3); i++)
        {
            Vector3 rootPos = Vector3.zero;
            do
            {
                rootPos.x = UnityEngine.Random.Range(-1 * cameraPositionx / 2, cameraPositionx / 2);
                rootPos.y = UnityEngine.Random.Range(ground_down_y, ground_up_y);
            }
            while (!CheckThunderPositionIsValid(rootPos, pastRootsPos));
            pastRootsPos.Add(rootPos);
        }
        foreach (Vector3 rootPos in pastRootsPos)
        {
            GameObject go = GameObject.Instantiate(thunderDanger);
            go.transform.position = rootPos;
            StartCoroutine(go.GetComponent<jinxThunderDanger>().StartThunderDanger());
        }
        yield return new WaitForSeconds(1.1f);
        foreach (Vector3 rootPos in pastRootsPos)
        {
            GameObject go = GameObject.Instantiate(holes);
            go.transform.position = rootPos;
            yield return new WaitForSeconds(2.0f);
            Destroy(go);
        }
        yield return new WaitForSeconds(2.0f);
        foreach (Vector3 rootPos in pastRootsPos)

        NextAction();
    }


    IEnumerator ThunderAttacks()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 2.0f));
        var pastRootsPos = new List<Vector3>();
        for (int i = 0; i < UnityEngine.Random.Range(2, 4); i++)
        {
            Vector3 rootPos = Vector3.zero;
            do
            {
                rootPos.x = UnityEngine.Random.Range(-1 * cameraPositionx / 2, cameraPositionx / 2);
                rootPos.y = UnityEngine.Random.Range(ground_down_y, ground_up_y);
            }
            while (!CheckThunderPositionIsValid(rootPos, pastRootsPos));
            pastRootsPos.Add(rootPos);
        }
        foreach (Vector3 rootPos in pastRootsPos)
        {
            GameObject go = GameObject.Instantiate(thunderDanger);
            go.transform.position = rootPos;
            StartCoroutine(go.GetComponent<jinxThunderDanger>().StartThunderDanger());
        }
        yield return new WaitForSeconds(1.1f);
        foreach (Vector3 rootPos in pastRootsPos)
        {
            GameObject go = GameObject.Instantiate(thunder);
            go.transform.position = rootPos;
        }
        yield return new WaitForSeconds(2.0f);
        NextAction();
    }

    IEnumerator StandIdle()
    {
        float dest;
        if (transform.position.x > 0)
        {
            dest = -1 * distanceX;
        }
        else dest = distanceX;
        transform.position = new Vector3(dest, transform.position.y, transform.position.z);
        Debug.Log("Im waiting in standidle");
        yield return new WaitForSeconds(UnityEngine.Random.Range(4.0f, 6.0f));
        Debug.Log("waiting in stand idle finished");
        NextAction();
    }

    private bool CheckThunderPositionIsValid(Vector3 rootPos, List<Vector3> pastRootsPos)
    {
        for (int j = 0; j < pastRootsPos.Count; j++)
        {
            //if new root is in a previous root's range, get another position
            if (Vector3.Distance(rootPos, pastRootsPos[j]) < thunderRange)
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
