using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FireCtrlNetwork : NetworkBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private AudioClip fireSfx; //�Ѿ� �߻� ����
    private AudioSource source = null; //������ҽ� ������Ʈ ���� ����
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
        //ũ�� ����
        float scale = Random.Range(1.0f, 1.5f);
        muzzleFlash.transform.localScale = Vector3.one * scale;
        //ȸ�� ����
        Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        muzzleFlash.transform.localRotation = rot;
        muzzleFlash.enabled = true;
        //���ӽð� ����
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

        //�����̽��� ������ ray �߻�
        if (Input.GetKeyDown(KeyCode.V))
        {
            Fire(); //�ѼҸ� + �� �Һ� �����ֱ�
            RaycastHit hit;

            //�Ѿ� �ݰ� = 10f
            if (Physics.Raycast(firePos.position, firePos.forward, out hit, 10.0f))
            {
                if (hit.collider.tag == "MONSTER")
                {
                    object[] _params = new object[2];
                    _params[0] = hit.point; //ray���� ��ġ;
                    _params[1] = Random.Range(10, 20); //���Ϳ� ���� ���� ��������

                    hit.collider.gameObject.SendMessage("OnDamage", _params, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
