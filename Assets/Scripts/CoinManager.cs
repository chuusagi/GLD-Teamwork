using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public TMP_Text coinText;
    public int coinCount = 0;
    
    

    private void Awake()
    {
            instance = this; //  creates singleton instance

    }

    private void Start()
    {
        coinText.text = "Coins: " + coinCount.ToString(); // initializes coin text
    }

    public void IncreaseCoins(int v)
    {
        coinCount += v;
        coinText.text = "Coins: " + coinCount.ToString(); // updates coin text
    }


}
