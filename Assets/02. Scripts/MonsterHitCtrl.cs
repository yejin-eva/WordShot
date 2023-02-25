using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterHitCtrl : MonoBehaviour
{
    GameObject player;
    UIManager uiManager;
    
    
    PlayerCtrl playerCtrl;

    private float dist;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCtrl = GameObject.FindGameObjectWithTag("PlayerObject").GetComponent<PlayerCtrl>();
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        dist = Vector3.Distance(this.transform.position, player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(this.transform.position, player.transform.position);
        timer += Time.deltaTime;
        //Debug.Log(dist);

        //�ݶ��̴� ��� ��� �ּ�ȭ�Ͽ� �Ÿ����
        if (dist < 2.0f)
        {
            //1�ʿ� �ѹ��� hit
            if (timer >= 1f)
            {
                //Debug.Log(timer + "Hit!");
                timer = 0f;
                //subtract player hp
                playerCtrl.hp -= Random.Range(5, 10);

                //Debug.Log("HP = " + playerCtrl.hp);


            }

            
        }
    }
}
