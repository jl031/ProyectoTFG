using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void TakeDamage(float damage)
    {
        Player.instance.playerStats.health -= damage;

        if (Player.instance.playerStats.health <= 0)
        {

            Player.instance.playerStats.dead = true;

            Animator animator = gameObject.GetComponent<Animator>();
            animator.SetTrigger("Dead");
        }
    }
}
