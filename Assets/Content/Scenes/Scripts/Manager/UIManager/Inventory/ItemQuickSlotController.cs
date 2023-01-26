using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemQuickSlotController : MonoBehaviour
{
    private ItemEffectDatabase theItemEffectDatabase;


    [SerializeField] private Slot[] quickSlots;  // �����Ե� (6��)
    [SerializeField] private Transform tf_parent;  // �����Ե��� �θ� ������Ʈ

    private int selectedSlot;  // ���õ� �������� �ε��� (0~5)
    [SerializeField] private GameObject go_SelectedImage;  // ���õ� ������ �̹���
    
    void Start()
    {
        theItemEffectDatabase = FindObjectOfType<ItemEffectDatabase>();

        quickSlots = tf_parent.GetComponentsInChildren<Slot>();
        selectedSlot = 0;
    }

    private void Update()
    {
        TryUsingQuickSlot();
    }
    private void TryUsingQuickSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            UsingSlotItem(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            UsingSlotItem(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            UsingSlotItem(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            UsingSlotItem(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            UsingSlotItem(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            UsingSlotItem(5);
    }
    private void UsingSlotItem(int _num)
    {
        SelectedSlot(_num);
        Execute();
    }

    private void SelectedSlot(int _num)
    {
        // ���õ� ����
        selectedSlot = _num;

        // ���õ� �������� ���� ȿ�� �̹��� �̵��ϱ�
        go_SelectedImage.transform.position = quickSlots[selectedSlot].transform.position;
    }

    // ������ ��� ó���ϴ� �Լ�
    private void Execute()
    {
        StatusManager sm = GameManager.Instance.statusMgr;
        if (quickSlots[selectedSlot].item != null)
        {
            // ���õ� �������� �������� �Ҹ�ǰ�϶�
            if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Used)
            {
                // ü���̳� ������ �� ������ ������
                if (!(sm.currentHp == sm.maxHp))
                {
                    theItemEffectDatabase.UseItem(quickSlots[selectedSlot].item);
                    quickSlots[selectedSlot].SetSlotCount(-1);
                }
                else if (!(sm.currentMp == sm.maxMp))
                {
                    theItemEffectDatabase.UseItem(quickSlots[selectedSlot].item);
                    quickSlots[selectedSlot].SetSlotCount(-1);
                }
                else
                {
                    Debug.Log("���°� ���Ƽ� ����� �� �����~");
                }
            }
            else
            {
                Debug.Log("�Һ� �����۸� ��� ����");
            }
        }
        else
        {
            Debug.Log("Item�� ���~");
        }
    }

    public void IsActivatedQuickSlot(int _num)
    {
        if (selectedSlot == _num)
        {
            Execute();
            return;
        }
        if (ItemShadow.instance != null)
        {
            if (ItemShadow.instance.itemShadowSlot != null)
            {
                if (ItemShadow.instance.itemShadowSlot.GetQuickSlotIndex() == selectedSlot)
                {
                    Execute();
                    return;
                }
            }
        }
    }


}
