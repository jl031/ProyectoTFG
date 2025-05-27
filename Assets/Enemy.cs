using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health{
        get {
            return health;
        }
        set {
            health = value;
            if (health <= 0) {
                Defeat();
            }
        }

    }

    public float health = 1f;
    
    float damageFlashDuration = 0.2f;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Color damageFlashColor = new Color(5, 5, 5);
    Color defaultColor;

    void Start()
    {
        animator = GetComponent<Animator>();   
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    public void TakeDamage(float damage) {
        if (Health <= 0) {
            return;
        }

        Health -= damage;
        StartCoroutine(DamageFlash(damageFlashDuration));
    } 

    private IEnumerator DamageFlash(float duration) {
        spriteRenderer.color = damageFlashColor;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = defaultColor;
    }

    private void Defeat(){
        animator.SetTrigger("Defeated");        
    }

    public void PostDefeat(){
        Destroy(gameObject);
    }
}
