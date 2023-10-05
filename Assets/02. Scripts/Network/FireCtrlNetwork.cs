using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FireCtrlNetwork : NetworkBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private AudioClip fireSfx; //총알 발사 사운드
    private AudioSource source = null; //오디오소스 컴포넌트 저장 변수
    [SerializeField] private MeshRenderer muzzleFlash;

    void Start()
    {
        source = GetComponent<AudioSource>();
        muzzleFlash.enabled = false;
    }

    void Fire()
    {
        NetworkGameManager.instance.PlaySfx(firePos.position, fireSfx);
        StartCoroutine(this.ShowMuzzleFlash());
    }
    IEnumerator ShowMuzzleFlash()
    {
        //크기 랜덤
        float scale = Random.Range(1.0f, 1.5f);
        muzzleFlash.transform.localScale = Vector3.one * scale;
        //회전 랜덤
        Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        muzzleFlash.transform.localRotation = rot;
        muzzleFlash.enabled = true;
        //지속시간 랜덤
        yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));
        muzzleFlash.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(!IsOwner)
        {
            return;
        }
        Debug.DrawRay(firePos.position, firePos.forward * 10.0f, Color.red);

        //스페이스바 누르면 ray 발사
        if (Input.GetKeyDown(KeyCode.V))
        {
            Fire(); //총소리 + 총 불빛 보여주기
            RaycastHit hit;

            //총알 반경 = 10f
            if (Physics.Raycast(firePos.position, firePos.forward, out hit, 10.0f))
            {
                if (hit.collider.tag == "MONSTER")
                {
                    object[] _params = new object[2];
                    _params[0] = hit.point; //ray맞은 위치;
                    _params[1] = Random.Range(10, 20); //몬스터에 입힐 랜덤 데미지값

                    hit.collider.gameObject.SendMessage("OnDamage", _params, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
