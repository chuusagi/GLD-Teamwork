using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject Shop;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			Shop.SetActive(!Shop.activeSelf);
		}
	}
}
