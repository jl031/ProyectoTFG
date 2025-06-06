using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonDoorScript : MonoBehaviour
{

    CircleCollider2D doorCollider;
    [SerializeField] private GameObject textPrompt;
    private bool promptEnabled = false;

    void Start()
    {
        doorCollider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            promptEnabled = true;
            textPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            promptEnabled = false;
            textPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && promptEnabled)
        {
            string[] levels = {
                "Tutorial",
                "Level1"
            };

            RunData runData = RunDataHandler.runData;
            runData.floor++;
            runData.health = Player.instance.playerStats.health;

            SceneManager.LoadScene( levels[RunDataHandler.runData.floor] );
        }   
    }
}
