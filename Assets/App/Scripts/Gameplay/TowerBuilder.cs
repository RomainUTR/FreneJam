using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class TowerBuilder : MonoBehaviour
{
    [TitleGroup("General")]
    [SerializeField, Range(0,10)] private int maxTowers = 5;
    [SerializeField] private Vector3 gridOffset = new Vector3(0.5f, 0, 0.5f);

    [TabGroup("Configuration"), SerializeField, Required] private Camera mainCamera;
    [TabGroup("Configuration"), SerializeField, Required] private LayerMask groundLayer;
    [TabGroup("Configuration"), SerializeField, Required] private LayerMask obstacleLayer;
    [TabGroup("Configuration"), SerializeField, Required] private LayerMask towerLayer;

    [TabGroup("Visual"), SerializeField, Required] private GameObject towerPrefab;
    [TabGroup("Visual"), SerializeField, Required] private GameObject ghostPrefab;
    [TabGroup("Visual"), SerializeField, Required] private Material validMaterial;
    [TabGroup("Visual"), SerializeField, Required] private Material invalidMaterial;
    [TabGroup("Visual"), SerializeField, Required] private TMP_Text builderButtonText;
    [TabGroup("Visual"), SerializeField, Required] private Button builderButton;

    private int currentTowerCount = 0;
    private bool isBuilding = false;
    private GameObject currentGhost;
    private Renderer[] ghostRenderers;
    private bool canBuildLocation = false;
    private Turret selectedTurret;

    void Start()
    {
        currentGhost = Instantiate(ghostPrefab);
        ghostRenderers = currentGhost.GetComponentsInChildren<Renderer>();

        foreach (Collider c in currentGhost.GetComponentsInChildren<Collider>()) Destroy(c);

        currentGhost.SetActive(false);
        builderButton.interactable = true;
        builderButtonText.text = "Build Tower : " + currentTowerCount + "/" + maxTowers;
    }

    void Update()
    {
        if (isBuilding)
        {
            if (Input.GetMouseButtonDown(1))
            {
                StopBuilding();
                return;
            }

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, groundLayer))
            {
                currentGhost.SetActive(true);
                
                float x = Mathf.Floor(hit.point.x) + gridOffset.x;
                float z = Mathf.Floor(hit.point.z) + gridOffset.z;
                float y = hit.point.y;

                Vector3 finalPosition = new Vector3(x, y, z);
                currentGhost.transform.position = finalPosition;

                if (Physics.CheckSphere(finalPosition + Vector3.up * 0.5f, 0.4f, obstacleLayer))
                {
                    canBuildLocation = false;
                    UpdateGhostColor(invalidMaterial);
                } else
                {
                    canBuildLocation = true;
                    UpdateGhostColor(validMaterial);
                }

                if (Input.GetMouseButtonDown(0) && canBuildLocation)
                {
                    if (EventSystem.current.IsPointerOverGameObject()) return;
                    BuildTower(finalPosition);
                }
            }
            else
            {
                currentGhost.SetActive(false);
            }
        } else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;

                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f, towerLayer))
                {
                    Turret newTurret = hit.collider.GetComponentInParent<Turret>();

                    if (newTurret != null)
                    {
                        if (selectedTurret != null)
                        {
                            selectedTurret.SetSelected(false);
                        }

                        selectedTurret = newTurret;
                        selectedTurret.SetSelected(true);

                        Debug.Log("Selection : " + selectedTurret.name);
                    }
                }
                else
                {
                    if (selectedTurret != null)
                    {
                        selectedTurret.SetSelected(false);
                        selectedTurret = null;
                    }
                }
            }
        }
    }

    public void StartBuilding()
    {
        if (currentTowerCount < maxTowers)
        {
            isBuilding = true;
        }
    }

    public void StopBuilding()
    {
        isBuilding = false;
        if (currentGhost != null) currentGhost.SetActive(false);
        if (currentTowerCount >= maxTowers)
        {
            builderButton.interactable = false;
        }
    }

    void BuildTower(Vector3 pos)
    {
        GameObject newTower = Instantiate(towerPrefab, pos, Quaternion.identity);

        int towerLayerIndex = LayerMask.NameToLayer("Tower");
        newTower.layer = towerLayerIndex;

        foreach (Transform t in newTower.transform)
        {
            t.gameObject.layer = towerLayerIndex;
        }

        currentTowerCount++;
        builderButtonText.text = "Build Tower : " + currentTowerCount + "/" + maxTowers;

        StopBuilding();
    }

    void UpdateGhostColor(Material mat)
    {
        foreach (Renderer r in ghostRenderers) r.material = mat;
    }
}