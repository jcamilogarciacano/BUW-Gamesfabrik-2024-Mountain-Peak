using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            LoadGameScene();
        }
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("Demo3");
    }
}
