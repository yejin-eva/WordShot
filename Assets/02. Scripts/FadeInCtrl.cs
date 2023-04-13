using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInCtrl : MonoBehaviour
{
    public float duration = 1f;
    public GameObject effectPrefab;
    public Transform effectSpawnPoint;

    public bool isFadeIn = true;
    public bool isEffect = true;
    

    private void OnEnable()
    {
        effectSpawnPoint = transform; //자신의 위치에서 이펙트 실행
        if (isFadeIn) StartCoroutine(FadeIn());
        if (isEffect) StartCoroutine(PlayEffect());
    }

    IEnumerator FadeIn()
    {
        Renderer renderer = GetComponent<Renderer>();
        if(renderer == null)
        {
            Debug.LogWarning("Renderer컴포넌트를 찾을 수 없습니다.");
            yield break;
        }
        Color originalColor = renderer.material.color;
        float elapsedTime = 0f;
        while (elapsedTime < duration) 
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            Color color = originalColor;
            color.a = t;
            renderer.material.color = color;
            yield return null;
        }
        Color finalColor = originalColor;
        finalColor.a = 1f;
        renderer.material.color = finalColor;
    }
    IEnumerator PlayEffect()
    {
        if (effectPrefab != null && effectSpawnPoint != null)
        {
            yield return new WaitForSeconds(duration * 0.5f); //절반 시간 후에 이펙트 재생
            GameObject effectInstance = Instantiate(effectPrefab, effectSpawnPoint.position, Quaternion.identity);
            Destroy(effectInstance, 3f); //이펙트 3초 재생 후 파괴
        }
    }
}
