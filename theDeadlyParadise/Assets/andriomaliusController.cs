using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class andriomaliusController : RegularEnemyController
{
    private Animator anim;
    public GameObject weapon;
    public float timeToAttack;
    private Vector3 recordedPos;
    private PlayerController player;
    private Renderer renderer;
    protected override void Hit()
    {
        base.Hit();
        recordedPos = player.transform.position;
        anim.Play("Base Layer.andromaliusAttack", 0, 0.25f);
        StartCoroutine("InstantiateWeapon");
    }

    private IEnumerator InstantiateWeapon()
    {
        yield return new WaitForSeconds(1);
        var go = GameObject.Instantiate(weapon);
        go.transform.position = recordedPos;
        anim.Play("Base Layer.andromaliusIdle", 0, 0.25f);
        isAttacking = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        effectiveSwords = new string[] { Tags.FireSword.ToString() };
        timeToAttack = config.attackMaxTime;
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!renderer.isVisible) //only work when it's in sight
            return;
        if (!isAttacking)
        {
            if (timeToAttack > 0)
            {
                timeToAttack -= Time.deltaTime;
            }
            else
            {
                Hit();
                timeToAttack = Random.Range(config.attackMaxTime - 1f, config.attackMaxTime + 1f);
            }
        }
    }
}

