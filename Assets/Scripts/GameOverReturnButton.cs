using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverReturnButton : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    } 
}
