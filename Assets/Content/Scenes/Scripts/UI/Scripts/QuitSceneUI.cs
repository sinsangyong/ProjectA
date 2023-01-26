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

        //���� while�����ٰ� operation.progress �̿��� �ε�ȭ���� �� ������൵ �ȴ�.
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        // Canvas��  ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
