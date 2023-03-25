using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterCtrl : MonoBehaviour
{
    private Animator animator;
    private GameObject player;
    private float timer; //공격 타이밍
    private float dist;
    private float moveSpeed;

    public int hp;
    public GameObject item; //drop item for boss monster

    PlayerCtrl playerCtrl;
    
    void Start()
    {
        hp = 300;
        moveSpeed = 1f;
        animator = this.gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCtrl = GameObject.FindGameObjectWithTag("PlayerObject").GetComponent<PlayerCtrl>();
    }

    
    void Update()
    {
        dist = Vector3.Distance(this.transform.position, player.transform.position);
        timer += Time.deltaTime;
        //set animator
        animator.SetBool("IsTrace", false);

        //follow player
        if (dist < 10.0f)
        {
            //change distance

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
            transform.LookAt(player.transform);
            //change to walking animation
            animator.SetBool("IsTrace", true);
            

            //if player is close attack player
            if (dist < 3.0f)
            {
                //attacks every 0.5 seconds
                if (timer >= 0.5f)
                {
                    //set animator
                    animator.SetBool("IsTrace", false);
                    animator.SetTrigger("IsAttack");

                    //deduct hp from player
                    Debug.Log("Attacked");
                    playerCtrl.hp -= Random.Range(10, 30);
                    timer = 0;
                }
            }
        }
        
        
    }

    void OnDamage(object[] _params) //FireCtrl에서 콜라이더 통해 사용
    {
        //Debug.Log(string.Format("Hit ray {0} : {1}", _params[0], _params[1]));

        hp -= (int)_params[1];

        //*구현중!! show hp
        //monsterHpShow.SetActive(true);


        if (hp <= 0)
        {
            MonsterDie();
        }
        animator.SetTrigger("IsHit");
    }
    void MonsterDie()
    {
        Debug.Log("Boss Monster Dead");
        //play dead animation
        animator.SetTrigger("IsDie");

        //drop item
        GameObject dropItem = GameObject.Instantiate(item);
        dropItem.transform.position = this.transform.position;

        //killed monster
        StartCoroutine(this.MonsterKilled());
       
    }

    //kill monster after 3 seconds
    IEnumerator MonsterKilled()
    {
        yield return new WaitForSeconds(3.0f);

        //destroy this game object
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
