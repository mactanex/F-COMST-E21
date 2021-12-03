using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TooltipSystem : MonoBehaviour
{
    public  TextMeshProUGUI Tooltip;
    public static TooltipSystem instance;
    private bool toolchipCheck;
    // Start is called before the first frame update

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Tooltip.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void MaskDespawnMessage(float time, string txt)
    {
        instance.SetTooltipWithTimer(time, txt);
    }
    public void SetTooltipText(string text)
    {
        Tooltip.text = text;
    }

    public void EnableTooltip()
    {
        Tooltip.enabled = true;
    }

    public void EnableTooltip(string text)
    {
        Tooltip.enabled = true;
        Tooltip.text = text;
    }

    public void DisableTooltip()
    {
        if(!toolchipCheck)
            Tooltip.enabled = false;
    }

    public void DisableTooltip(string text)
    {
        if (!toolchipCheck)
        {
            Tooltip.text = text;
            Tooltip.enabled = false;
        }
            
    }

    public void SetTooltipWithTimer(float time, string text)
    {
        Tooltip.text = text;
        StartCoroutine(TooltipDelayedDestruction(time));
    }
    private IEnumerator TooltipDelayedDestruction(float time)
    {
        Tooltip.enabled = true;
        toolchipCheck = true;
        yield return new WaitForSeconds(time);
        Tooltip.enabled = false;
        toolchipCheck = false;
        //yield break;
    }
}
