using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Transform shopItemTemplate;
    private Transform shirtWnd;
    private Transform pantsWnd;
    private IShopCustomer shopCustomer;

    // Find references to shirt and pants panels, and disable the item template
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

    // Create item buttons for shirts and pants, and hide the shop
    private void Start()
    {
        CreateItemButton(Items.shopItems.shirt_1, "Shirt 1", shirtWnd);
        CreateItemButton(Items.shopItems.shirt_2, "Shirt 2", shirtWnd);
        CreateItemButton(Items.shopItems.shirt_3, "Shirt 3", shirtWnd);
        CreateItemButton(Items.shopItems.shirt_4, "Shirt 4", shirtWnd);

        CreateItemButton(Items.shopItems.pants_1, "Pants 1", pantsWnd);
        CreateItemButton(Items.shopItems.pants_2, "Pants 2", pantsWnd);
        CreateItemButton(Items.shopItems.pants_3, "Pants 3", pantsWnd);
        CreateItemButton(Items.shopItems.pants_4, "Pants 4", pantsWnd);

        HideShop();
    }

    // Create an item button for a specific item
    private void CreateItemButton(Items.shopItems itemID, string itemName, Transform clotheWnd)
    {
        Sprite itemSprite = Items.Getsprite(itemID);
        int itemCost = Items.GetCost(itemID);

        Transform itemTransform = Instantiate(shopItemTemplate, clotheWnd);
        itemTransform.Find("ItemNameTxt").GetComponent<TMP_Text>().text = itemName;
        itemTransform.Find("ItemCostTxt").GetComponent<TMP_Text>().text = itemCost.ToString() + "$";
        itemTransform.Find("ItemImg").GetComponent<Image>().sprite = itemSprite;
        itemTransform.gameObject.SetActive(true);
        itemTransform.GetComponent<Button>().onClick.AddListener(() => TryBuyItem(itemID));
    }


    // Try to buy an item when the button is clicked
    private void TryBuyItem(Items.shopItems itemID)
    {
        if (shopCustomer.TryBoughtItem(Items.GetCost(itemID)))
        {
            shopCustomer.BoughtItem(itemID);
        }
    }

    // Show the shop
    public void ShowShop(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }

    // Hide the shop
    public void HideShop()
    {
        gameObject.SetActive(false);
    }
}
