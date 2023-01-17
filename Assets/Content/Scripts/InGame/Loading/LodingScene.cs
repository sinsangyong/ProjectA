using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LodingScene : MonoBehaviour
{
    private static string nextScene;
    [SerializeField]
    private Image bar;
    private float loadingPercent;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene(nextScene);
    }
    private void Start()
    {
        loadingPercent = 0f;
        StartCoroutine(LoadScene());
    }
    private IEnumerator LoadScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("GameStartScene");
        op.allowSceneActivation = false; // ����� �غ�� ��� ����� Ȱ��ȭ�Ȱ� false

        float timer = 0f;
        while (!op.isDone) // isDone = �ش� ������ �Ϸ�Ǿ������� ��Ÿ��
        {
            yield return null;

            if (op.progress < loadingPercent) // progress = �۾��� ������¸� ��Ÿ��
            {
                bar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                bar.fillAmount = Mathf.Lerp(loadingPercent, 1f, timer);
                if(bar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
