using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed;

    public Vector2 inputVec;
    public Scanner scanner;
    public Hand[] hands;

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

    void Start()
    {
        
    }

    private void FixedUpdate()
    {        
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        //À§Ä¡
        rb.MovePosition(rb.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            sprite.flipX = inputVec.x < 0;
        }
    }
}
