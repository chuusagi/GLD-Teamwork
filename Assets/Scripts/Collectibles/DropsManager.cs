using UnityEngine;
using TMPro;

public class DropsManager : MonoBehaviour
{
    public static DropsManager instance;
    public TMP_Text coinText;
    public TMP_Text dropText;
    public int coinCount = 0;
    public int dropCount = 0;


    private void Awake()
    {
        instance = this; //  creates singleton instance

    }

    private void Start()
    {
        coinText.text = "Coins: " + coinCount.ToString(); // initializes coin text
        dropText.text = "Drops: " + dropCount.ToString(); // initializes drop text

    }

    public void IncreaseCoins(int v)
    {
        coinCount += v;
        coinText.text = "Coins: " + coinCount.ToString(); // updates coin text
    }

    public bool SpendCoins(int v)
    {
        if (coinCount >= v)
        {
            coinCount -= v;
            return true;
        }
        return false;
    }

    public void IncreaseDrop(int v)
    {
        dropCount += v;
        dropText.text = "Drops: " + dropCount.ToString();
    }


}