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
        shirtWnd = transform.Find("ShirtsColumn");
        pantsWnd = transform.Find("PantsColumn");
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

    public void CreatePantButton(Items.shopItems itemID, Sprite itemSprite, string intemName, int itemCost)
    {
        foreach (Transform child in pantsWnd)
        {
            child.GetComponent<Button>().interactable = true;
        }

        Transform itemTransform = Instantiate(inventoryItemTemplate, pantsWnd);
        itemTransform.Find("ItemNameTxt").GetComponent<TMP_Text>().text = intemName;
        itemTransform.Find("ItemCostTxt").GetComponent<TMP_Text>().text = itemCost.ToString() + "$";
        itemTransform.Find("ItemImg").GetComponent<Image>().sprite = itemSprite;
        itemTransform.gameObject.SetActive(true);
        itemTransform.GetComponent<Button>().interactable = false;
        itemTransform.GetComponent<Button>().onClick.AddListener(() => TrySellItem(itemCost, itemTransform));
    }
    public void CreateShirtButton(Items.shopItems itemID, Sprite itemSprite, string intemName, int itemCost)
    {
        foreach (Transform child in shirtWnd)
        {
            child.GetComponent<Button>().interactable = true;
        }

        Transform itemTransform = Instantiate(inventoryItemTemplate, shirtWnd);
        itemTransform.Find("ItemNameTxt").GetComponent<TMP_Text>().text = intemName;
        itemTransform.Find("ItemCostTxt").GetComponent<TMP_Text>().text = itemCost.ToString() + "$";
        itemTransform.Find("ItemImg").GetComponent<Image>().sprite = itemSprite;
        itemTransform.gameObject.SetActive(true);
        itemTransform.GetComponent<Button>().interactable = false;
        itemTransform.GetComponent<Button>().onClick.AddListener(() => TrySellItem(itemCost, itemTransform));
    }

    public void TrySellItem(int cost, Transform itemBttn)
    {
        if (shopCustomer.TrySelltItem(cost))
        {
            Destroy(itemBttn.gameObject);
        }
    }

    public void ShowInventory(IShopCustomer shopCustomer, bool show)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(show);
    }

}
