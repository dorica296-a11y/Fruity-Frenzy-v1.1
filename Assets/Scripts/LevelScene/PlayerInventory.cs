using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventory : MonoBehaviour, IInventory
{
    public int Money {get => _money; set => _money = value; }

    public int _money = 0;
}