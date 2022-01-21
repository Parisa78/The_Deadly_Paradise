using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BreakableMirrorController : MonoBehaviour
{
    protected string[] effectiveSwords;

    private void Awake()
    {
        effectiveSwords = new string[] { Tags.Sword.ToString() }; //TODO: make the GlassSword
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Array.IndexOf(effectiveSwords, collision.gameObject.tag.ToString()) > -1)  //collision.gameObject. CompareTag(Tags.Sword.ToString()))
        {
            FindObjectOfType<cyrusInMirrorController>().isFound = true;
            Destroy(this.gameObject);
        }
    }
}
