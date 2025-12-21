using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerTooltip : MonoBehaviour
{
    [SerializeField, Required] private GameObject tooltip;
    [SerializeField, Required] private TMP_Text upgradeCostText;
    [SerializeField, Required] private TMP_Text titleUpgradeText;
    [SerializeField, Required] private Button upgradeButton;

    [SerializeField, Required] private TowerBuilder towerBuilder;
    private Turret target;

    private void Start()
    {
        upgradeButton.interactable = false;
    }


    private void Update()
    {
        if (target != null)
        {
            if (Economy.gold >= target.turretPriceUpgrade && !target.IsMaxLevel())
        	{
            	upgradeButton.interactable = true;
        	}
        	else
        	{
            	upgradeButton.interactable = false;
        	}
        }
    }

    public void SetTarget(Turret _target)
    {
        target = _target;
        transform.position = target.transform.position;
		titleUpgradeText.text = "Tower (Level " + (target.turretLevel).ToString() + ")";
        upgradeCostText.text = "$" + target.turretPriceUpgrade.ToString();
        tooltip.SetActive(true);
    }

    public void Hide()
    {
        tooltip.SetActive(false);
		target.selectionRing.SetActive(false);
        target = null;
    }

    public void Upgrade()
    {
        if (target != null)
        {
            towerBuilder.UpgradeSelectedTurret();
            Debug.Log("Upgrade");
            Hide();
        }
    }
}
