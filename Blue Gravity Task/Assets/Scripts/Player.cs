using UnityEngine;
using System;
using static UnityEditor.Progress;

public class Player : MonoBehaviour, IShopCustomer
{
    private int goldAmount = 1300;
    public event EventHandler OnGoldAmountChanged;
    private SpriteRenderer bodySpriteRenderer;
    [SerializeField] private Texture2D skinsTexture;
    private void Start()
    {
        bodySpriteRenderer = transform.Find("Body").GetComponent<SpriteRenderer>();
        if (bodySpriteRenderer == null)
        {
            Debug.LogError("No se encontró el componente SpriteRenderer en el objeto body.");
        }
        EquipShirt(2);
        EquipPants(4);
    }
    public int GetGoldAmount() 
    { 
        return goldAmount;
    }
    public void EquipShirt(int id)
    {
        Debug.Log("shirt number: " + id);
        if (bodySpriteRenderer != null)
        {
            if (id == 0)
            {
                return;
            }
            else
            {
                // Acceder a la textura (SpriteSheet) asociada al SpriteRenderer
                Texture2D spriteSheetTexture = (Texture2D)bodySpriteRenderer.sprite.texture;
                Color[] bodySpriteSheet = skinsTexture.GetPixels(0, 43 + (32 * (id - 1)), 180, 7);
                spriteSheetTexture.SetPixels(0, 139, 180, 7, bodySpriteSheet);
                spriteSheetTexture.Apply();
            }
        }

    }
    public void EquipPants(int id)
    {
        Debug.Log("Pants number: " + id);
        if (bodySpriteRenderer != null)
        {
            if (id == 5)
            {
                return;
            }
            else
            {
                Texture2D spriteSheetTexture = (Texture2D)bodySpriteRenderer.sprite.texture;
                Color[] legsTexture = skinsTexture.GetPixels(0, 32 + (32 * (id - 1)), 180, 11);
                spriteSheetTexture.SetPixels(0, 128, 180, 11, legsTexture);
                spriteSheetTexture.Apply();
            }
        }

    }
    public void BoughtItem(Items.shopItems itemID)
    {
        Debug.Log("Item: " + itemID + " bought." );
        if (itemID <= Items.shopItems.shirt_4)
        {
            EquipShirt((int)itemID);
        }
        else
        {
            EquipPants((int)itemID - 5);
        }
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
}
