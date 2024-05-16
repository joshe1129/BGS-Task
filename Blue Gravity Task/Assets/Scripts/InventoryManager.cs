using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Transform inventoryItemTemplate;
    private Transform shirtWnd;
    private Transform pantsWnd;
    private IShopCustomer shopCustomer;

    private void Awake()
    {
        // Find references to the shirt and pants columns
        shirtWnd = transform.Find("ShirtsColumn");
        pantsWnd = transform.Find("PantsColumn");

        // Disable the inventory item template
        if (shirtWnd != null)
        {
            inventoryItemTemplate = shirtWnd.Find("ItemBttnTemplate");
            inventoryItemTemplate.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    // Create an inventory button for the bought item
    public void CreateInventoryButton(Items.shopItems itemID)
    {
        Sprite itemSprite= null; 
        string itemName = ""; 
        int itemCost = 0;

        // Set item sprite, name, and cost based on the item ID
        switch (itemID)
        {
            case Items.shopItems.shirtNone: break;
            case Items.shopItems.shirt_1:
                itemSprite = Items.Getsprite(Items.shopItems.shirt_1);
                itemName = "Shirt 1";
                itemCost = Items.GetCost(Items.shopItems.shirt_1);
                break;
            case Items.shopItems.shirt_2:
                itemSprite = Items.Getsprite(Items.shopItems.shirt_2);
                itemName = "Shirt 2";
                itemCost = Items.GetCost(Items.shopItems.shirt_2);
                break;
            case Items.shopItems.shirt_3:
                itemSprite = Items.Getsprite(Items.shopItems.shirt_3);
                itemName = "Shirt 3";
                itemCost = Items.GetCost(Items.shopItems.shirt_3);
                break;
            case Items.shopItems.shirt_4:
                itemSprite = Items.Getsprite(Items.shopItems.shirt_4);
                itemName = "Shirt 4";
                itemCost = Items.GetCost(Items.shopItems.shirt_4);
                break;
            case Items.shopItems.pantsNone: break;
            case Items.shopItems.pants_1:
                itemSprite = Items.Getsprite(Items.shopItems.pants_1);
                itemName = "Pants 1";
                itemCost = Items.GetCost(Items.shopItems.pants_1);
                break;
            case Items.shopItems.pants_2:
                itemSprite = Items.Getsprite(Items.shopItems.pants_2);
                itemName = "Pants 2";
                itemCost = Items.GetCost(Items.shopItems.pants_2);
                break;
            case Items.shopItems.pants_3:
                itemSprite = Items.Getsprite(Items.shopItems.pants_3);
                itemName = "Pants 3";
                itemCost = Items.GetCost(Items.shopItems.pants_3);
                break;
            case Items.shopItems.pants_4:
                itemSprite = Items.Getsprite(Items.shopItems.pants_4);
                itemName = "Pants 4";
                itemCost = Items.GetCost(Items.shopItems.pants_4);
                break;
            default:
                itemSprite = null;
                itemName = "";
                itemCost = 0; 
                break;
        }

        // Determine the window to add the inventory button to
        Transform window = itemID > Items.shopItems.pantsNone ? pantsWnd : shirtWnd;

        // Enable interactability of other buttons in the window
        foreach (Transform child in window)
        {
            child.GetComponent<Button>().interactable = true;
        }

        // Instantiate the inventory button template
        Transform itemTransform = Instantiate(inventoryItemTemplate, window);
        itemTransform.Find("ItemNameTxt").GetComponent<TMP_Text>().text = itemName;
        itemTransform.Find("ItemCostTxt").GetComponent<TMP_Text>().text = itemCost.ToString() + "$";
        itemTransform.Find("ItemImg").GetComponent<Image>().sprite = itemSprite;
        itemTransform.gameObject.SetActive(true);
        itemTransform.GetComponent<Button>().interactable = false;

        // Add listener to the inventory button
        itemTransform.GetComponent<Button>().onClick.AddListener(() => TrySellItem(itemCost, itemTransform, itemID));

    }

    // Attempt to sell an item to the shop customer
    public void TrySellItem(int cost, Transform itemBttn, Items.shopItems itemID)
    {
        if (shopCustomer.TrySelltItem(cost, itemID))
        {
            Destroy(itemBttn.gameObject);
        }
        else
        {
            // If the sale fails, enable the inventory button again
            foreach (Transform child in itemID > Items.shopItems.pantsNone ? pantsWnd : shirtWnd)
            {
                child.GetComponent<Button>().interactable = true;
            }
            itemBttn.gameObject.SetActive(true);
            itemBttn.GetComponent<Button>().interactable = false;
        }
    }

    // Show or hide the inventory
    public void ShowInventory(IShopCustomer shopCustomer, bool show)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(show);
    }

}
