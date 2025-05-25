using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static string targetScene; // Статическая переменная для хранения имени сцены

    public static void LoadScene(string sceneName)
    {
        targetScene = sceneName;
        SceneManager.LoadScene("LoadingScreen"); // Загружаем сцену загрузки
    }
}