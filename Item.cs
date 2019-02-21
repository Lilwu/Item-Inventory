using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite Icon;
}

public class ItemEventArgs: EventArgs
{
    public ItemEventArgs(Item item)
    {
        Item = item;
    }

    public Item Item;
}

