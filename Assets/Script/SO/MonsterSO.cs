using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObject")]
public class MonsterSO : ScriptableObject
{//Using this approach to store object data allows identical objects to share a single copy of the data, reducing memory usage.

    [Header("Basic Data")]
    public float speed;
    public float maxHealth;
    public float damage;
}
