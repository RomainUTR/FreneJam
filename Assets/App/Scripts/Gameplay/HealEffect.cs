using UnityEngine;

public class HealEffect : MonoBehaviour
{
    public float duration = 0.5f;
    public float maxSize = 4f;

    private float timer = 0f;
    private Renderer rend;
    private Color startColor;

    void Start()
    {
        rend = GetComponent<Renderer>();

        if (rend != null)
        {
            startColor = rend.material.color;
        }

        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float progress = timer / duration;

        if (progress >= 1f)
        {
            Destroy(gameObject);
            return;
        }

        float currentSize = Mathf.Lerp(0f, maxSize, progress);
        transform.localScale = new Vector3(currentSize, currentSize, currentSize);

        if (rend != null)
        {
            Color newColor = startColor;
            newColor.a = Mathf.Lerp(startColor.a, 0f, progress);
            rend.material.color = newColor;
        }
    }
}