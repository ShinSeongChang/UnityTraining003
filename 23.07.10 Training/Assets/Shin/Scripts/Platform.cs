using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // 함정 오브젝트들 => 배열로 선언한다.
    public GameObject[] obstacles;

    // 플레이어가 랜덤발판을 밟았는지 판정할 bool stepped 변수.
    private bool stepped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 컴포넌트가 활성화 될때마다 실행되는 메서드 OnEnable()
    private void OnEnable()
    {
        // 발판을 밟지않은 상태로 초기화.
        stepped = false;

        // 함정의 갯수만큼 반복문을 돌린다 => 해당 프로젝트에서는 함정의 갯수는 3개 ( Left, Mid, Right ).
        for(int i = 0; i<obstacles.Length; i++)
        {
            // 각 함정은 1/3 확률로 소환된다 ( 0, 1, 2 중 0의 값이 나왔을시에만 ).
            if(Random.Range(0,3) == 0)
            {
                // 0값이 나온 함정은 오브젝트 활성화.
                obstacles[i].SetActive(true);
            }
            else
            {
                // 그 외의 값이 나온 함정은 오브젝트를 비활성화.
                obstacles[i].SetActive(false);
            }
        }
    }

    // 무언가가 발판에 닿는다면
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어가 발판에 닿았으면서 발판에 닿지 않았던 상태라면.
        if (collision.collider.tag == "Player" && !stepped)
        {
            // 발판에 닿은 상태로 초기화.
            stepped=true;

            // 게임매니저의 AddScore() 메서드 호출과 1값 추가.
            GameManager.Instance.AddScore(1);
        }     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
