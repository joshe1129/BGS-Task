using UnityEngine;
using System;

public class Player : MonoBehaviour, IShopCustomer
{
    private int goldAmount = 300;
    public event EventHandler OnGoldAmountChanged; // Event triggered when gold amount changes
    private SpriteRenderer bodySpriteRenderer;
    [SerializeField] private Texture2D skinsTexture; // Texture containing player skins
    [SerializeField] InventoryManager inventoryManager;
    private bool nearShopKeeper = false;  // Flag indicating if the player is near a shopkeeper

    private void Awake()
    {
        // Get the SpriteRenderer component of the player's body
        bodySpriteRenderer = transform.Find("Body").GetComponent<SpriteRenderer>();
        if (bodySpriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the body object.");
        }
    }

    // Equip default items when the game starts
    private void Start()
    {
        BoughtItem(Items.shopItems.shirt_1);
        BoughtItem(Items.shopItems.pants_1);
    }

    void Update()
    {
        // Toggle inventory display when "I" key is pressed
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryManager.ShowInventory(this, !inventoryManager.gameObject.activeSelf);
        }
    }

    // Get the player's current gold amount
    public int GetGoldAmount() 
    { 
        return goldAmount;
    }

    // Equip the specified item to the player
    public void EquipItem(Items.shopItems itemID)
    {
        if (bodySpriteRenderer == null) return;
            
        if (itemID < Items.shopItems.pantsNone)
        {
            Texture2D spriteSheetTexture = (Texture2D)bodySpriteRenderer.sprite.texture;
            Color[] bodySpriteSheet = skinsTexture.GetPixels(0, 43 + (32 * ((int)itemID - 1)), 180, 7);
            spriteSheetTexture.SetPixels(0, 139, 180, 7, bodySpriteSheet);
            spriteSheetTexture.Apply();
        }
        else
        {
            Texture2D spriteSheetTexture = (Texture2D)bodySpriteRenderer.sprite.texture;
            Color[] legsTexture = skinsTexture.GetPixels(0, 32 + (32 * ((int)itemID - 6)), 180, 11);
            spriteSheetTexture.SetPixels(0, 128, 180, 11, legsTexture);
            spriteSheetTexture.Apply();
        }
    }

    // Handle buying an item: Create an inventory button for the bought item and equip it
    public void BoughtItem(Items.shopItems itemID)
    {
        inventoryManager.CreateInventoryButton(itemID);
        EquipItem(itemID);
    }

    // Attempt to buy an item: // Check if has enough gold, subtract the gold and trigger the event
    public bool TryBoughtItem(int spendGoldAmount)
    {
        if (GetGoldAmount() >= spendGoldAmount) 
        {
            goldAmount -= spendGoldAmount;
            OnGoldAmountChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else { return false; }
    }

    // Set the flag indicating if the player is near a shopkeeper
    public void SetNearShop(bool isNearShop)
    {
        nearShopKeeper = isNearShop;
        inventoryManager.ShowInventory(this, nearShopKeeper); // Show or hide the inventory based on the player's proximity to the shopkeeper
    }

    // Attempt to sell an item: Add earned gold, trigger the gold event and if not near equip the item
    public bool TrySelltItem(int earnGoldAmount, Items.shopItems itemID)
    {
        if (nearShopKeeper)
        {
            goldAmount += earnGoldAmount;
            OnGoldAmountChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else {
            EquipItem(itemID);
            return false; 
        }
    }
}
