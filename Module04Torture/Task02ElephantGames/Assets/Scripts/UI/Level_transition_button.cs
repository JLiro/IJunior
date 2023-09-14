using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_transition_button : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}