using System;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ContactFilter2D contactFilter;
    Animator animator;
    Enemy enemy;
    Rigidbody2D rb;
    List<RaycastHit2D> collisionList = new List<RaycastHit2D>();
    SpriteRenderer spriteRenderer;

    private DateTime lastMoved;
    private float moveInterval = 1f;
    private float speed = 0.6f;
    private Vector2 direction = Vector2.zero;

    public float collisionOffset = 0.1f;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemy.state != Enemy.EnemyState.dead && enemy.state != Enemy.EnemyState.attacking)
        {
            DoAction();
        }
    }

    void DoAction()
    {
        Vector2 playerPosition = Player.instance.controller.transform.position;
        Vector2 enemyPosition = transform.position;

        if ((playerPosition - enemyPosition).magnitude < 0.5f)
        {
            direction = (playerPosition - enemyPosition).normalized;
            animator.SetBool("Moving", true);
            spriteRenderer.flipX = direction.x < 0;
        }
        else if (DateTime.Now >= lastMoved)
        {
            if (direction == Vector2.zero)
            {
                direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
                animator.SetBool("Moving", true);
            }
            else
            {
                direction = Vector2.zero;
                animator.SetBool("Moving", false);
            }

            lastMoved = DateTime.Now.AddSeconds(moveInterval);
            spriteRenderer.flipX = direction.x < 0;
        }

        TryMove(direction);
        
    }

    private bool TryMove(Vector2 direction) {
        if (Player.instance.playerStats.dead)
            return false;


        if (direction == Vector2.zero) {
            return false;
        }

        int hitCount = rb.Cast(
            direction,
            contactFilter,
            collisionList,
            speed * Time.fixedDeltaTime + collisionOffset
        );

        if (hitCount == 0) {
            rb.MovePosition(rb.position + Time.fixedDeltaTime * speed * direction);
            return true;
        }

        return false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(0.5f);
        }
    }
}
