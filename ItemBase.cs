using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{   

    public void OnPickUp()
    {
        gameObject.SetActive(false);
    }
}
