using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("TutorialScene"); 
    }

    public void Continue()
    {
        // Implement continue game logic here
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene("LeaderboardScene"); 
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
