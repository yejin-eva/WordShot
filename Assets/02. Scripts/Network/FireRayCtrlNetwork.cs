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

        //좌표 로컬기준으로 변경
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

        //슈팅 버튼 눌렀을 때 
        if (Input.GetKeyDown(KeyCode.V))
        {
            //라인렌더러의 첫 번째 점 위치 기존 광선과 겹치지 않도록 조금 높게 설정
            line.SetPosition(0, tr.InverseTransformPoint(ray.origin));

            //광선이 맞은 물체 라인렌더러의 끝점으로 설정
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //inversetransformpoint로 월드->로컬좌표로 변환
                line.SetPosition(1, tr.InverseTransformPoint(hit.point));
            }
            else
            {
                line.SetPosition(1, tr.InverseTransformPoint(ray.GetPoint(100.0f)));
            }
            //광선 표시
            StartCoroutine(this.ShowLaserBeam());
        }
    }
    IEnumerator ShowLaserBeam()
    {
        line.enabled = true;
        //일정 시간 이후 라인 제거 
        yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));
        line.enabled = false;
    }
}
