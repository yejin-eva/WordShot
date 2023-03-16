using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEaterCtrl : MonoBehaviour
{
    private Animator animator;
    public int hp;
    void Start()
    {
        hp = 300;
        animator = this.gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        
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

    }
}
