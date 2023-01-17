using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNumber : MonoBehaviour
{
    private bool activated = false; // Ȱ��ȭ �Ǿ��ִ��� �ƴ���

    [SerializeField]
    private Text text_Preview; // ����� �� ���� ǥ��
    [SerializeField]
    private Text text_Input; // ������ �Է��� ��
    [SerializeField]
    private InputField if_text; // Ȱ��ȭ �� �� �ؽ�Ʈ ����� �ʱ�ȭ �ؾ��� text ����
                                // inputfield �������� �ҷ��ͼ� ����� ����

    [SerializeField]
    private GameObject go_Base; // inputfield ui������Ʈ�� �Ҵ� ��, �Է��ʵ� Ȱ��ȭ ��Ȱ�� ���� �׿�����Ʈ ����

    [SerializeField]
    private ActionController thePlayer; // ���� ������ ������ actioncontroller ���� �Ұ�

    [SerializeField]
    private Transform thePlayerTrans;

    void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.Return)) OK();

            else if (Input.GetKeyDown(KeyCode.Escape)) Cancel();
        }
    }

    public void Call() // �巡�װ� ������ ȣ��
    {
        go_Base.SetActive(true);
        activated = true;
        if_text.text = "";
        text_Preview.text = ItemShadow.instance.itemShadowSlot.itemCount.ToString();
    }

    public void Cancel()
    {
        activated = false;
        ItemShadow.instance.SetColor(0);
        go_Base.SetActive(false);
        ItemShadow.instance.itemShadowSlot = null;
    }

    public void OK()
    {
        ItemShadow.instance.SetColor(0);

        int num;
        if (text_Input.text != "") // ���ڰ� �ִٸ�
        {
            if (CheckNumber(text_Input.text))
            {
                num = int.Parse(text_Input.text);
                if (num > ItemShadow.instance.itemShadowSlot.itemCount)
                {
                    num = ItemShadow.instance.itemShadowSlot.itemCount;
                }
            }
            else
            {
                num = 1;
            }
                
        }
        else
        {
            num = int.Parse(text_Preview.text);

        }
        StartCoroutine(DropItemCorountine(num));
    }

    IEnumerator DropItemCorountine(int _num)
    {
        for (int i = 0; i < _num; i++)
        {
            if (ItemShadow.instance.itemShadowSlot.item.itemPrefab != null) // ������ �ȶ� inputnumber base �Ȳ��� �ȸ� ���⼭ null (�翬��)
            {
                Transform trans = GameObject.Find("Player").transform; // �ڱ� �ڽ��� ã��
                Instantiate(ItemShadow.instance.itemShadowSlot.item.itemPrefab,
                trans.position + trans.forward,
                Quaternion.identity); // ������ ������ ���� �÷��̾� �� �տ��ٰ�
            }
            ItemShadow.instance.itemShadowSlot.SetSlotCount(-1); // ���� �ϳ��� ���� �Է� ������ �� ��ŭ �ݺ�
            yield return new WaitForSeconds(0.05f);
        }

        ItemShadow.instance.itemShadowSlot = null;
        go_Base.SetActive(false);
        activated = false;
    }

    private bool CheckNumber(string _argString)
    {
        char[] _tempCharArray = _argString.ToCharArray();
        bool isNumber = true;

        for (int i = 0; i < _tempCharArray.Length; i++)
        {
            if (_tempCharArray[i] >= 48 && _tempCharArray[i] <= 57) continue; // �ƽ�Ű �ڵ� 47 ~ 57 �̸� ���� �ƴϸ� ����
            
            isNumber = false;
        }
        return isNumber;
    }
}