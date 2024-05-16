public interface IShopCustomer 
{
    void BoughtItem(Items.shopItems itemID);
    bool TryBoughtItem(int goldAmount);
    bool TrySelltItem(int goldAmount);
    void SetNearShop(bool isNearShop);

}
