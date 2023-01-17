using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SellItemUI : MonoBehaviour//, IDropHandler
{
    private bool activated;
    // ������ �ȶ� ���� ui ===================================================================
    [SerializeField] private GameObject sellItemBase;
    [SerializeField] private GameObject sellPopup_main;
    [SerializeField] private GameObject sellPopup_sub;

    [SerializeField] private TMP_InputField sellItemCountInput;
    [SerializeField] private TMP_InputField if_text;
    [SerializeField] private TMP_Text sellItemName;
    [SerializeField] private TMP_Text sellItemTotalPrice;
    [SerializeField] private TMP_Text text_Preview;

    [SerializeField] private Image sellItemImage;
    [SerializeField] private Button OKbtn;

    private void Start()
    {
        OKbtn.onClick.AddListener(OK);
    }

    private void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.Return)) 
                OK();
            else if (Input.GetKeyDown(KeyCode.Escape)) 
                Cancel();
        }
    }

    // ������ ������ �޾ƿ���
    public void Call()
    {
        if (ItemShadow.instance.itemShadowSlot != null)
        {
            Debug.Log("sellUI call �Լ�");

            Slot iss = ItemShadow.instance.itemShadowSlot;

            sellItemBase.SetActive(true);
            SellPopupSub();
            activated = true;
            if_text.text = "";
            sellItemImage.sprite = iss.item.itemImage;
            sellItemName.text = iss.item.itemName;
            sellItemTotalPrice.text = (iss.item.itemPrice * 0.7f).ToString();
            text_Preview.text = iss.itemCount.ToString();
        }
    }

    public void Cancel()
    {
        activated = false;
        ItemShadow.instance.SetColor(0);
        sellItemBase.SetActive(false);
        SellPopupMain();
        ItemShadow.instance.itemShadowSlot = null;
    }

    public void OK()
    {
        ItemShadow.instance.SetColor(0);

        int num;
        if (string.IsNullOrEmpty(sellItemCountInput.text))
        {
            // ���ڰ� �ƴѰ� �Է���������
            num = 1;

            // ���ڰ� �ִٸ�
            if (CheckNumber(sellItemCountInput.text))
            {
                num = int.Parse(sellItemCountInput.text);
                if (num > ItemShadow.instance.itemShadowSlot.itemCount)
                {
                    num = ItemShadow.instance.itemShadowSlot.itemCount;
                    Debug.Log(num + ": OK �Լ� num 1��");
                }
            }
        }
        else
        {
            num = int.Parse(text_Preview.text);
            Debug.Log(num + ": OK �Լ� num 3��");
        }
        StartCoroutine(SellItemCorountine(num));
    }

    IEnumerator SellItemCorountine(int _num)
    {
        // �κ��丮�� �߰��� ���� �Ĵ� �������� 70���� ���ݸ�
        int sellprice = (int)(ItemShadow.instance.itemShadowSlot.item.itemPrice * 0.7f);
        // �Է��� _num������ŭ for��
        for (int i = 0; i < _num; i++)
        {
            if (ItemShadow.instance.itemShadowSlot.item != null)
            {
                // 1�� ������ ������ ����(?) ������ 1�߰�
                GameManager.Instance.Coin += (sellprice) + 1; 
                Inventory.Instance.coin.text = GameManager.Instance.Coin.ToString();
                // ���� �ϳ��� ���� �Է� ������ �� ��ŭ �ݺ� 
                ItemShadow.instance.itemShadowSlot.SetSlotCount(-1); 
                yield return new WaitForSeconds(0.05f);
            }
        }
        sellItemTotalPrice.text = ((_num * sellprice) + 1).ToString();
        ItemShadow.instance.itemShadowSlot = null;
        sellItemBase.SetActive(false);
        activated = false;
    }

    private bool CheckNumber(string _argString)
    {
        char[] _tempCharArray = _argString.ToCharArray();
        bool isNumber = true;

        for (int i = 0; i < _tempCharArray.Length; i++)
        {
            // �ƽ�Ű �ڵ� 47 ~ 57 �̸� ���� �ƴϸ� ����
            if (_tempCharArray[i] >= 48 && _tempCharArray[i] <= 57) 
                continue;

            isNumber = false;
        }
        return isNumber;
    }

    public void SellPopupMain()
    {
        sellItemBase.transform.SetParent(sellPopup_main.transform);
    }

    public void SellPopupSub()
    {
        sellItemBase.transform.SetParent(sellPopup_sub.transform);
    }
}
