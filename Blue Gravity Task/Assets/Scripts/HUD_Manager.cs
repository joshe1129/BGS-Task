using UnityEngine;
using TMPro;
using System;

public class HUD_Manager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text goldText;

    //Suscribe the method and update the gold at start
    void Start()
    {
        player.OnGoldAmountChanged += UpdateGoldText;
        UpdateGoldText(null, EventArgs.Empty);
    }

    //Update the gold text
    private void UpdateGoldText(object sender, EventArgs e)
    {
        int goldAmount = player.GetGoldAmount();
        goldText.text = goldAmount.ToString() + "$";
    }

    //Unsubscribe the method when the object is destroyed
    private void OnDestroy()
    {
        player.OnGoldAmountChanged -= UpdateGoldText;
    }
}
