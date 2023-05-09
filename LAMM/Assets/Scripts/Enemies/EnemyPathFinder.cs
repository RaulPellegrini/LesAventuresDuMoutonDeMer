using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinder : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private bool canMove = true;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;
    private Animator myAnimator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (knockback.GettingKnockedBack) { return; }
        if (!canMove) { return; }

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
        
        if (moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        myAnimator.SetBool("Walking", true);
        moveDir = targetPosition;
    }

    public void StopMoving()
    {
        myAnimator.SetBool("Walking", false);
        
        moveDir = Vector3.zero;
    }
    public void StopMoveStart()
    {
        myAnimator.SetBool("Walking", false);

        canMove = false;
        moveDir = Vector3.zero;
    }

    public void StopMoveEnd()
    {
        canMove = true;
    }

}
