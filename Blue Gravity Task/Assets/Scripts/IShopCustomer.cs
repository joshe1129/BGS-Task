public interface IShopCustomer 
{
    void BoughtItem(Items.shopItems itemID);
    bool TryBoughtItem(int goldAmount);
    bool TrySelltItem(int goldAmount, Items.shopItems itemID);
    void SetNearShop(bool isNearShop);

}
