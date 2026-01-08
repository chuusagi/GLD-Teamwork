using UnityEngine;

public class ShopUi : MonoBehaviour
{
    private Transform container;
    private Transform shopItem;

    private void Awake()
    {
        container = transform.Find("container");
        shopItem = container.Find("shopItem");
        shopItem.gameObject.SetActive(false); 

	}

}
