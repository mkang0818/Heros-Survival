using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoad : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI loadtext;

    private bool readyToActivate = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("GameScene");
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            yield return null;
            progressBar.value = operation.progress;

            if (operation.progress >= 0.9f)
            {
                loadtext.text = "Press SpaceBar to continue";
                readyToActivate = true;
            }
            else
            {
                loadtext.text = "Loading...";
            }

            if (readyToActivate && Input.GetKeyDown(KeyCode.Space))
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
