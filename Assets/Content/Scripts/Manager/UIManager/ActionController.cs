using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;  // ������ ������ ������ �ִ� �Ÿ�

    private bool pickupActivated = false;  // ������ ���� �����ҽ� True 

    private RaycastHit hitInfo;  // �浹ü ���� ����

    [SerializeField]
    private LayerMask layerMask;  // Ư�� ���̾ ���� ������Ʈ�� ���ؼ��� ������ �� �־�� �Ѵ�.

    [SerializeField]
    private Text actionText;  // �ൿ�� ���� �� �ؽ�Ʈ ���� �ȸ���
    
    void Update()
    {
        // ��ġ�� �պ� ��
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }   
    }

    private void CheckItem()
    {
        // RAYCAST�� �� �� ���߱� ���� spherecast�� ����
        if (Physics.SphereCast(transform.position, transform.lossyScale.x / 2, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
        {
            ItemInfoDisappear();
        }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
    }

    private void CanPickUp() // ������ �ֿ� ��
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ��!");
                Inventory.Instance.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }
}