using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] points;
    public GameObject spawnPoints;
    public GameObject monsterPrefab;
    public List<GameObject> monsterPool = new List<GameObject>();


    public float createTime = 3.0f;
    public int maxMonster = 5;
    public bool isGameOver = false;

    public float sfxVolumn = 1.0f;
    public bool isSfxMute = false;


    public Transform[] potionPoints;
    public GameObject potionSpawnPoints;
    public GameObject potionPrefab;
    public List<GameObject> potionPool = new List<GameObject>();

    public float potionCreateTime = 5.0f;
    public int maxPotion = 5;
    

    //�̱������� ����
    public static GameManager instance = null;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //��������Ʈ ã��
        points = spawnPoints.GetComponentsInChildren<Transform>();

        //���� Ǯ�� ����
        for (int i = 0; i < maxMonster; i++)
        {
            GameObject monster = (GameObject)Instantiate(monsterPrefab);
            monster.name = "Monster_" + i.ToString();
            monster.SetActive(false);
            monsterPool.Add(monster);
        }
        //����Ʈ ã������ ���� ���� ����
        if (points.Length > 0)
        {
            StartCoroutine(this.CreateMonster());
        }



        //���� ����Ʈ ã��
        potionPoints = potionSpawnPoints.GetComponentsInChildren<Transform>();

        for (int i = 0; i < maxPotion; i++)
        {
            GameObject potion = (GameObject)Instantiate(potionPrefab);
            potion.name = "Potion_" + i.ToString();
            potion.SetActive(false);
            potionPool.Add(potion);
        }
        if (potionPoints.Length > 0)
        {
            StartCoroutine(this.CreatePotion());
        }
    }
    IEnumerator CreatePotion()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(potionCreateTime);
            if (isGameOver) yield break;

            foreach (GameObject potion in potionPool)
            {
                if (!potion.activeSelf)
                {
                    int idx = Random.Range(1, potionPoints.Length);
                    potion.transform.position = potionPoints[idx].position;
                    potion.SetActive(true);
                    break;
                }
            }
        }
    }

    IEnumerator CreateMonster()
    {
        while (!isGameOver)
        {
            //������ �ð��� ���� �ѹ���
            yield return new WaitForSeconds(createTime);
            if (isGameOver) yield break;

            foreach (GameObject monster in monsterPool)
            {
                if (!monster.activeSelf)
                {
                    //���� Ǯ���� ���� ������ �ϳ� Ȱ��ȭ �� for������ ����
                    int idx = Random.Range(1, points.Length);
                    monster.transform.position = points[idx].position;
                    monster.SetActive(true);
                    break;
                }
            }
        }
    }

    public void PlaySfx(Vector3 pos, AudioClip sfx)
    {
        if (isSfxMute) return;
        GameObject soundObj = new GameObject("Sfx");
        soundObj.transform.position = pos;

        AudioSource audioSource = soundObj.AddComponent<AudioSource>();
        audioSource.clip = sfx;
        audioSource.minDistance = 10.0f;
        audioSource.maxDistance = 30.0f;

        audioSource.volume = sfxVolumn;
        audioSource.Play();

        Destroy(soundObj, sfx.length);
    }
}
