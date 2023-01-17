using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] private Button ButtontoQuit;

    public ToggleGroup inventoryToggleGroup;
    public Toggle[] toggles;

    [SerializeField] private Toggle toggleInventoryUsed;
    [SerializeField] private Toggle toggleInventoryEquipment;
    [SerializeField] private Toggle toggleInventoryETC;

    [SerializeField] private GameObject inventoryScreen;
    [SerializeField] private GameObject background;



    private Inventory inventory;
    //public Item item;
    public Slot[] slots;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        slots = inventory.GetSlots();
        ButtontoQuit.onClick.AddListener(() =>
        {
            inventoryScreen.SetActive(false);
            background.SetActive(false);
        });

    }

    private void Update()
    {
        /*if (Inventory.inventoryActivated == true)
        {
            if (toggleInventoryEquipment.enabled || toggleInventoryETC.enabled || toggleInventoryUsed)
            {
                CheckItemType();
            }
        }*/
        SetUpToggleChoice();

    }

    public Toggle setupToggleCurrentSeletion
    {
        get { return inventoryToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    public void SetUpToggleChoice()
    {
        if (inventoryToggleGroup.ActiveToggles().Any()) // Toggles �� �ϳ��� Active�� Toggle�� �ִٸ�
        {
            if (setupToggleCurrentSeletion.name.Equals("Toggle : Equipment"))
            {
                SelectionActive(true, false, false);
            }
            else if (setupToggleCurrentSeletion.name.Equals("Toggle : Used"))
            {
                SelectionActive(false, true, false);
            }
            else if (setupToggleCurrentSeletion.name.Equals("Toggle : ETC"))
            {
                SelectionActive(false, false, true);
            }
        }
    }
    void SelectionActive(bool equipType, bool usedType, bool etcType)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (equipType == true)
                {
                    if (slots[i].item.itemType != Item.ItemType.Equipment)
                    {
                        Debug.Log("equip");
                        slots[i].SetColor(0f);
                    }
                }
                else
                {
                    slots[i].SetColor(1f);
                }

                if (usedType == true)
                {
                    if (slots[i].item.itemType != Item.ItemType.Used)
                    {
                        Debug.Log("Used");
                        slots[i].SetColor(0f);
                    }
                }
                else
                {
                    slots[i].SetColor(1f);
                }

                if (etcType == true)
                {
                    if (slots[i].item.itemType != Item.ItemType.ETC)
                    {
                        Debug.Log("ETC");
                        slots[i].SetColor(0f);
                    }
                }
                else
                {
                    slots[i].SetColor(1f);
                }
            }
     
        }
    }
   /* private void CheckItemType()
    {
        Debug.Log("CheckItemType");
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                toggleInventoryEquipment.onValueChanged.AddListener((bool p) =>
                {
                    if (p)
                    {
                        if (slots[i].item.itemType != Item.ItemType.Equipment)
                        {
                            slots[i].SetColor(0f);
                        }
                    }
                });

                toggleInventoryUsed.onValueChanged.AddListener((bool p) =>
                {
                    if (p)
                    {
                        if (slots[i].item.itemType != Item.ItemType.Used)
                        {
                            slots[i].SetColor(0f);
                        }
                    }
                });

                toggleInventoryETC.onValueChanged.AddListener((bool p) =>
                {
                    if (p)
                    {
                        if (slots[i].item.itemType != Item.ItemType.ETC)
                        {
                            slots[i].SetColor(0f);
                        }
                    }
                });
                //slots[i].SetColor(1f);
            }
        }
    }*/
}
