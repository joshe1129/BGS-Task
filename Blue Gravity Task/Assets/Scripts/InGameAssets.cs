using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;

    public static GameAssets instance
    {
        get
        {
            if (_instance == null) _instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _instance;
        }
    }

    public Sprite shirtNone;
    public Sprite shirt_1;
    public Sprite shirt_2;
    public Sprite shirt_3;
    public Sprite shirt_4;
    public Sprite pantsNone;
    public Sprite pants_1;
    public Sprite pants_2;
    public Sprite pants_3;
    public Sprite pants_4;
}
