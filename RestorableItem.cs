using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RestoreItemType
{
    Potion,
    Scroll,
}


[CreateAssetMenu]
public class RestorableItem : Item
{
    public int add_HP;       //回復血量
    public int add_MP;       //回復魔力
    public int add_Speed;    //增加速度
    [Space]
    public bool BackToTown;   //傳送回村莊

    public RestoreItemType restoreItemType;
}
