using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> Items = new List<Item>();
    public ItemDatabase itemDatabase;

    // dunno if all the functions are necessary...
    public Item GetItem(int id)
    {
        return Items.Find(item => item.Id == id);
    }

    public Item GetItem(string title)
    {
        return Items.Find(item => item.Title == title);
    }

    public bool HasItem(int id)
    {
        return Items.Exists(item => item.Id == id);
    }

    public bool HasItem(string title)
    {
        return Items.Exists(item => item.Title == title);
    }

    public void RemoveItem(int id)
    {
        if (Items.Exists(item => item.Id == id))
        {
            Item item = Items.Find(item => item.Id == id);
            Items.Remove(item);
            Debug.Log("Item removed from player: " + item.Title);
        }
        else
        {
            Debug.Log("Item id: " + id + " Does not exist in character items");
        }
            
    }

    public void RemoveItem(string title)
    {
        if (Items.Exists(item => item.Title == title))
        {
            Item item = Items.Find(item => item.Title == title);
            Items.Remove(item);
            Debug.Log("Item removed from player: " + item.Title);
        }
        else
        {
            Debug.Log("Item: " + title + " Does not exist in character items");
        }
    }

    public void AddItem(int id)
    {
        Item item = itemDatabase.GetItem(id);
        if (item != null)
        {
            Items.Add(item);
            Debug.Log("Item added to player: " + item.Title);
        }
        else
        {
            Debug.Log("Item id: " + id + " Does not exist in item database");
        }
    }
    public void AddItem(string title)
    {
        Item item = itemDatabase.GetItem(title);
        if (item != null)
        {
            Items.Add(item);
            Debug.Log("Item added to player: " + item.Title);
        }
        else
        {
            Debug.Log("Item: " + title + " Does not exist in item database" );
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //AddItem(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
