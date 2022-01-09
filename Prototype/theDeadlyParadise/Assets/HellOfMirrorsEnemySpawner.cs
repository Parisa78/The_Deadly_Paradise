using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellOfMirrorsEnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    void Start()
    {
        StartCoroutine(StartTimerAndPlaceEnemy());
    }

    public IEnumerator StartTimerAndPlaceEnemy()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(5, 10));
        Instantiate(enemy);
        StartCoroutine(StartTimerAndPlaceEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
