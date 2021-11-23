using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GrabSystemClass : MonoBehaviour
{

    public Camera m_camera;
    // Reference to the slot for holding picked item.
    public Transform slot;

    public GameObject Crosshair;
    public GameObject FlashlightBorderChild;
    public Sprite RedCrossHair;
    public Sprite GreenCrossHair;

    public TooltipSystem TooltipSystem;
    // Reference to the currently held item.
    PickableItem pickedItem;
    PickablePuzzleItem pickedPuzzleItem;
    
    // Start is called before the first frame update
    public Inventory inventory;

    public float distance = 10.0f;
    public float smooth = 5.0f;

    [Header("Flashlight")]
    public Light Flashlight;
    public GameObject FlashlightBody;

    private bool carrying = false;
    private bool firstTimePickup = true;
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
            var pickablePuzzle = hit.transform.GetComponent<PickablePuzzleItem>();

            if (pickable ||collectable || pickablePuzzle)
            {
                Crosshair.transform.GetComponent<UnityEngine.UI.Image>().sprite = GreenCrossHair;
                if(!carrying)
                {
                    TooltipSystem.EnableTooltip("Press E to pickup");
                } else
                {
                    TooltipSystem.EnableTooltip();  
                }
                

                    
            } else if(!carrying)
            {
                Crosshair.transform.GetComponent<UnityEngine.UI.Image>().sprite = RedCrossHair;
                TooltipSystem.DisableTooltip("Press E to pickup");

            }
        }
        else if (!carrying)
        {
            Crosshair.transform.GetComponent<UnityEngine.UI.Image>().sprite = RedCrossHair;
            TooltipSystem.DisableTooltip("Press E to pickup");
            

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
            if(pickedPuzzleItem)
            {
                DropPuzzleItem(pickedPuzzleItem);
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
                    var pickablePuzzle = hit.transform.GetComponent<PickablePuzzleItem>();

                    // If object has PickableItem class
                    if (pickable)
                    {
                        // Pick it
                        PickItem(pickable);
                        carrying = true;
                        TooltipSystem.SetTooltipText("Press E to throw");
                    } 
                    if(collectable)
                    {
                        CollectItem(collectable);
                    }
                    // If object has collectibleItem class
                    if (pickablePuzzle)
                    {
                        PickPuzzleItem(pickablePuzzle);
                        carrying = true;
                        TooltipSystem.SetTooltipText("Press E to let go");
                    }

                }
            }
        }

        if (inventory.HasItem(0))
        {
            if (firstTimePickup)
            {
                TooltipSystem.SetTooltipWithTimer(2f, "Press F to use flashligt");
                FlashlightBorderChild.transform.GetComponent<UnityEngine.UI.Image>().sprite = inventory.GetItem(0).Icon;
                FlashlightBorderChild.SetActive(true);
                firstTimePickup = false;
            }
            FlashlightBody.GetComponent<MeshRenderer>().enabled = Flashlight.enabled;
            if (Input.GetKeyUp(KeyCode.F))
            {
                if (pickedItem)
                {
                    DropItem(pickedItem);
                }
                if (pickedPuzzleItem)
                {
                    DropPuzzleItem(pickedPuzzleItem);
                }
                Flashlight.enabled = !Flashlight.enabled;
            }
        }

        if (carrying)
        {
            if (pickedItem)
            {
                carryObject(pickedItem.gameObject);
            }
            if(pickedPuzzleItem)
            {
                carryObject(pickedPuzzleItem.gameObject);
            }

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
    /// 

    private void PickPuzzleItem(PickablePuzzleItem puzzleItem)
    {
        pickedPuzzleItem = puzzleItem;

        puzzleItem.Rb.useGravity = false;

        if (Flashlight.enabled)
        {
            Flashlight.enabled = false;
        }
    }
    private void DropItem(PickableItem item)
    {
        TooltipSystem.SetTooltipText("Press E to pickup");
        carrying = false;
        // Remove reference
        pickedItem = null;
        // Remove parent
        item.transform.SetParent(null);
        // Enable rigidbody
        item.Rb.useGravity = true;
        // Add force to throw item a little bit
        item.Rb.AddForce(transform.forward * 1, ForceMode.VelocityChange);
        
    }

    private void DropPuzzleItem(PickablePuzzleItem puzzleItem)
    {
        TooltipSystem.SetTooltipText("Press E to pickup");
        carrying = false;
        pickedPuzzleItem = null;
        puzzleItem.Rb.useGravity = true;

        if(puzzleItem.gameObject.tag == "puzzle3")
        {
            Debug.Log("Mask puzzle piece");
        }
    }
}
