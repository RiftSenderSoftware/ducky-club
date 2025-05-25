using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressText;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync(SceneLoader.targetScene));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = true;

        // Обновляем прогресс
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // operation.progress доходит до 0.9
            if (progressBar != null)
                progressBar.value = progress;
            if (progressText != null)
                progressText.text = $"Загрузка... {(progress * 100):0}%";

            yield return null;
        }
    }
}