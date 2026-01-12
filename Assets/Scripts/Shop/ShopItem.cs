using UnityEngine;

[System.Serializable]
public class ShopItem
{
    public enum ItemType
    {
        HealthPotion,
        DefensePotion,
        SpeedPotion
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion:
                return 5;
            case ItemType.DefensePotion:
                return 3;
            case ItemType.SpeedPotion:
                return 2;
        }
    }



}