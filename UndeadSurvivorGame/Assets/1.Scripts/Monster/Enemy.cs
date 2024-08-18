using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public RuntimeAnimatorController[] animatorController;
    public float health;
    public float maxHealth;

    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rb;
    Collider2D coll;
    Animator animator;
    SpriteRenderer spriteRenderer;
    WaitForFixedUpdate wait;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        if (!Gamemanager.instance.isLive)
        {
            return;
        }
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) // GetCurrentAnimatorStateInfo() 현재 상태 정보를 가져오는 함수 
            return;
        
        
        Vector2 dirVec = target.position - rb.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + nextVec);

        rb.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!Gamemanager.instance.isLive)
        {
            return;
        }
        if (!isLive)
            return;

        spriteRenderer.flipX = target.position.x < rb.position.x;
    }

    //스크립트가 활성화 될 때 호출되는 이벤트 함수 
    private void OnEnable()
    {
        target = Gamemanager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rb.simulated = true;
        spriteRenderer.sortingOrder = 2;
        animator.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animatorController[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if(health > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rb.simulated = false;
            spriteRenderer.sortingOrder = 1;
            animator.SetBool("Dead" , true);
            Gamemanager.instance.kill++;
            Gamemanager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; //다음 하나의 물리 프레임 딜레이
        Vector3 playerPos = Gamemanager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rb.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
