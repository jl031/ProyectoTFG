using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 1f;
    public ContactFilter2D contactFilter;
    public float collisionOffset = 0.1f;
    public SwordAttack swordAttack;
    Vector2 movementDirection;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    List<RaycastHit2D> collisionList = new List<RaycastHit2D>();
    private bool canMove = true;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {

        if (!canMove) {
            return;
        }

        if (movementDirection == Vector2.zero) {
            animator.SetBool("IsMoving", false);
            return;
        }

        bool success = TryMove(movementDirection);

        if (!success) {
            success = TryMove(new Vector2( movementDirection.x, 0));

        }    

        if (!success) {
            success = TryMove(new Vector2(0, movementDirection.y));
        }

        animator.SetBool("IsMoving", success);

        if (!success) {
            return;
        }

        float absY = Mathf.Abs(movementDirection.y);
        float absX = Mathf.Abs(movementDirection.x);

        if (movementDirection.x < 0) {
            spriteRenderer.flipX = true;
            swordAttack.attackDirection = SwordAttack.AttackDirection.left;
        } else {
            spriteRenderer.flipX = false;
            swordAttack.attackDirection = SwordAttack.AttackDirection.right;
        }

        if (absY > absX) {
            spriteRenderer.flipX = false;
            animator.SetBool("MovingVertically", true);
            animator.SetInteger("VerticalDirection", Math.Sign(movementDirection.y));
            
            if (movementDirection.y > 0) {
                swordAttack.attackDirection = SwordAttack.AttackDirection.up;
            } else {
                swordAttack.attackDirection = SwordAttack.AttackDirection.down;
            }

        } else {
            animator.SetBool("MovingVertically", false);
        }

    }

    private bool TryMove(Vector2 direction) {
        if (direction == Vector2.zero) {
            return false;
        }
        int hitCount = rb.Cast(
            direction,
            contactFilter,
            collisionList,
            walkSpeed * Time.fixedDeltaTime + collisionOffset
        );

        if (hitCount == 0) {
            rb.MovePosition(rb.position + Time.fixedDeltaTime * walkSpeed * direction);
            return true;
        }

        return false;
    }

    void OnMove(InputValue directionInput){
        movementDirection = directionInput.Get<Vector2>();
    }

    void OnFire(){
        animator.SetTrigger("SwordAttack");
        Attack();
    }

    private void Attack(){
        swordAttack.Attack();
    }

    private void LockMovement(){
        canMove = false;
    }
    private void UnlockMovement(){
        canMove = true;
        swordAttack.StopAttack();
    }

}
