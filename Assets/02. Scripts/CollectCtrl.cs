using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCtrl : MonoBehaviour
{
    //UIManager uiManager;
    [SerializeField] private AudioClip itemCollectSfx; //æ∆¿Ã≈€ »πµÊ ªÁøÓµÂ

    
    [SerializeField] private GameObject player;
    private PlayerCtrl playerCtrl;

    [SerializeField] private AudioClip potionCollectSfx;
    private void Start()
    {
        //uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        playerCtrl = player.GetComponent<PlayerCtrl>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //collecting box item
        if (other.tag == "ITEM")
        {
            Debug.Log("Item found");
            UIManager.instance.itemAmount += 1;
            Destroy(other.gameObject);
            
            //play collect sound
            GameManager.instance.PlaySfx(transform.position, itemCollectSfx);
        }
        //collecting potion item
        if (other.tag == "POTION")
        {
            Debug.Log("Potion found");
            if(PlayerData.instance.PlayerHp < 300)
            {
                PlayerData.instance.SetHp(PlayerData.instance.PlayerHp + 10);
            }
            
            other.gameObject.SetActive(false); //hide potion once collected
            GameManager.instance.PlaySfx(transform.position, potionCollectSfx);
        }
    }
}
