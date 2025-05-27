using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

    public Image hpBar;
    public Text hpText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float maxHP = Player.instance.playerStats.maxHealth;
        float HP = Player.instance.playerStats.health;

        hpBar.fillAmount = HP/maxHP;
                
    }
}
