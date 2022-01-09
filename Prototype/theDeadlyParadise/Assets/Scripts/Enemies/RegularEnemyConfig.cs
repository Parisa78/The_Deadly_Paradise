using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RegularEnemyConfig", menuName = "EnemyConfigs/RegularEnemyConfig")]
public class RegularEnemyConfig : ScriptableObject
{
    public string enemyName;
    public int maxHP;
    public float attackMaxTime;
    public int attackAmount;
}
