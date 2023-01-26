using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    
    public Animator anim; // ���� �尡�� ������ npc�� �����ִ� ������ �ִϸ��̼� �ֱ� ����

    [SerializeField]
    private Player enterPlayer;

    public static bool shopActivated = false;  // ���� Ȱ��ȭ ����. true�� �Ǹ� ī�޶� �����Ӱ� �ٸ� �Է��� ���� ���̴�.
    private GameObject go_ShopBase; // Shop_Base �̹���

    public GameObject itemObject;

    public int itemPrice;

    public Transform itemSpawnPos;
    
    public ShopItemSlotUI shopItems;
    

    private Inventory inventory = null;
    
    public void Enter(Player player)
    {
        enterPlayer = player;
        shopActivated = true;
        
        if(go_ShopBase == null)
        {
            string path = "Canvas/Shop UI/Image : ShopFrame";
            go_ShopBase = GameManager.Instance.FindGameObject(path);
        }
        
        go_ShopBase.SetActive(true);

        FindObject();
        inventory.OpenInventory();
    }

    public void Exit()
    {
        if (go_ShopBase != null)
        {
            shopActivated = false;
            go_ShopBase.SetActive(false);
            FindObject();
            inventory.CloseInventory();
        }
    }

    void FindObject()
    {
        if (inventory == null)
        {
            string path = "Canvas/Inventory";
            inventory = GameManager.Instance.FindGameObject(path).GetComponent<Inventory>();
        }
        if (go_ShopBase == null)
        {
            string path = "Canvas/Shop UI/Image : ShopFrame";
            go_ShopBase = GameManager.Instance.FindGameObject(path);
        }
    }
}
