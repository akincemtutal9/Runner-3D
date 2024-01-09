using UnityEngine;
using TMPro;
public class InGameGem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gemText;
    private PlayerGem playerGem;
    private void Awake()
    {
        playerGem = FindObjectOfType<PlayerGem>();
    }

    private void Update()
    {
        gemText.text = playerGem.currentGem.ToString();
    }
}