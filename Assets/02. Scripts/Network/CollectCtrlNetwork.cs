using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCtrlNetwork : MonoBehaviour
{
    //UIManager uiManager;
    [SerializeField] private AudioClip itemCollectSfx; //������ ȹ�� ����


    //[SerializeField] private GameObject player;
    private PlayerCtrlNetwork playerCtrl;

    [SerializeField] private AudioClip potionCollectSfx;
    private void Start()
    {
        //uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        playerCtrl = gameObject.GetComponentInParent<PlayerCtrlNetwork>();

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
            PlayerData.instance.SetHp(PlayerData.instance.PlayerHp + 10);
            other.gameObject.SetActive(false); //hide potion once collected
            GameManager.instance.PlaySfx(transform.position, potionCollectSfx);
        }
    }
}