using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterCtrl : MonoBehaviour
{
    public enum MonsterState {idle, trace, attack, die };
    public MonsterState monsterState = MonsterState.idle;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;

    public float traceDist = 3f;
    public float attackDist = 1f;
    
    private bool isDie = false;
    public int hp = 100;
    private int initHp;

    public GameObject item;
    
    //public GameObject monsterHpShow;
    //public Image hpImage;
    

    private void Awake()
    {
        monsterTr = this.gameObject.GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponent<Animator>();

        //!구현중: hp
        //give monster the hp bar
        //monsterHpShow = GameObject.Find("MonsterHpBar");
        //hpImage = GameObject.Find("MonsterHpBarFill").GetComponentInChildren<Image>();

    }

    private void OnEnable() 
    {
        //랜덤한 hp로 난이도 조정
        hp = Random.Range(50, 100);
        //상태 확인
        StartCoroutine(this.CheckMonsterState());
        //상태 따라 코루틴
        StartCoroutine(this.MonsterAction());
    }
    IEnumerator CheckMonsterState()
    {
        
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);
            
            float dist = Vector3.Distance(playerTr.position, monsterTr.position);
            //Debug.Log("dist" + playerTr.position); //check is relocated

            if (dist <= attackDist)
            {
                monsterState = MonsterState.attack;
            }
            else if (dist <= traceDist)
            {
                monsterState = MonsterState.trace;
            }
            else
            {
                monsterState = MonsterState.idle;
            }

            //!구현중: hp
            //hpImage.transform.position = Camera.main.WorldToScreenPoint(monsterTr.position + new Vector3(0, 1, 0));
            //hpImage.fillAmount = (float)hp / (float)initHp; //show hp

        }
    }
    IEnumerator MonsterAction()
    {
        while(!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    nvAgent.isStopped = true;
                    animator.SetBool("IsTrace", false);
                    break;

                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false;
                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsTrace", true);
                    break;

                case MonsterState.attack:
                    nvAgent.isStopped = true;
                    animator.SetBool("IsAttack", true);
                    break;
            
            }
            yield return null;

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
        gameObject.tag = "Untagged";
        StopAllCoroutines();
        isDie = true;
        monsterState = MonsterState.idle;
        nvAgent.isStopped = true;
        animator.SetTrigger("IsDie");

        //추가된 콜라이더 비활성화
        //사망 이후 혈흔 & 플레이어 공격 방지 
        gameObject.GetComponentInChildren<CapsuleCollider>().enabled =false;
        foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = false;
        }

        //몬스터 오브젝트 풀로 환원
        StartCoroutine(this.PushObjectPool());


        //아이템 드랍
        GameObject dropItem = GameObject.Instantiate(item);
        dropItem.transform.position = this.transform.position;

    }

    IEnumerator PushObjectPool()
    {
        yield return new WaitForSeconds(3.0f);

        isDie = false;
        hp = Random.Range(50, 100);
        initHp = hp;
        gameObject.tag = "MONSTER";
        monsterState = MonsterState.idle;
        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;

        foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = true;
        }

        gameObject.SetActive(false);
    }
}
