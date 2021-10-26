using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CollectibleItem : MonoBehaviour
{
    private Rigidbody rb;
    public Rigidbody Rb => rb;

    public int Id;

    public Item item;

    public ItemDatabase m_itemDatabase;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        item = m_itemDatabase.GetItem(Id);
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
