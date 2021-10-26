using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> Items = new List<Item>();

    void BuildDatabase()
    {
        // add different items here:
       Items.Add(new Item(0,"Flashlight", new List<Stats>
            {
                    
                new Stats{name = "Awesome", power = 9000}
            })
        );
    }


    public Item GetItem(int id)
    {
        return Items.Find(item => item.Id == id);
    }

    public Item GetItem(string title)
    {
        return Items.Find(item => item.Title == title);
    }
    // Awake is called when the game starts
    private void Awake()
    {
        //BuildDatabase();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
