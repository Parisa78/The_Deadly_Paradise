using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBlockerController : MonoBehaviour
{
    public string effectiveSwordName;
    public int shardsNeededToMakeThisDisappear;
    public Sprite[] sprites;
    private int spriteIdx;
    // Start is called before the first frame update
    void Start()
    {
        spriteIdx = 0;
        if (gameStatus.instance.shardsCount >= shardsNeededToMakeThisDisappear)
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.ToString() == effectiveSwordName)  //collision.gameObject. CompareTag(Tags.Sword.ToString()))
        {
            if (++spriteIdx >= sprites.Length)
            {
                Destroy(this.gameObject);
            }
            else
                GetComponent<SpriteRenderer>().sprite = sprites[spriteIdx];
        }
    }
}
