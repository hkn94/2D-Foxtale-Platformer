using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    public Transform leftPoint, rightPoint;

    private bool movingRight;

    private Rigidbody2D rb;
    private Animator animator;

    public SpriteRenderer sR;

    public float moveTime, waitTime;
    private float moveCount, waitCount;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;

        moveCount = moveTime;
    }

    void Update()
    {

        if (moveCount > 0)
        {
            EnemyIsMoving();
        }
        else if (waitCount > 0)
        {
            EnemyIdle();
        }

    }

    private void EnemyIsMoving()
    {
        moveCount -= Time.deltaTime;

        if (movingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            sR.flipX = true;

            if (transform.position.x > rightPoint.position.x)
            {
                movingRight = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

            sR.flipX = false;

            if (transform.position.x < leftPoint.position.x)
            {
                movingRight = true;
            }
        }


        if (moveCount <= 0)
        {
            waitCount = Random.Range(waitTime * 0.75f, waitTime * 2.25f);
        }

        animator.SetBool("isMoving", true);

    }

    private void EnemyIdle()
    {
        waitCount -= Time.deltaTime;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        if (waitCount <= 0)
        {
            moveCount = Random.Range(moveTime * 0.75f, moveTime * 2.25f);
        }

        animator.SetBool("isMoving", false);
    }
}
