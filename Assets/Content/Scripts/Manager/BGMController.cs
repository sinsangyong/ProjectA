using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMType { Stage = 0, Boss}

public class BGMController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] bgmClips;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeBGM(BGMType index)
    {
        // ���� ��� ���� ������� ����
        audioSource.Stop();

        // ������� ���� ��Ͽ��� index��° ����������� ���� ��ü
        audioSource.clip = bgmClips[(int)index];

        //�ٲ� ������� ���
        audioSource.Play();
    }
}
