using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public GameObject Shop;
    private Transform container;
    private Transform shopItem;
    private IShopPurchase shopPurchase;

    [SerializeField] private Button buyHealthPotion;
    [SerializeField] private Button buyDefPotion;
    [SerializeField] private Button buySpeedPotion;

    [SerializeField] private DropsManager dropsManager; // Reference to DropsManager

    void Start()
    {
        // Hook up button click events
        buyHealthPotion.onClick.AddListener(() => BuyItem(ShopItem.ItemType.HealthPotion));
        buyDefPotion.onClick.AddListener(() => BuyItem(ShopItem.ItemType.DefensePotion));
        buySpeedPotion.onClick.AddListener(() => BuyItem(ShopItem.ItemType.SpeedPotion));
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            bool shopActive = !Shop.activeSelf;
            Shop.SetActive(shopActive);

            if (shopActive)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f; // Pause the game
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f; // Resume the game
            }
        }
    }



    private void BuyItem(ShopItem.ItemType itemType)
    {
        if (dropsManager.coinCount > 0) // Check if there are enough coins
        {
            shopPurchase.BoughtItem(itemType); // Notify the purchase handler
            dropsManager.coinCount--; // Deduct coins
            dropsManager.coinText.text = dropsManager.coinCount.ToString(); // Update coin text

            Debug.Log($"Successfully purchased {itemType}!"); // Confirmation message
        }
        else
        {
            Debug.Log("Not enough coins to buy this item!");
        }
    }



}