using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerTooltip : MonoBehaviour
{
    [SerializeField, Required] private GameObject tooltip;
    [SerializeField, Required] private TMP_Text upgradeCostText;
    [SerializeField, Required] private Button upgradeButton;

    private Turret target;

    public void SetTarget(Turret _target)
    {
        target = _target;
        transform.position = target.transform.position;
        upgradeCostText.text = "$" + "150";
        tooltip.SetActive(true);
    }

    public void Hide()
    {
        tooltip.SetActive(false);
    }

    public void Upgrade()
    {
        if (target != null)
        {
            Debug.Log("Upgrade");
            Hide();
        }
    }
}
