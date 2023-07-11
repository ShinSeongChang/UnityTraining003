using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public AudioClip deathClip;
    public float jumpForce = 700f;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D playerRigid;
    private Animator animator;
    private AudioSource playerAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        // isDead 상태라면 게임 종료
        if(isDead)
        {
            return;
        }

        // 마우스 왼쪽버튼 키를 누르는 순간 점프카운트가 2 미만일때 ( 점프횟수 2회로 제한 )
        else if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            // 점프카운트 증가
            jumpCount++;

            // 점프하는 순간 플레이어의 속도는 0
            playerRigid.velocity = Vector2.zero;

            // 2D 니까 Vector2 사용(x,y축), y축에 점프포스만큼 힘을 줘서 점프구현
            playerRigid.AddForce(new Vector2 (0, jumpForce));

            // 오디오 소스 재생
            playerAudio.Play();     

        }

        // 마우스 왼쪽버튼 키를 눌렀다 떼는 순간 플레이어가 점프한 상태라면 == y 값이 0 초과라면
        else if(Input.GetMouseButtonUp(0) && playerRigid.velocity.y > 0)
        {
            playerRigid.velocity = playerRigid.velocity * 0.5f;
        }

        // 애니메이터.데이터타입(여기에서는 bool형태) ("키값" == "Grounded" 파라미터 , 키값에 들어갈 변수 == isgronded 는 함수에 따라 상태가 변할 예정)
        animator.SetBool("Grounded", isGrounded);
    }

    private void Die()
    {
        // 애니메이터.데이터 타입 (여기에서는 Trigger 형태) ( "키값" == "Die" 파라미터 )
        animator.SetTrigger("Die");

        //  플레이어 오디오 소스 클립을 교체 ( death클립으로 )
        playerAudio.clip = deathClip;

        //  교체한 오디오소스를 다시 플레이 ( 죽었을때 오디오로 전환 )
        playerAudio.Play();

        // 플레이어는 죽으면 제자리에서 속도를 잃게된다.
        playerRigid.velocity = Vector2.zero;

        // isDead를 true로 만들어 각 함수에서 작용하게 한다.
        isDead = true;                  

        // 게임매니저의 플레이어 죽음 상태 메서드 호출
        GameManager.Instance.OnPlayerDead();

    }


    //  무언가의 트리거에 닿는 순간
    private void OnTriggerEnter2D(Collider2D other) 
    {
        //  해당 트리거 물체의 태그가 "Dead" 이면서 현재 상태가 !isDead라면 ( true 라면 ==> 초깃값은 false )
        if (other.tag == "Dead" && !isDead)     
        {
            //  Die 함수를 실행한다. ( 플레이어는 Die 애니메이션을 보여주며 제자리에서 속도를 잃게 된다 )
            Die();      
        }

    }

    // 무언가의 콜라이더에 닿는 순간 ( 땅에 닿아 있는 순간 )
    public void OnCollisionEnter2D(Collision2D collision)   
    {
        //  충돌 방향이 왼쪽 혹은 오른쪽이라면 ( 절벽에 부딪힌 상황 제외 )
        //  y값이 1에 가까울수록 완만한 경사, 1이라면 위쪽 방향, -1 이라면 아랫방향
        if (collision.contacts[0].normal.y > 0.7f)      
        {
            //  땅이라고 판정 ( 런 애니메이션 구현 )
            isGrounded = true;

            //  점프 카운트를 초기화 시켜준다.
            jumpCount = 0;          
        }
    }

    // 무언가의 콜라이더에서 벗어나는 순간 ( 땅에서 벗어나는 순간 )
    public void OnCollisionExit2D(Collision2D collision)    
    {
        // 땅이 아니라고 판정 ( 점프 애니메이션 구현, 점프 카운트 등 )
        isGrounded = false;     
    }
}
