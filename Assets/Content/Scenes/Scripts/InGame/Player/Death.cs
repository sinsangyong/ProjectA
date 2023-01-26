using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private static Death instance;
    public static Death Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<Death>();
            return instance;
        }
    }

    [SerializeField]
    private GameObject gameOver;
    private void Start()
    {
    }

    public void HandleDeath()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}


