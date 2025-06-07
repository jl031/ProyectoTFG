using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject gameOverScreen;
    private DateTime iframeEnd;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        iframeEnd = DateTime.Now;
    }


    public void TakeDamage(float damage)
    {

        if (iframeEnd < DateTime.Now)
        {
            iframeEnd = DateTime.Now.AddSeconds(0.25f);
            StartCoroutine(DamageFlash(0.15f));
            Player.instance.playerStats.health -= damage;

            if (Player.instance.playerStats.health <= 0)
            {
                Player.instance.playerStats.dead = true;

                Animator animator = gameObject.GetComponent<Animator>();
                animator.SetTrigger("Dead");
                gameOverScreen.SetActive(true);
                RunDataHandler.EndGame();
            }
        }
    }

    private IEnumerator DamageFlash(float duration) {
        spriteRenderer.color = new Color(10, 10, 10);
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = Color.white;
    }
}
