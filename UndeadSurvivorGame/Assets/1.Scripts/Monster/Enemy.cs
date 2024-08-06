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
    Animator animator;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(!isLive)
            return;
        
        
        Vector2 dirVec = target.position - rb.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + nextVec);

        rb.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive)
            return;

        spriteRenderer.flipX = target.position.x < rb.position.x;
    }

    //스크립트가 활성화 될 때 호출되는 이벤트 함수 
    private void OnEnable()
    {
        target = Gamemanager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
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
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;

        if(health > 0)
        {

        }
        else
        {
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
