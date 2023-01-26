using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class TargettingIMG : MonoBehaviour
{
    private static TargettingIMG instance;

    public static TargettingIMG Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TargettingIMG>();
            }
            return instance;
        }
    }

    public GameObject inventory;
    public TMP_Text Damage;
    public GameObject canvas;
    public Transform target;
    

    public void Damageing(float damage)
    {
        StartCoroutine(OndamageText(damage));
    }

    public IEnumerator OndamageText(float damage)
    {
        canvas.SetActive(true);
        Damage.text = damage.ToString(); //�ؽ�Ʈ ���޹��� ��������
        Damage.fontSize = 60;
        for (int i = (int)Damage.fontSize; i >= 30; i--) // ��Ʈ 15�ɶ����� for�� �۵�
        {
            Damage.fontSize = i;
            yield return new WaitForFixedUpdate(); //���� fixedupdate ���� ��ٸ�
        }
        yield return new WaitForSeconds(1f);
        canvas.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(inventory.activeSelf == false)
        {
            Damage.transform.position = Camera.main.WorldToScreenPoint(target.position + new Vector3(0, 1, 0));
        }
    }
}
