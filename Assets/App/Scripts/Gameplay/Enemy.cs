using UnityEngine;
using Sirenix.OdinInspector;
using Shapes;

public class Enemy : MonoBehaviour
{
    [TitleGroup("Stats")]
    [SerializeField, Range(0f, 200f)] private float startHealth = 100f;
    private float health;

    [TitleGroup("Shapes Interface")]
    [SerializeField, Required] private Rectangle healthBar;
    [SerializeField] private Color colorFull = Color.green;
    [SerializeField] private Color colorLow = Color.red;

    private float maxBarWidth;

    private void Start()
    {
        health = startHealth;

        if (healthBar != null)
        {
            maxBarWidth = healthBar.Width;
            healthBar.Color = colorFull;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        
        if (healthBar != null)
        {
            float pct = health / startHealth;
            healthBar.Width = maxBarWidth * Mathf.Max(0, pct);
            healthBar.Color = Color.Lerp(colorLow, colorFull, pct);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
