using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulEaterHpBarUI : MonoBehaviour
{
    [SerializeField] SoulEaterCtrl soulEaterCtrl;
    [SerializeField] private Image hpBarImage;
    
    void Start()
    {
        hpBarImage.fillAmount = 100f;
        soulEaterCtrl.OnSoulEaterHit += SoulEaterCtrl_OnSoulEaterHit;
    }

    private void SoulEaterCtrl_OnSoulEaterHit(object sender, System.EventArgs e)
    {
        hpBarImage.fillAmount = (float)soulEaterCtrl.hp / (float)soulEaterCtrl.initialHp;
        Debug.Log("Monster Hit");
    }

}
