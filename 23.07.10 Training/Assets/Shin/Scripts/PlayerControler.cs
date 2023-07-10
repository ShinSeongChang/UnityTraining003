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

        // isDead ���¶�� ���� ����
        if(isDead)
        {
            return;
        }

        // ���콺 ���ʹ�ư Ű�� ������ ���� ����ī��Ʈ�� 2 �̸��϶� ( ����Ƚ�� 2ȸ�� ���� )
        else if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            // ����ī��Ʈ ����
            jumpCount++;
            
            playerRigid.velocity = Vector2.zero;    // �����ϴ� ���� �÷��̾��� �ӵ��� 0
            playerRigid.AddForce(new Vector2 (0, jumpForce));   // 2D �ϱ� Vector2 ���(x,y��), y�࿡ ����������ŭ ���� �༭ ��������

            playerAudio.Play();     // ����� �ҽ� ���

        }

        // ���콺 ���ʹ�ư Ű�� ������ ���� ���� �÷��̾ ������ ���¶�� == y ���� 0 �ʰ����
        else if(Input.GetMouseButtonUp(0) && playerRigid.velocity.y > 0)
        {
            playerRigid.velocity = playerRigid.velocity * 0.5f;
        }

        // �ִϸ�����.������Ÿ��(���⿡���� bool����) ("Ű��" == "Grounded" �Ķ���� , Ű���� �� ���� == isgronded �� �Լ��� ���� ���°� ���� ����)
        animator.SetBool("Grounded", isGrounded);
    }

    private void Die()
    {
        // �ִϸ�����.������ Ÿ�� (���⿡���� Trigger ����) ( "Ű��" == "Die" �Ķ���� )
        animator.SetTrigger("Die");
        
        playerAudio.clip = deathClip;   //  �÷��̾� ����� �ҽ� Ŭ���� ��ü ( deathŬ������ )
        playerAudio.Play();             //  ��ü�� ������ҽ��� �ٽ� �÷��� ( �׾����� ������� ��ȯ )

        playerRigid.velocity = Vector2.zero;    // �÷��̾�� ������ ���ڸ����� �ӵ��� �ҰԵȴ�.

        isDead = true;                  // isDead�� true�� ����� �� �Լ����� �ۿ��ϰ� �Ѵ�.

        GameManager.Instance.OnPlayerDead();

    }

    private void OnTriggerEnter2D(Collider2D other) //  ������ Ʈ���ſ� ��� ����
    {
        
        if (other.tag == "Dead" && !isDead)     //  �ش� Ʈ���� ��ü�� �±װ� "Dead" �̸鼭 ���� ���°� !isDead��� ( true ��� ==> �ʱ갪�� false )
        {
            Die();      //  Die �Լ��� �����Ѵ�. ( �÷��̾�� Die �ִϸ��̼��� �����ָ� ���ڸ����� �ӵ��� �Ұ� �ȴ� )
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)   // ������ �ݶ��̴��� ��� ���� ( ���� ��� �ִ� ���� )
    {        
        if (collision.contacts[0].normal.y > 0.7f)      //  �浹 ������ ���� Ȥ�� �������̶�� ( ������ �ε��� ��Ȳ ���� )
        {                                               //  y���� 1�� �������� �ϸ��� ���, 1�̶�� ���� ����, -1 �̶�� �Ʒ�����
            isGrounded = true;      //  ���̶�� ���� ( �� �ִϸ��̼� ���� )
            jumpCount = 0;          //  ���� ī��Ʈ�� �ʱ�ȭ �����ش�.
        }
    }

    public void OnCollisionExit2D(Collision2D collision)    // ������ �ݶ��̴����� ����� ���� ( ������ ����� ���� )
    {
        isGrounded = false;     // ���� �ƴ϶�� ���� ( ���� �ִϸ��̼� ����, ���� ī��Ʈ �� )
    }
}
