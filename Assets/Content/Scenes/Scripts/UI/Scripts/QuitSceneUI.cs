using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitSceneUI : MonoBehaviour
{
    [SerializeField]
    private Button reStartBtn;
    [SerializeField]
    private Button quitBtn;

    public string sceneName = "InGame";

    public static QuitSceneUI instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        quitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        reStartBtn.onClick.AddListener(() =>
        {
            Debug.Log("Lode Game");
            StartCoroutine(LoadSceneCoroutine());
        });
    }

    IEnumerator LoadSceneCoroutine()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        //여기 while문에다가 operation.progress 이용해 로딩화면을 또 만들어줘도 된다.
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        // Canvas는  비활성화
        gameObject.SetActive(false);
    }
}
