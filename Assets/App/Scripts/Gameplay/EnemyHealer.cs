using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyHealer : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float healRange = 3f;
    [SerializeField, Range(0f, 100f)] private float healAmount = 10f;
    [SerializeField, Range(0f, 10f)] private float healRate = 1f;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField, Required] private GameObject healEffect;

    private void Start()
    {
        InvokeRepeating("HealNearbyEnemies", 1f, healRate);
    }

    void HealNearbyEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, healRange, enemyLayer);

        bool hasHealed = false;

        foreach (Collider col in colliders)
        {
            if (col.gameObject == gameObject) continue;

            Enemy e = col.GetComponent<Enemy>();
            if (e != null)
            {
                e.Heal(healAmount);
                hasHealed = true;
            }
        }

        if (hasHealed &&  healEffect != null)
        {
            Instantiate(healEffect, transform.position, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRange);
    }
}
