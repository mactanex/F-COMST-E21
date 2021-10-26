using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSystemClass : MonoBehaviour
{

    public Camera m_camera;
    // Reference to the slot for holding picked item.
    public Transform slot;

    public GameObject Crosshair;
    public Sprite RedCrossHair;
    public Sprite GreenCrossHair;
    // Reference to the currently held item.
    PickableItem pickedItem;
    
    // Start is called before the first frame update
    public Inventory inventory;

    public float distance = 10.0f;
    public float smooth = 5.0f;

    [Header("Flashlight")]
    public Light Flashlight;
    public GameObject FlashlightBody;

    private bool carrying = false;
    void Start()
    {
        Flashlight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = m_camera.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3f))
        {
            var pickable = hit.transform.GetComponent<PickableItem>();
            var collectable = hit.transform.GetComponent<CollectibleItem>();

            if (pickable ||collectable)
            {
                Crosshair.transform.GetComponent<UnityEngine.UI.Image>().sprite = GreenCrossHair;
            } else if(!carrying)
            {
                Crosshair.transform.GetComponent<UnityEngine.UI.Image>().sprite = RedCrossHair;
            }
        }
        else if (!carrying)
        {
            Crosshair.transform.GetComponent<UnityEngine.UI.Image>().sprite = RedCrossHair;
        }
        // Execute logic only on button pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check if player picked some item already
            if (pickedItem)
            {
                // If yes, drop picked item
                DropItem(pickedItem);
            }
            else
            {
                
                // If no, try to pick item in front of the player
                // Create ray from center of the screen
                //var ray = m_camera.ViewportPointToRay(Vector3.one * 0.5f);
                //RaycastHit hit;
                // Shot ray to find object to pick
                Debug.Log("Trying to pickup: " + Physics.Raycast(ray, out hit, 1.5f));
                if (Physics.Raycast(ray, out hit, 3f))
                {
                    // Check if object is pickable
                    var pickable = hit.transform.GetComponent<PickableItem>();
                    var collectable = hit.transform.GetComponent<CollectibleItem>();

                    // If object has PickableItem class
                    if (pickable)
                    {
                        // Pick it
                        PickItem(pickable);
                        carrying = true;
                    } 
                    if(collectable)
                    {
                        CollectItem(collectable);
                    }
                    // If object has collectibleItem class

                }
            }
        }

        if (inventory.HasItem(0))
        {
            FlashlightBody.GetComponent<MeshRenderer>().enabled = Flashlight.enabled;
            if (Input.GetKeyUp(KeyCode.F))
            {
                if (pickedItem)
                {
                    DropItem(pickedItem);
                }
                Flashlight.enabled = !Flashlight.enabled;
            }
        }

        if (carrying)
        {
            carryObject(pickedItem.gameObject);
        }
    }

    private void carryObject(GameObject o)
    {
        o.transform.position = Vector3.Lerp(o.transform.position, m_camera.transform.position + m_camera.transform.forward * distance, Time.deltaTime * smooth);
        o.transform.rotation = Quaternion.identity;
    }
    private void CollectItem(CollectibleItem item)
    {
        inventory.AddItem(item.Id);
        if (item.Id == 0)
        {
            item.Rb.isKinematic = true;
            item.Rb.velocity = Vector3.zero;
            item.Rb.angularVelocity = Vector3.zero;
            // Set Slot as a parent
            item.transform.SetParent(slot);
            var collider = item.GetComponentInChildren<MeshCollider>();
            collider.enabled = false;
            // Reset position and rotation
            item.transform.localPosition = Vector3.zero;
            item.transform.localEulerAngles = Vector3.zero;
        }else
        {
            item.gameObject.SetActive(false);
        }
            
        
    }
    private void PickItem(PickableItem item)
    {
        // Assign reference
        pickedItem = item;
        // Disable rigidbody and reset velocities
        item.Rb.useGravity = false;
        if(Flashlight.enabled)
        {
            Flashlight.enabled = false;
        }
        //item.Rb.velocity = Vector3.zero;
        //item.Rb.angularVelocity = Vector3.zero;
        // Set Slot as a parent
        //item.transform.SetParent(slot);
        // Reset position and rotation
        //item.transform.localPosition = Vector3.zero;
        //item.transform.localEulerAngles = Vector3.zero;
    }
    /// <summary>
    /// Method for dropping item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void DropItem(PickableItem item)
    {
        carrying = false;
        // Remove reference
        pickedItem = null;
        // Remove parent
        item.transform.SetParent(null);
        // Enable rigidbody
        item.Rb.useGravity = true;
        // Add force to throw item a little bit
        item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
        
    }
}
