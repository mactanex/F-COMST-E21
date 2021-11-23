using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CP_OnClick : MonoBehaviour, IPointerClickHandler
{
    private bool Clicked = false;
    private GameObject _instance;
    private Vector3 OriginalPosition;
    private ColorPuzzleManager m_ColorPuzzleManager;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked on object: " + _instance.name);
        if (Clicked)
        {
            iTween.MoveTo(_instance, OriginalPosition, 1);
            Clicked = false;
            m_ColorPuzzleManager.picked = false;

        } else if (!m_ColorPuzzleManager.picked)
        {
            Clicked = true;
            _instance.transform.SetAsFirstSibling();
            m_ColorPuzzleManager.picked = true;
        }
    }

    void Awake()
    {
        _instance = gameObject;
        OriginalPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        m_ColorPuzzleManager = this.GetComponentInParent<ColorPuzzleManager>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Clicked)
        {
            this.transform.position = Input.mousePosition;
        } else
        {
            
        }
        
    }
}
