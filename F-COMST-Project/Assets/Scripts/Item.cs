using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Stats
{
    public string name;
    public int power;
}
 [Serializable]
public class Item
{
    public int Id;
    public string Title;
    public Sprite Icon;
    public List<Stats> Stats = new List<Stats>();

    public Item(int id, string title, List<Stats> stats)
    {
        Id = id;
        Title = title;
        Icon = Resources.Load<Sprite>("Sprites/Items/" + title);
        Stats = stats;
    }

    public Item(Item item)
    {
        Id = item.Id;
        Title = item.Title;
        Resources.Load<Sprite>("Sprites/Items/" + item.Title);
        Stats = item.Stats;
    }
}
