using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySubWeaponConfig", menuName = "EnemyConfigs/EnemySubWeaponConfig")]
public class EnemySubWeaponConfig : ScriptableObject
{
    public string subWeaponName;
    public int hitAmount;
}
