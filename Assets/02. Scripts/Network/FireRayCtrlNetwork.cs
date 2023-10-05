using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FireRayCtrlNetwork : NetworkBehaviour
{
    private Transform tr;
    private LineRenderer line;
    private RaycastHit hit;

    void Start()
    {
        tr = GetComponent<Transform>();
        line = GetComponent<LineRenderer>();

        //��ǥ ���ñ������� ����
        line.useWorldSpace = false;
        line.enabled = false;
        line.startWidth = 0.3f;
        line.endWidth = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        Ray ray = new Ray(tr.position + (Vector3.up * 0.02f), tr.forward);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);

        //���� ��ư ������ �� 
        if (Input.GetKeyDown(KeyCode.V))
        {
            //���η������� ù ��° �� ��ġ ���� ������ ��ġ�� �ʵ��� ���� ���� ����
            line.SetPosition(0, tr.InverseTransformPoint(ray.origin));

            //������ ���� ��ü ���η������� �������� ����
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //inversetransformpoint�� ����->������ǥ�� ��ȯ
                line.SetPosition(1, tr.InverseTransformPoint(hit.point));
            }
            else
            {
                line.SetPosition(1, tr.InverseTransformPoint(ray.GetPoint(100.0f)));
            }
            //���� ǥ��
            StartCoroutine(this.ShowLaserBeam());
        }
    }
    IEnumerator ShowLaserBeam()
    {
        line.enabled = true;
        //���� �ð� ���� ���� ���� 
        yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));
        line.enabled = false;
    }
}
