using UnityEngine;
using Sirenix.OdinInspector;
using Shapes;

public class Enemy : MonoBehaviour
{
    [TitleGroup("Stats")]
    [SerializeField, Required, InlineEditor] private EnemySettings settings;
    private float health;

    [TitleGroup("Shapes Interface")]
    [SerializeField, Required] private Rectangle healthBar;
    [SerializeField] private Color colorFull = Color.green;
    [SerializeField] private Color colorLow = Color.red;

    private float maxBarWidth;

    private void Start()
    {
        health = settings.startHealth;

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
            float pct = health / settings.startHealth;
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

    public void Heal(float amount)
    {
        health += amount;
        if (health > settings.startHealth)
        {
            health = settings.startHealth;
        }

        if (healthBar != null)
        {
            float pct = health / settings.startHealth;
            healthBar.Width = maxBarWidth * Mathf.Max(0, pct);
            healthBar.Color = Color.Lerp(colorLow, colorFull, pct);
        }
    }
}
