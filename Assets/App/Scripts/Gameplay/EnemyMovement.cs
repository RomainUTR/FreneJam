using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField, Required, InlineEditor] private EnemySettings settings;

    private Transform target;
    private int wavepointIndex = 0;

    private void Start()
    {
        target = Waypoints.points[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * settings.speed * Time.deltaTime, Space.World);

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
            if (PlayerState.lives <= 0)
            {
                Destroy(gameObject);
                Debug.Log("Game Over!");
                UIManager.Instance.GameOver();
                Time.timeScale = 0f;
                PlayerState.isAlive = false;
            }
            else
            {
                PlayerState.lives--;
                UIManager.Instance.UpdateUI();
                Destroy(gameObject);
            }
        }
    }
}
