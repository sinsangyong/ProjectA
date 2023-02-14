using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ReStart()
    {
        Time.timeScale = 1;
        StartCoroutine(SceneChange("InGame"));
    }

    public void Quit()
    {
        StartCoroutine(SceneChange("GameStartScene"));
    }
    IEnumerator SceneChange(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}
