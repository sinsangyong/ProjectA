using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect // �� �����ۿ� ���� ȿ���� ���� class
{
    public string itemName;  // �������� �̸�  --> Key��
    [Tooltip("HP, MP �� ����")] // DP �� �������� ȸ���� �Ǹ� �ȵȴ�.
    public string[] part;  // ȿ��. ��� �κ��� ȸ���ϰų� ������ �� ��������. ���� �ϳ��� ��ġ�� ȿ���� �������� �� �־� �迭�� ��.
    public int[] num;  // ��ġ. ���� �ϳ��� ��ġ�� ȿ���� �������� �� �־� �迭�� ��. �׿� ���� ��ġ ���ϱ�.
}

public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects; // �������� �߰� ȿ���� ���� �迭
    //ItemEffectDatabase.cs ���� ���� ���� �����۵��� �� `itemEffects` �迭 ������� �ѹ��� �����ϰ� ��.

    private const string HP = "HP", MP = "MP"/*, DP = "DP"*/;  // DP �� �������� ȸ�� �Ұ��� �ϰ� 
    
    private StatusManager thePlayerStatus; 
    [SerializeField]
    private WeaponManager theWeaponManager;

    [SerializeField]
    private ItemQuickSlotController theItemQuickSlotController;

    void Start()
    {
        thePlayerStatus = FindObjectOfType<StatusManager>();
    }
    // QuickSlotController -> Slot�� ¡�˴ٸ� ����
    public void IsActivatedquickSlot(int _num)
    {
        theItemQuickSlotController.IsActivatedQuickSlot(_num);
    }

    public void UseItem(Item _item)
    {
        if (_item.itemType == Item.ItemType.Equipment)
        {
          // ������� ��� �����ϴ� �ڵ����� ���� ������ ���� x
          // StartCoroutine(theWeaponManager.ChangeWeaponCoroutine(item.weaponType, item.itemName));
        }
        if (_item.itemType == Item.ItemType.Used) // �Ҹ�ǰ�϶�
        {
            for (int i = 0; i < itemEffects.Length; i++)
            {
                if (itemEffects[i].itemName == _item.itemName)
                {
                    for (int j = 0; j < itemEffects[i].part.Length; j++)
                    {
                        switch (itemEffects[i].part[j])
                        {
                            case HP:
                                if (thePlayerStatus.maxHp == thePlayerStatus.currentHp)
                                {
                                    Debug.Log("HP�� �ִ�ġ�Դϴ�.");
                                }
                                else
                                {
                                    Debug.Log("HP ȸ��");
                                    thePlayerStatus.IncreaseHP(itemEffects[i].num[j]);
                                }
                                break;
                            case MP:
                                if (thePlayerStatus.maxMp == thePlayerStatus.currentMp)
                                {
                                    Debug.Log("MP�� �ִ�ġ�Դϴ�.");
                                }
                                else
                                {
                                    Debug.Log("MP ȸ��");
                                    thePlayerStatus.IncreaseMana(itemEffects[i].num[j]);
                                }
                                break;
                            
                            default:
                                Debug.Log("HP, MP �� �����մϴ�.");
                                break;
                        }
                        Debug.Log(_item.itemName + " �� ����߽��ϴ�.");
                    }
                    return;
                }
            }
            Debug.Log("itemEffectDatabase�� ��ġ�ϴ� itemName�� �����ϴ�.");
        }
    }
}