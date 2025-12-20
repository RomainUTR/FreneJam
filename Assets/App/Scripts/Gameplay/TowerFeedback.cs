using UnityEngine;
using Sirenix.OdinInspector;
using MoreMountains.Feedbacks;

public class TowerFeedback : MonoBehaviour
{
    [SerializeField, Required] private MMF_Player player;
    [SerializeField, Required] private MMF_Player shootFeedback;

    public static TowerFeedback Instance;

    void Awake()
    {
        Instance = this;
    }

    public void TowerBuilder()
    {
        player?.PlayFeedbacks();
    }
    
    public void TowerShoot()
    {
        shootFeedback?.PlayFeedbacks();
    }
}
