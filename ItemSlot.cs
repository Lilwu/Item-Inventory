﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image image;
    [SerializeField] ItemTooltip tooltip;

    public event Action<Item> OnRightClickEvent;

    private Item _item;
    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null)
            {
                image.enabled = false;
            }
            else
            {
                image.sprite = _item.Icon;
                image.enabled = true;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (Item != null && OnRightClickEvent != null)
                OnRightClickEvent(Item);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Item is EquippableItem)
        {
            tooltip.ShowTooltip((EquippableItem)Item);
            //讓物品資訊依照滑鼠游標顯示;
            if(Item != null)
            {
                tooltip.transform.position = eventData.position;
            }
        }
        //補助道具顯示 20190221
        if (Item is RestorableItem)
        {
            tooltip.ShowTooltip((RestorableItem)Item);
            //讓物品資訊依照滑鼠游標顯示;
            if (Item != null)
            {
                tooltip.transform.position = eventData.position;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }
}
