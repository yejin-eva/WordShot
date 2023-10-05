using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance = null;

    public string PlayerName = "Eva";
    private int hp = 300;
    private int initialHp = 300;
    

    private void Awake()
    {
        instance = this;
    }

    public int PlayerHp
    {
        get { return hp; }
    }

    public int PlayerInitialHp
    {
        get { return initialHp; }
    }

    public void SetHp(int hpValue)
    {
        hp = hpValue;
    }

}
