using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items
{
    public enum shopItems
    {
        shirtNone,
        shirt_1,
        shirt_2,
        shirt_3,
        shirt_4,
        pantsNone,
        pants_1,
        pants_2,
        pants_3,
        pants_4
    }
    public static int GetCost(shopItems item)
    {
        switch (item)
        {
            case shopItems.shirtNone: return 0;
            case shopItems.shirt_1: return 10;
            case shopItems.shirt_2: return 40;
            case shopItems.shirt_3: return 70;
            case shopItems.shirt_4: return 20;
            case shopItems.pantsNone: return 0;
            case shopItems.pants_1: return 70;
            case shopItems.pants_2: return 100;
            case shopItems.pants_3: return 130;
            case shopItems.pants_4: return 90;
            default: return 0;
        }
    }
    public static Sprite Getsprite(shopItems item)
    {
        switch (item)
        {
            case shopItems.shirtNone: return GameAssets.instance.shirtNone;
            case shopItems.shirt_1: return GameAssets.instance.shirt_1;
            case shopItems.shirt_2: return GameAssets.instance.shirt_2;
            case shopItems.shirt_3: return GameAssets.instance.shirt_3;
            case shopItems.shirt_4: return GameAssets.instance.shirt_4;
            case shopItems.pantsNone: return GameAssets.instance.pantsNone;
            case shopItems.pants_1: return GameAssets.instance.pants_1;
            case shopItems.pants_2: return GameAssets.instance.pants_2;
            case shopItems.pants_3: return GameAssets.instance.pants_3;
            case shopItems.pants_4: return GameAssets.instance.pants_4;
            default: return GameAssets.instance.shirtNone;
        }
    }


}
