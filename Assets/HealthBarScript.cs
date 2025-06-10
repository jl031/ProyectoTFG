using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

    public Image hpBar;
    public Text hpText;
    public Text scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerStats stats = Player.instance.playerStats;
        float maxHP = stats.maxHealth;
        float HP = stats.health;

        RunData data  = RunDataHandler.runData;

        hpBar.fillAmount = HP/maxHP;

        scoreText.text = $"Score: {data.score}\nMoney: {data.money}";                
    }
}
