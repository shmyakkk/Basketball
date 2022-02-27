using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProperty
{
    private int price;
    private bool avaible;

    public ItemProperty(int price, bool avaible)
    {
        this.price = price;
        this.avaible = avaible;
    }
    public int Price { get => price; set => price = value; }
    public bool Avaible { get => avaible; set => avaible = value; }
}
