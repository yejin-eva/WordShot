using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterHitCtrl : MonoBehaviour
{
    private GameObject player;
    private UIManager uiManager;
    
    
    PlayerCtrl playerCtrl;

    private float distance;
    private float timer;

    private int minimumDamage = 5;
    private int maximumDamage = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCtrl = GameObject.FindGameObjectWithTag("PlayerObject").GetComponent<PlayerCtrl>();
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        distance = Vector3.Distance(this.transform.position, player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        timer += Time.deltaTime;
        //Debug.Log(dist);

        //콜라이더 상시 사용 최소화하여 거리계산
        if (distance < 2.0f)
        {
            //1초에 한번씩 hit
            if (timer >= 1f)
            {
                //Debug.Log(timer + "Hit!");
                timer = 0f;
                //subtract player hp
                playerCtrl.hp -= Random.Range(minimumDamage, maximumDamage);

                //Debug.Log("HP = " + playerCtrl.hp);


            }

            
        }
    }
}
