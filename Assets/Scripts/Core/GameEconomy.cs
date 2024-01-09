using TMPro;
using UnityEngine;

public class GameEconomy : MonoBehaviour
{
    public static GameEconomy instance;
    [SerializeField] private TextMeshProUGUI gemText;
    private void Awake()
    {
        instance = this;
        gemText.text = PlayerPrefs.GetInt("Gem", 0).ToString();
    }
    public void GrantGem(int amount)
    {
        var currentAmount = PlayerPrefs.GetInt("Gem", 0);
        currentAmount += amount;
        PlayerPrefs.SetInt("Gem", currentAmount);
    }

    public void SpendGem(int amount)
    {
        var currentAmount = PlayerPrefs.GetInt("Gem", 0);
        currentAmount -= amount;
        PlayerPrefs.SetInt("Gem", currentAmount);
    }
}
