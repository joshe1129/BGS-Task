using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;

    //Check if the collider is a IShopCustomer and then show the shop and notify the shop customer that it's near the shop
    private void OnTriggerEnter2D(Collider2D collider)
    {
        IShopCustomer shopCustomer = collider.GetComponent<IShopCustomer>();
        if (shopCustomer != null)
        {
            shopManager.ShowShop(shopCustomer);
            shopCustomer.SetNearShop(true);
        }
    }

    //Hide the shop and notify the shop customer that it's no longer near the shop if the collider is a IShopCustomer
    private void OnTriggerExit2D(Collider2D collider)
    {
        IShopCustomer shopCustomer = collider.GetComponent<IShopCustomer>();
        if (shopCustomer != null)
        {
            shopManager.HideShop();
            shopCustomer.SetNearShop(false);
        }

    }
}
