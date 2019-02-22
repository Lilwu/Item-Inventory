﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] Image image;
    [SerializeField] ItemTooltip tooltip;

    public event Action<ItemSlot> OnRightClickEvent;

    //滑鼠游標移動物品 20190221
    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);


    private Item _item;
    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null)
            {
                image.color = disabledColor;
                //image.enabled = false;
            }
            else
            {
                image.sprite = _item.Icon;
                image.color = normalColor;  
                //image.enabled = true;
            }
        }
    }


    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
        if (tooltip == null)
            tooltip = FindObjectOfType<ItemTooltip>();
    }

    //20190221
    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }




    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (Item != null && OnRightClickEvent != null)
            OnRightClickEvent(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if(Item is EquippableItem)
        //{
        //    tooltip.ShowTooltip((EquippableItem)Item);
        //    //讓物品資訊依照滑鼠游標顯示;
        //    if(Item != null)
        //    {
        //        tooltip.transform.position = eventData.position;
        //    }
        //}
        ////補助道具顯示 20190221
        //if (Item is RestorableItem)
        //{
        //    tooltip.ShowTooltip((RestorableItem)Item);
        //    //讓物品資訊依照滑鼠游標顯示;
        //    if (Item != null)
        //    {
        //        tooltip.transform.position = eventData.position;
        //    }
        //}

        if (OnPointerEnterEvent != null)
            OnPointerEnterEvent(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //tooltip.HideTooltip();
        if (OnPointerExitEvent != null)
            OnPointerExitEvent(this);
    }



    //滑鼠游標移動物品 20190221
    Vector2 originalPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item != null)
            image.color = disabledColor;

        if (OnBeginDragEvent != null)
            OnBeginDragEvent(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Item != null)
            image.color = normalColor;

        if (OnEndDragEvent != null)
            OnEndDragEvent(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
            OnDragEvent(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
            OnDropEvent(this);


    }
}
