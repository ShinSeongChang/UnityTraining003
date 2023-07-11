using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //      스태틱으로 선언
    public static GameManager Instance = default;   // 싱글턴을 할당할 전역 변수

    public bool isGameover = false;     //  게임오버 상태를 판단할 변수
    public Text scoreText = default;    //  점수를 출력할 텍스트
    public GameObject gameoverUi = default; //  게임 오버시 화면에 표시할 게임오버 Ui

    private int score = 0;      //  점수를 저장할 스코어

    public void Awake()
    {
        if (Instance == null)   //  인스턴스가 비어있다면 
        {
            Instance = this;    //  자기 자신을 할당
        }
        else    //  인스턴스가 이미 할당되어 있는 상태라면 
        {
            Debug.LogWarning("씬에 두 개 이상의 게임매니저가 존재합니다!");
            Destroy(gameObject);    //  자신의 오브젝트는 파괴를 한다.
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        //  게임오버 상태이면서 마우스 왼쪽버튼을 누른다면
        if(isGameover && Input.GetMouseButtonDown(0))
        {
            //  현재 씬 재시작
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
     
    // 점수를 증가시킬 메서드
    public void AddScore(int newScore)
    {
        //  게임오버 상태가 트루가 아니라면 ( 필드 선언시 false로 초기화 해줬음 )
        if(!isGameover)
        {
            //  스코어에는 새 점수를 추가해준다.
            score += newScore;

            //  스코어 텍스트에는 스코어 점수를 출력한다
            scoreText.text = "Score : " + score;    
        }
    }

    // 플레이어가 죽을때 호출할 메서드
    public void OnPlayerDead()
    {
        // 게임오버상태를 트루로 바꿔준다.
        isGameover = true;

        // 감추어 두었던 ( false 상태였던 ) 게임오버 Ui를 비춰준다. ( true )
        gameoverUi.SetActive(true);
    }

}
