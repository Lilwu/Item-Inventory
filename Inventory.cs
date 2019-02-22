using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    [FormerlySerializedAs("items")]
    [SerializeField] List<Item> startingItem;
    [SerializeField] Transform itemParent;
    [SerializeField] ItemSlot[] itemSlots;

    //撿取物品 20190222

    public event Action<ItemSlot> OnRightClickEvent;

    //滑鼠游標移動物品 20190221
    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private void Awake()
    {
        //for (int i = 0; i < itemSlots.Length; i++)
        //{
        //    itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        //}

        //SetStartingItem();
    }
    //滑鼠游標移動物品 20190221
    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
        }
        SetStartingItem();
    }

    private void OnValidate()
    {
        if (itemParent != null)
            itemSlots = itemParent.GetComponentsInChildren<ItemSlot>();

        SetStartingItem();
    }

    private void SetStartingItem()
    {
        int i = 0;
        for (; i < startingItem.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = startingItem[i];
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    //private void RefreshUI()
    //{
    //    int i = 0;
    //    for (; i < items.Count && i < itemSlots.Length; i++)
    //    {
    //        itemSlots[i].Item = items[i];
    //    }

    //    for (; i < itemSlots.Length; i++)
    //    {
    //        itemSlots[i].Item = null;
    //    }
    //}


    //public bool AddItem(Item item)
    //{
    //    if (IsFull())
    //        return false;

    //    items.Add(item);
    //    RefreshUI();
    //    return true;
    //}

    //public bool RemoveItem(Item item)
    //{
    //    if(items.Remove(item))
    //    {
    //        RefreshUI();
    //        return true; 
    //    }
    //    return false;
    //}

    //public bool IsFull()
    //{
    //    return items.Count >= itemSlots.Length;
    //}

    public bool AddItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == null)
            {
                itemSlots[i].Item = item;
                //20190222
                startingItem.Add(item);
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == item)
            {
                itemSlots[i].Item = null;
                //20190222
                startingItem.Remove(item);
                return true;
            }
        }
        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                return false;
            }
        }
        return true;
    }
}
