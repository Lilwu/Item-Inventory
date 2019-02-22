using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;

    //20190221
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Image draggableItem;

    private ItemSlot draggedSlot;

    private void OnValidate()
    {
        inventory = FindObjectOfType<Inventory>();
        equipmentPanel = FindObjectOfType<EquipmentPanel>();

        if (itemTooltip == null)
            itemTooltip = FindObjectOfType<ItemTooltip>();
    }


    private void Awake()
    {
        //inventory.OnRightClickEvent += EquipFormInventory;
        //equipmentPanel.OnRightClickEvent += UnequipFromEquipPanel;

        //滑鼠游標移動物品 20190221

        //Setup Events:
        //Right Click
        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += Unequip;
        //Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;
        //Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;
        //Begin Drag
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;
        //End Drag
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;
        //Drag
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;
        //Drop
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
    }

    //滑鼠游標移動物品 20190221
    private void Equip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if(equippableItem != null)
        {
            Equip(equippableItem);
        }
    }

    private void Unequip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            Unequip(equippableItem);
        }
    }

    private void ShowTooltip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            itemTooltip.transform.position = Input.mousePosition;
            itemTooltip.ShowTooltip(equippableItem);
        }

        //新增補助道具顯示功能 20190222
        RestorableItem restorableItem = itemSlot.Item as RestorableItem;
        if (restorableItem != null)
        {
            itemTooltip.transform.position = Input.mousePosition;
            itemTooltip.ShowTooltip(restorableItem);
        }
    }

    private void HideTooltip(ItemSlot itemSlot)
    {
        itemTooltip.HideTooltip();
    }

    private void BeginDrag(ItemSlot itemSlot)
    {
        if(itemSlot.Item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(ItemSlot itemSlot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(ItemSlot itemSlot)
    {
       draggableItem.transform.position = Input.mousePosition;
    }

    private void Drop(ItemSlot dropItemSlot)
    {
        if(dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(draggedSlot.Item))
        {
            EquippableItem dragItem = draggedSlot.Item as EquippableItem;
            EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

            if(draggedSlot is EquipmentSlot)
            {
                if (dragItem != null)
                 dragItem.Unequip(this);
                if (dropItem != null)
                 dropItem.Equip(this);

                //TODO 移動inventory物品到equipment上數量沒有減少 20190222
                inventory.AddItem(dropItem);
            }

            if(dropItemSlot is EquipmentSlot)
            {
                if (dragItem != null) 
                dragItem.Equip(this);
                if (dropItem != null) 
                dropItem.Unequip(this);
            }

            Item draggedItem = draggedSlot.Item;
            draggedSlot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
        }
    }

    private void EquipFormInventory(Item item)
    {
        if(item is EquippableItem)
        {
            Equip((EquippableItem)item);
        }
    }

    private void UnequipFromEquipPanel(Item item)
    {
        if (item is EquippableItem)
        {
            Unequip((EquippableItem)item);
        }
    }

    public void Equip(EquippableItem item)
    {
        if(inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if(equipmentPanel.AddItem(item , out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                }
            }
        }
        else
        {
            inventory.AddItem(item);
        }
    }

    public void Unequip(EquippableItem item)
    {
        if(!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            inventory.AddItem(item);
        }
    }
}
