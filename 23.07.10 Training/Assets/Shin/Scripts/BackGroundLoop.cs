using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    // 박스의 사이즈를 담을 변수 선언
    private float width = default;
    // Start is called before the first frame update

    private void Awake()    // Awake 함수는 start 보다 한프레임 먼저 실행된다 ( 추후에 Unity Life cycle 참조 )
    {
        //  BoxCollider2D 컴포넌트를 참조한다
        BoxCollider2D backgroindCollider = GetComponent<BoxCollider2D>();
        width = backgroindCollider.size.x;  //  박스콜라이더의 사이즈를 담는다. 
    }
    void Start()
    {

    }

    public void Reposition()    //  배경을 다시 뒷자리로 포지션 시킬 함수
    {        
        Vector2 offset = new Vector2(width * 2f, 0);    // 이동 시킬 위치는 Vector2 의 ( x축 == 박스의 사이즈 * 2 칸으로 한다 )
        transform.position = (Vector2)transform.position + offset;      //  transform.position 에 offset을 대입한다.
    }

    // Update is called once per frame
    void Update()
    {
        // update를 통해 게임이 실행되는 동안 박스의 위치를 계속 탐색한다
        if (transform.position.x <= -width)     // 이후 박스의 사이즈 치수만큼 -로 향한다면 (자신의 크기만큼 왼쪽으로 갔다면 )
        {
            Reposition();   //  리포지션 해준다.
        }
    }
}
