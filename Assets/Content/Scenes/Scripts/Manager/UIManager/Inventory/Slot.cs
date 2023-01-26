using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item; // ȹ���� ������
    public int itemCount; // ȹ���� �������� ����
    public Image itemImage;  // �������� �̹���

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    private InputNumber theInputNumber;

    private SellItemUI theSellItemUI;

    private ItemEffectDatabase theItemEffectDatabase;


    [SerializeField] private bool isQuickSlot;  // �ش� ������ ���������� ���� �Ǵ�
    [SerializeField] private int quickSlotIndex;  // ������ �ѹ�

    private void Awake()
    {
        theSellItemUI = FindObjectOfType<SellItemUI>();

    }
    void Start()
    {
        theInputNumber = FindObjectOfType<InputNumber>();
        theItemEffectDatabase = FindObjectOfType<ItemEffectDatabase>();
    }


    // ������ �̹����� ���� ����
    public void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // �κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if (item.itemType != Item.ItemType.Equipment) //������� �ƴҶ� 
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else // ������̸� ���� ǥ�� x ���� ǥ�� �̹����� �ؽ�Ʈ x
        {
            text_Count.text = "0"; // ���� ���� �ڽ� ����
            go_CountImage.SetActive(false);
        }
        SetColor(1);
    }

    // �ش� ������ ������ ���� ������Ʈ
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    // �ش� ���� �ϳ� ����
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) // ItemEffectDataBase���� ��� �Ϻ� ���� ���� ���
    {
        StatusManager sm = GameManager.Instance.statusMgr;
        if (eventData.button == PointerEventData.InputButton.Right) // ������ ���콺 ������ ���
        {
            if (item != null)
            {
                theItemEffectDatabase.UseItem(item); // database���� ������ ��� ���� ��� ����

                if (item.itemType == Item.ItemType.Used)
                {
                    // �ǰ� ���� ���ְų� ������ ���� ������ ���� �ƴҶ����� ������ ����
                    if (!(sm.currentHp == sm.maxHp))
                    {
                        SetSlotCount(-1);
                    }
                    else if (!(sm.currentMp == sm.maxMp))
                    {
                        SetSlotCount(-1);
                    }
                    else
                    {
                        Debug.Log("���°� ���Ƽ� ����� �� �����~");
                    }
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData) // �巡�� ���� �ɶ�
    {
        if (item != null)
        {
            ItemShadow.instance.itemShadowSlot = this;
            ItemShadow.instance.DragSetImage(itemImage);
            ItemShadow.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData) // �巡�� ���� ��
    {
        if (item != null)
        {
            ItemShadow.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData) // �巡�� ������ ��
    {
        // �巡�װ� ������ ������ raycast�� ������ ù��°�� contact�� ���� ������.
        RaycastResult result = eventData.pointerCurrentRaycast;
        // 3���� ����
        switch (result.gameObject.name)
        {
            // ������ �Ǹ� �Ҷ�
            case "Image : ItemSlot":
            case "Image : ShopFrame":
                {
                    // ȣ���ؾ��� ������ �Ĵ� UI �׸��� �Ĵ� �Լ� ���
                    if (ItemShadow.instance.itemShadowSlot != null)
                    {
                        theSellItemUI.Call();  
                    }
                }
                break;
            // ������
            case "Image :: BG":
                {
                    if (ItemShadow.instance.itemShadowSlot != null) 
                    {
                        theInputNumber.Call();
                    }
                }
                break;
            // ��� = �ǵ�����
            default:
                {
                    ItemShadow.instance.SetColor(0);
                    ItemShadow.instance.itemShadowSlot = null;
                }
                break;
        }
    }

    public void OnDrop(PointerEventData eventData) // �ش� ���Կ� ������ ���콺 ��� ���� �� �� �� �ڽſ��� ���� ��ӵȰ� ���� �� ȣ�� 
    {
        if (ItemShadow.instance.itemShadowSlot != null)
        {
            ChangeSlot();

            if (isQuickSlot)  // �κ��丮->������ or ������->������
            {
                theItemEffectDatabase.IsActivatedquickSlot(quickSlotIndex);
            }
            else  // �κ��丮->�κ��丮. ������->�κ��丮
            {
                if (ItemShadow.instance.itemShadowSlot.isQuickSlot)  // ������->�κ��丮
                {
                    theItemEffectDatabase.IsActivatedquickSlot(ItemShadow.instance.itemShadowSlot.quickSlotIndex);
                }
            }
        }
    }

    private void ChangeSlot()
    {
        // a���� �巡�� �� ���� b ���� �巡�� ���ϴ� ����
        // �ٲ������ ���� ������ ������ ����
        Item _tempItem = item; 
        int _tempItemCount = itemCount;
        // a ������ ItemShadow���� ���� ��. ChangeSlot()�� ȣ��ɶ� ��� ��ġ�� �� ���Կ��ٰ� a ������ �߰�
        AddItem(ItemShadow.instance.itemShadowSlot.item, ItemShadow.instance.itemShadowSlot.itemCount);
        // a���Կ� �����س��� b���� �߰���
        if (_tempItem != null) //�󽽷��� �ƴ϶�� �߰�
        {
            ItemShadow.instance.itemShadowSlot.AddItem(_tempItem, _tempItemCount);
        }
        else // �󽽷��̾��ٸ� �巡�� �� ������ �����
        {
            ItemShadow.instance.itemShadowSlot.ClearSlot();
        }
    }


    public int GetQuickSlotIndex()
    {
        return quickSlotIndex;
    }
}
