using UnityEngine;
using TMPro;
using System;

public class HUD_Manager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text goldText;

    void Start()
    {
        player.OnGoldAmountChanged += UpdateGoldText;
        UpdateGoldText(null, EventArgs.Empty);
    }

    private void UpdateGoldText(object sender, EventArgs e)
    {
        int goldAmount = player.GetGoldAmount();
        goldText.text = goldAmount.ToString() + "$";
    }

    private void OnDestroy()
    {
        player.OnGoldAmountChanged -= UpdateGoldText;
    }
}
