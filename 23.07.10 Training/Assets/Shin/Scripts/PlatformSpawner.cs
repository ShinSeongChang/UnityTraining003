using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    // �÷��� �������� �Ҵ��� ���ӿ�����Ʈ ����.
    public GameObject platformPrefabs;

    // �÷����� ���� ����.
    public int count = 3;

    // �÷����� �����ֱ⸦ ���� �� ���� Spawn = Min ~ Max .
    public float timeBetSpawnMin = 1.25f;
    public float timeBetSpawnMax = 2.25f;
    private float timeBetSpawn;

    // �÷����� ��ǥ ��ġ ���ѽ�ų �� ���� 
    public float yMin = -3.5f;
    public float yMax = 1.5f;
    private float xPos = 20f;

    // �̸� �����صξ��� ���ǵ�
    private GameObject[] platforms;

    // ���� ����� ������ �ε��� ��.
    private int currentIndex = 0;

    // �ʹݿ� ����� ������ ���ܵ� ��ġ.
    private Vector2 poolPosition = new Vector2(0, -25);

    // ������ ��ġ ���� �ð�.
    private float lastSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        // ���۽� ������ ������ ���� => �ش� ������Ʈ������ 3���� �Ҵ�.
        platforms = new GameObject[count];

        // �÷����� �������� ��ŭ (count) �������� �ν��Ͻ�ȭ �����ش� .
        for(int i = 0; i < count; i++)
        {
            platforms[i] = Instantiate(platformPrefabs, poolPosition, Quaternion.identity);
        }

        // ������ ��ġ �ð������� �����ֱ� 0���� �ʱ�ȭ.
        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // ���ӸŴ����� ���ӿ��� ���°� �ȴٸ�.
        if(GameManager.Instance.isGameover)
        {
            // ���� �÷��� ������ �����.
            return;
        }
        
        // ����ð���   ������ ��ġ ��������  timeBetSpawn ��ŭ�� �ð��� �귶�ٸ�.
        if(Time.time >= lastSpawnTime + timeBetSpawn)
        {
            // ������ ��ġ ������ ���� �ð��� ����.
            lastSpawnTime = Time.time;

            // �����ð��� ���� �ֱ⸦ ���� ( Min ~ Max ).
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            // ������ ������ y���� ������ Min ~ Max ��
            float yPos = Random.Range(yMin, yMax);

            // ������ �÷����� ������Ʈ�� ��Ȱ��ȭ ��Ű�鼭 �ٷ� Ȱ��ȭ => Platform ��ũ��Ʈ�� OnEnable() �޼ҵ尡 �����.
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            // ������ �÷����� ��ġ�� ��ǥ�� x,y .
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);

            // ���� ������ �÷����� ������Ű�� ���� �ε����� ����.
            currentIndex++;
        }

        // �ε������� ī��Ʈ�� �̻��� �Ǹ� 0���� �ʱ�ȭ���� 0������ �÷������� �����ǰ� �Ѵ�.
        if(currentIndex >= count)
        {
            currentIndex = 0;
        }
    }
}
