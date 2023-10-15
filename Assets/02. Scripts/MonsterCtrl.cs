using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterCtrl : MonoBehaviour
{
    [SerializeField] private enum MonsterState {idle, trace, attack, die };
    [SerializeField] private MonsterState monsterState = MonsterState.idle;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;

    [SerializeField] private float traceDist = 3f;
    [SerializeField] private float attackDist = 1f;
    
    private bool isDie = false;
    public int hp = 100;
    public int initialHp = 100;
    [SerializeField] private int minimumHp = 50;
    [SerializeField] private int maximumHp = 100;
    private int initHp;

    [SerializeField] private GameObject item;
    [SerializeField] private GameObject HpBarGameObject;

    public event EventHandler OnMonsterHit;

    private enum MonsterAnimation
    {
        IsTrace,
        IsAttack,
        IsHit,
        IsDie,
    }

    private void Awake()
    {
        monsterTr = this.gameObject.GetComponent<Transform>();
        //playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponent<Animator>();

    }
   

    private void OnEnable() 
    {
        //Enable�ÿ� �÷��̾� ã�� 
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //������ hp�� ���̵� ����
        hp = UnityEngine.Random.Range(minimumHp, maximumHp);
        initialHp = hp;
        //hp not shown until hit
        HpBarGameObject.SetActive(false);
        //���� Ȯ��
        StartCoroutine(this.CheckMonsterState());
        //���� ���� �ڷ�ƾ
        StartCoroutine(this.MonsterAction());
    }
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);
            
            float dist = Vector3.Distance(playerTr.position, monsterTr.position);
            

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
                    animator.SetBool(MonsterAnimation.IsTrace.ToString(), false);
                    break;

                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false;
                    animator.SetBool(MonsterAnimation.IsAttack.ToString(), false);
                    animator.SetBool(MonsterAnimation.IsTrace.ToString(), true);
                    break;

                case MonsterState.attack:
                    nvAgent.isStopped = true;
                    animator.SetBool(MonsterAnimation.IsAttack.ToString(), true);
                    break;
            
            }
            yield return null;

        }
    }

    void OnDamage(object[] _params) //FireCtrl���� �ݶ��̴� ���� ���
    {
        //Debug.Log(string.Format("Hit ray {0} : {1}", _params[0], _params[1]));
        HpBarGameObject.SetActive(true);

        OnMonsterHit?.Invoke(this, EventArgs.Empty);
        hp -= (int)_params[1];
        
        if (hp <= 0)
        {
            MonsterDie();
        }
        animator.SetTrigger(MonsterAnimation.IsHit.ToString());



    }

    void MonsterDie()
    {
        gameObject.tag = "Untagged";
        StopAllCoroutines();
        isDie = true;
        monsterState = MonsterState.idle;
        nvAgent.isStopped = true;
        animator.SetTrigger(MonsterAnimation.IsDie.ToString());

        //�߰��� �ݶ��̴� ��Ȱ��ȭ
        //��� ���� �÷��̾� ���� ���� 
        gameObject.GetComponentInChildren<CapsuleCollider>().enabled =false;
        foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = false;
        }

        //���� ������Ʈ Ǯ�� ȯ��
        StartCoroutine(this.PushObjectPool());


        //������ ���
        GameObject dropItem = GameObject.Instantiate(item);
        dropItem.transform.position = this.transform.position;

    }

    IEnumerator PushObjectPool()
    {
        yield return new WaitForSeconds(3.0f);

        isDie = false;
        hp = UnityEngine.Random.Range(minimumHp, maximumHp);
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
