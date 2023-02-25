using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCtrl : MonoBehaviour
{
    UIManager uiManager;
    public AudioClip itemCollectSfx; //æ∆¿Ã≈€ »πµÊ ªÁøÓµÂ
    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ITEM")
        {
            Debug.Log("Item found");
            uiManager.itemAmount += 1;
            Destroy(other.gameObject);
            
            //play collect sound
            GameManager.instance.PlaySfx(transform.position, itemCollectSfx);
        }
    }
}
