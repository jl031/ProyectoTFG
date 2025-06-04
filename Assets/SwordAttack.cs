using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public BoxCollider2D swordHitbox;
    public float rightAttackOffset = 0.03848194f;
    public AttackDirection attackDirection;
    public float damage = 0.5f;

    public enum AttackDirection{
        left, right, up, down
    }

    private List<Collider2D> objectsCollided = new List<Collider2D>();

    private void Start()
    {
        swordHitbox = GetComponent<BoxCollider2D>();   
        swordHitbox.enabled = false;
        attackDirection = AttackDirection.down;
    }
    
    private void StartAttackLeft(){
        swordHitbox.enabled = true;
        transform.localPosition = new Vector3(-rightAttackOffset, 0, 0);
    }

    private void StartAttackRight(){
        swordHitbox.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset, 0, 0);
    }

    private void StartAttackUp(){
        swordHitbox.enabled = true;
        transform.localPosition = Vector3.zero;
    }

    private void StartAttackDown(){
        swordHitbox.enabled = true;
        transform.localPosition = Vector3.zero;
    }

    public void Attack(){
        if (swordHitbox.enabled)
        {
            return;
        }
        
        switch (attackDirection)
        {
            case AttackDirection.left:
                StartAttackLeft();
                break;
            case AttackDirection.right:
                StartAttackRight();
                break;
            case AttackDirection.down:
                StartAttackDown();
                break;
            case AttackDirection.up:
                StartAttackUp();
                break;
        }
    }

    public void StopAttack(){
        swordHitbox.enabled = false;
        objectsCollided.Clear();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!objectsCollided.Contains(collision)) {
            CheckEnemy(collision);
            objectsCollided.Add(collision);
        }
    }

    private void CheckEnemy(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null) {
                
                enemy.TakeDamage(damage + Player.instance.playerStats.damage);
            }

        }   
    }

}
