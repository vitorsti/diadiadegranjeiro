using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Slider progressBar;
    public Text progressText;
    public void LoadScene(string _sceneToLoad)
    {
        StartCoroutine(LoadAsync(_sceneToLoad));
    }

    IEnumerator LoadAsync(string sceneToLoad)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);
            progressBar.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;

        }
    }
}
