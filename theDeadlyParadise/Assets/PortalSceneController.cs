using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SaveSystem.SaveData(FindObjectOfType<PlayerController>().transform.position,
            Camera.main.transform.position);
    }
}
