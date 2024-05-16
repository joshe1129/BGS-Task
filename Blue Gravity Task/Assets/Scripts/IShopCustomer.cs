using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer 
{
    void BoughtItem(Items.shopItems itemID);
    bool TryBoughtItem(int goldAmount);

}
