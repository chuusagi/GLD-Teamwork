using UnityEngine;

// an interface is used to notify other scripts when an item is purchased from the shop
public interface IShopPurchase
{
    void BoughtItem(ShopItem.ItemType itemType);
    bool CanBuyItem(int coinAmount);
}
