using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField, Required] private TMP_Text goldText;

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
    }
}
