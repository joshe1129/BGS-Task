using UnityEngine;
using System;
using static Items;

public class Player : MonoBehaviour, IShopCustomer
{
    private int goldAmount = 300;
    public event EventHandler OnGoldAmountChanged;
    private SpriteRenderer bodySpriteRenderer;
    [SerializeField] private Texture2D skinsTexture;
    [SerializeField] InventoryManager inventoryManager;
    private bool nearShopKeeper = false;

    private void Awake()
    {
        bodySpriteRenderer = transform.Find("Body").GetComponent<SpriteRenderer>();
        if (bodySpriteRenderer == null)
        {
            Debug.LogError("No se encontró el componente SpriteRenderer en el objeto body.");
        }
    }
    private void Start()
    {
        BoughtItem(Items.shopItems.shirt_1);
        BoughtItem(Items.shopItems.pants_1);
    }

    void Update()
    {
        // Verifica si la tecla "I" ha sido presionada
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Activa o muestra el inventario si está desactivado o oculto, y viceversa
            inventoryManager.ShowInventory(this, !inventoryManager.gameObject.activeSelf);
        }
    }

    public int GetGoldAmount() 
    { 
        return goldAmount;
    }

    public bool IsNearShopKeeper() { return nearShopKeeper; }
    public void EquipItem(Items.shopItems itemID)
    {
        switch (itemID)
        {
            case shopItems.shirtNone: break;
            case shopItems.shirt_1:
                inventoryManager.CreateShirtButton(itemID, Items.Getsprite(Items.shopItems.shirt_1), "Shirt 1", Items.GetCost(Items.shopItems.shirt_1));
                EquipShirt(itemID);
                break;
            case shopItems.shirt_2:
                inventoryManager.CreateShirtButton(itemID, Items.Getsprite(Items.shopItems.shirt_2), "Shirt 2", Items.GetCost(Items.shopItems.shirt_2));
                EquipShirt(itemID); 
                break;
            case shopItems.shirt_3: 
                inventoryManager.CreateShirtButton(itemID, Items.Getsprite(Items.shopItems.shirt_3), "Shirt 3", Items.GetCost(Items.shopItems.shirt_3)); 
                EquipShirt(itemID); 
                break;
            case shopItems.shirt_4:
                inventoryManager.CreateShirtButton(itemID, Items.Getsprite(Items.shopItems.shirt_4), "Shirt 4", Items.GetCost(Items.shopItems.shirt_3));
                EquipShirt(itemID); 
                break;
            case shopItems.pantsNone: break;
            case shopItems.pants_1:
                inventoryManager.CreatePantButton(itemID, Items.Getsprite(Items.shopItems.pants_1), "Pants 1", Items.GetCost(Items.shopItems.pants_1));
                EquipPants(itemID);
                break;
            case shopItems.pants_2:
                inventoryManager.CreatePantButton(itemID, Items.Getsprite(Items.shopItems.pants_2), "Pants 2", Items.GetCost(Items.shopItems.pants_2));
                EquipPants(itemID);
                break;
            case shopItems.pants_3:
                inventoryManager.CreatePantButton(itemID, Items.Getsprite(Items.shopItems.pants_3), "Pants 3", Items.GetCost(Items.shopItems.pants_3));
                EquipPants(itemID); 
                break;
            case shopItems.pants_4:
                inventoryManager.CreatePantButton(itemID, Items.Getsprite(Items.shopItems.pants_4), "Pants 4", Items.GetCost(Items.shopItems.pants_4));
                EquipPants(itemID); 
                break;
            default: break;
        }
    }
    public void EquipShirt(Items.shopItems itemID)
    {
        if (bodySpriteRenderer != null)
        {
            Texture2D spriteSheetTexture = (Texture2D)bodySpriteRenderer.sprite.texture;
            Color[] bodySpriteSheet = skinsTexture.GetPixels(0, 43 + (32 * ((int)itemID - 1)), 180, 7);
            spriteSheetTexture.SetPixels(0, 139, 180, 7, bodySpriteSheet);
            spriteSheetTexture.Apply();
        }
    }
    public void EquipPants(Items.shopItems itemID)
    {
        if (bodySpriteRenderer != null)
        {
            Texture2D spriteSheetTexture = (Texture2D)bodySpriteRenderer.sprite.texture;
            Color[] legsTexture = skinsTexture.GetPixels(0, 32 + (32 * ((int)itemID - 6)), 180, 11);
            spriteSheetTexture.SetPixels(0, 128, 180, 11, legsTexture);
            spriteSheetTexture.Apply();
        }
    }
    public void BoughtItem(Items.shopItems itemID)
    {
        EquipItem(itemID);
    }
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

    public void SetNearShop(bool isNearShop)
    {
        nearShopKeeper = isNearShop;
        inventoryManager.ShowInventory(this, nearShopKeeper);
    }

    public bool TrySelltItem(int earnGoldAmount)
    {
        if (nearShopKeeper)
        {
            goldAmount += earnGoldAmount;
            OnGoldAmountChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else { return false; }
    }

}
