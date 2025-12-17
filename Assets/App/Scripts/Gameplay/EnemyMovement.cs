using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField, Range(0f, 20f)] private float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;

    private void Start()
    {
        target = Waypoints.points[0];
        speed = speed * Random.Range(0.85f, 1.15f);
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoints();
        }
    }

    void GetNextWaypoints()
    {
        if (wavepointIndex < Waypoints.points.Length - 1)
        {
            wavepointIndex++;
            target = Waypoints.points[wavepointIndex];
        } else
        {
            Destroy(gameObject);
        }
    }
}
