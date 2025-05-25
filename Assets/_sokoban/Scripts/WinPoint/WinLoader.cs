using UnityEngine.SceneManagement;
using UnityEngine;

public class WinLoader : MonoBehaviour
{
    [SerializeField] private string[] levelScenes = 
        { 
        "Level 1", 
        "Level 2", 
        "Level 3", 
        "Level 4", 
        "Level 5", 
        "Level 6", 
        "Level 7",
        "Level 8",
        "Level 9",
        "Level 10",
    };

    public void PlayerWin()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < levelScenes.Length)
        {
            SceneLoader.LoadScene(levelScenes[nextSceneIndex]);
        }
        else
        {
            SceneLoader.LoadScene("MainMenu");
        }
    }
}