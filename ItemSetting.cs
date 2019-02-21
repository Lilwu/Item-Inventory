using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//給GameObject抓取Item.asset數值;
public class ItemSetting : MonoBehaviour
{
    public EquippableItem setting;

    private void Awake()
    {
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            GetComponent<ItemBase>().OnPickUp();
            FindObjectOfType<Inventory>().AddItem(setting);
            Debug.Log("消除物件" + setting.ItemName);
        }
    }
}
