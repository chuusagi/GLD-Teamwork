using Unity.VisualScripting;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject Shop;
    private Transform container;
    private Transform shopItem;
    private IShopPurchase shopPurchase;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Shop.SetActive(!Shop.activeSelf);

            if (Shop.activeSelf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
   


    private void BuyItem(ShopItem.ItemType itemType)
    {
        shopPurchase.BoughtItem(itemType); // notify the purchase handler
    }

   

}