﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OakRootsDanger : MonoBehaviour
{
    public IEnumerator StartRootsDanger()
    {
        yield return new WaitForSeconds(0.3f);
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
