using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugUI : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Test");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
