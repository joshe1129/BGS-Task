using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] ShopManager shopManager;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        IShopCustomer shopCustomer = collider.GetComponent<IShopCustomer>();
        if (shopCustomer != null)
        {
            shopManager.ShowShop(shopCustomer);
            shopCustomer.SetNearShop(true);
        }
    }

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
