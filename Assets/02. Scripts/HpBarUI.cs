using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    [SerializeField] private MonsterCtrl monsterCtrl;
    [SerializeField] private Image hpBarImage;

    private void Start()
    {
        hpBarImage.fillAmount = 100f;
        monsterCtrl.OnMonsterHit += MonsterCtrl_OnMonsterHit;
    }

    private void MonsterCtrl_OnMonsterHit(object sender, EventArgs e)
    {
        hpBarImage.fillAmount = (float)monsterCtrl.hp / (float)monsterCtrl.initialHp;
        Debug.Log("Monster Hit");
    }

}
