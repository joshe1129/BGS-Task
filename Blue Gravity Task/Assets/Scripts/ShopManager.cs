using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Transform shopItemTemplate;
    private Transform shirtWnd;
    private Transform pantsWnd;
    private IShopCustomer shopCustomer;

    private void Awake()
    {
        shirtWnd = transform.Find("ShirtsPnl");
        pantsWnd = transform.Find("PantsPnl");
        if (shirtWnd != null)
        {
            shopItemTemplate = shirtWnd.Find("ItemBttnTemplate");
            shopItemTemplate.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        CreateItemButton(Items.shopItems.shirt_1, Items.Getsprite(Items.shopItems.shirt_1), "Shirt 1", Items.GetCost(Items.shopItems.shirt_1), shirtWnd);
        CreateItemButton(Items.shopItems.shirt_2, Items.Getsprite(Items.shopItems.shirt_2), "Shirt 2", Items.GetCost(Items.shopItems.shirt_2), shirtWnd);
        CreateItemButton(Items.shopItems.shirt_3, Items.Getsprite(Items.shopItems.shirt_3), "Shirt 3", Items.GetCost(Items.shopItems.shirt_3), shirtWnd);
        CreateItemButton(Items.shopItems.shirt_4, Items.Getsprite(Items.shopItems.shirt_4), "Shirt 4", Items.GetCost(Items.shopItems.shirt_4), shirtWnd);

        CreateItemButton(Items.shopItems.pants_1, Items.Getsprite(Items.shopItems.pants_1), "Pants 1", Items.GetCost(Items.shopItems.pants_1), pantsWnd);
        CreateItemButton(Items.shopItems.pants_2, Items.Getsprite(Items.shopItems.pants_2), "Pants 2", Items.GetCost(Items.shopItems.pants_2), pantsWnd);
        CreateItemButton(Items.shopItems.pants_3, Items.Getsprite(Items.shopItems.pants_3), "Pants 3", Items.GetCost(Items.shopItems.pants_3), pantsWnd);
        CreateItemButton(Items.shopItems.pants_4, Items.Getsprite(Items.shopItems.pants_4), "Pants 4", Items.GetCost(Items.shopItems.pants_4), pantsWnd);

        HideShop();
    }

    private void CreateItemButton(Items.shopItems itemID, Sprite itemSprite, string intemName, int itemCost, Transform clotheWnd)
    {
        Transform itemTransform = Instantiate(shopItemTemplate, clotheWnd);
        itemTransform.Find("ItemNameTxt").GetComponent<TMP_Text>().text = intemName;
        itemTransform.Find("ItemCostTxt").GetComponent<TMP_Text>().text = itemCost.ToString() + "$";
        itemTransform.Find("ItemImg").GetComponent<Image>().sprite = itemSprite;
        itemTransform.gameObject.SetActive(true);

        itemTransform.GetComponent<Button>().onClick.AddListener(() => TryBuyItem(itemID));
        
    }

    private void TryBuyItem(Items.shopItems itemID)
    {
        if (shopCustomer.TryBoughtItem(Items.GetCost(itemID)))
        {
            shopCustomer.BoughtItem(itemID);
        }
    }

    public void ShowShop(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }
    public void HideShop()
    {
        gameObject.SetActive(false);
    }


}
