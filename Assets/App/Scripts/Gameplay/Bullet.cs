using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    [SerializeField, Range(0f, 100f)] private float bulletSpeed = 50f;
    public float speed = 20f;
    public int damage = 25;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        transform.LookAt(target);
    }

    void HitTarget()
    {
        // TODO : effect

        Destroy(gameObject);

        Enemy e = target.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }
        else
        {
            Destroy(target.gameObject);
        }
    }
}
