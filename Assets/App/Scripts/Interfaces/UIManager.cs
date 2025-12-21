using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField, Required] private TMP_Text goldText;
    [SerializeField, Required] private TMP_Text heartText;
    [SerializeField, Required] private GameObject gameOverPanel;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        goldText.text = Economy.gold.ToString();
        heartText.text = PlayerState.lives.ToString();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
