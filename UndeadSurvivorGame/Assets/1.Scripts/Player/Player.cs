using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;

    public Vector2 inputVec;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;

    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();  
        hands = GetComponentsInChildren<Hand>(true);
    }
    private void OnEnable()
    {
        speed *= Character.Speed;
        animator.runtimeAnimatorController = animCon[Gamemanager.instance.playerId];
    }

    private void Update()
    {
        if(!Gamemanager.instance.isLive)       
            return;
        
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (!Gamemanager.instance.isLive)
        {
            return;
        }
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        //À§Ä¡
        rb.MovePosition(rb.position + nextVec);
    }


    private void LateUpdate()
    {
        animator.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            sprite.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!Gamemanager.instance.isLive)
        {
            return;
        }
        Gamemanager.instance.health -= Time.deltaTime * 10;

        if(Gamemanager.instance.health < 0)
        {
            for(int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            animator.SetTrigger("Dead");
            Gamemanager.instance.GameOver();
        }
    }

}
