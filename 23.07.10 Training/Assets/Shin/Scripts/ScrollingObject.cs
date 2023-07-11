using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 게임매니저상에서 게임오버상태가 아니라면.
        // 게임오버상태가 된다면 배경과 지형의 이동들은 그만두게 된다.
        if(!GameManager.Instance.isGameover)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
