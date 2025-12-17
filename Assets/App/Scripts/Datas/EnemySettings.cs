using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings_new", menuName = "TowerDefense/EnemySettings")]
public class EnemySettings : ScriptableObject
{
    [Range(0f, 200f)] public float startHealth = 100f;
    [Range(0f, 20f)] public float speed = 10f;
    [Range(0, 100)] public int coinPerKill = 10;
}
