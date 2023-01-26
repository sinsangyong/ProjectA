using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject  // ���� ������Ʈ�� ���� �ʿ� X 
{
    public enum ItemType  // ������ ����
    {
        Equipment,
        Used,
        ETC,
    }

    public int index; // ��뿩�� �����
    public string itemName; // �������� �̸�
    public ItemType itemType; // ������ ����
    public Sprite itemImage; // �������� �̹���(�κ� �丮 �ȿ��� ���)
    public GameObject itemPrefab;  // �������� ������ (������ ������ ���������� ��)
    public int itemPrice;
    public string itemDescript;

    public string weaponType;  // ���� ���� 

    public Item(string itemName, ItemType itemType, Sprite itemImage, GameObject itemPrefab, int itemPrice, string itemDescript)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemImage = itemImage;
        this.itemPrefab = itemPrefab;
        this.itemPrice = itemPrice;
        this.itemDescript = itemDescript;
    }
}

