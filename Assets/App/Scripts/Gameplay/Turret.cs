using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [TitleGroup("Configuration")]
    [SerializeField, Range(0f, 10f)] private float turretRange = 5f;
    [SerializeField, Range(0f, 50f)] private float turnSpeed = 5f;

    [TitleGroup("Setup")]
    [SerializeField, Required] private string enemyTag = "Enemy";
    [SerializeField, Required] private Transform partToRotate;
    [SerializeField, Required] private GameObject bulletPrefab;
    [SerializeField, Required] private Transform firePoint;

    [TitleGroup("Bullet Settings")]
    [SerializeField, Range(0f, 5f)] private float fireRate = 1f;
    private float fireCountdown = 0f;

    private Transform target;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= turretRange)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }
}
