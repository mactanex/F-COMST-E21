using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CP_OnClick : MonoBehaviour, IPointerClickHandler
{
    public bool Clicked = false;
    private GameObject _instance;
    private Vector3 OriginalPosition;
    private Quaternion originalRotation;
    private ColorPuzzleManager m_ColorPuzzleManager;
    public int m_AmountOfBallse = 3;
    public int row;
    private GameObject ball;
    private Vector3 ballOldPosition;
    //public GameObject[] balls { get; private set; } = new GameObject[3] ;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked on object: " + _instance.name);
        if (Clicked)
        {
            Clicked = false;
            m_ColorPuzzleManager.picked = false;
            m_ColorPuzzleManager.pickedObject = null;
            GoBack();
            AudioManager.Play("ColorFailure");

        } else if (!m_ColorPuzzleManager.picked)
        {
            Debug.Log("Setting to true");
            Clicked = true;
            _instance.transform.SetAsFirstSibling();
            m_ColorPuzzleManager.picked = true;
            m_ColorPuzzleManager.pickedObject = this.transform.gameObject;
        } else if (m_ColorPuzzleManager.picked )
        {
            CP_OnClick target = m_ColorPuzzleManager.pickedObject.GetComponent<CP_OnClick>();
            if(m_AmountOfBallse < 3)
            {
                Debug.Log(m_AmountOfBallse + ", " + target.m_AmountOfBallse);
                if (m_ColorPuzzleManager.GetColorOfPosition(row, m_AmountOfBallse - 1) == m_ColorPuzzleManager.GetColorOfPosition(target.row, target.m_AmountOfBallse - 1))
                {
                    string temp = "position" + (m_ColorPuzzleManager.pickedObject.GetComponent<CP_OnClick>().m_AmountOfBallse - 1);
                    ball = m_ColorPuzzleManager.pickedObject.GetComponent<CP_OnClick>().GetChildComponentByName<Transform>(temp).gameObject;
                    iTween.MoveTo(m_ColorPuzzleManager.pickedObject, new Vector3(this.transform.position.x - 150, this.transform.position.y + 200, this.transform.position.z), 1);
                    
                    
                    iTween.MoveBy(ball, iTween.Hash("y", 100f,
                      "delay", 0.1f,
                      "islocal", true,
                      "time", 1f,
                      "easeType", "linear",
                      "onComplete", "BallGoBack",
                      "oncompletetarget", gameObject));

                    iTween.RotateBy(m_ColorPuzzleManager.pickedObject, iTween.Hash(
                      "z", -0.35f,
                      "delay", 0.1f,
                      "time", 1f,
                      "easeType", "linear",
                      "onComplete", "GoBackSuccess"
                  ));

                    AudioManager.Play("ColorSuccess");
                    Clicked = false;
                    m_ColorPuzzleManager.pickedObject.GetComponent<CP_OnClick>().Clicked = false;
                    m_ColorPuzzleManager.picked = false;
                    m_ColorPuzzleManager.pickedObject = null;
                    Debug.Log("Should animate and put balls in object: " + this.transform.name);
     
                    m_ColorPuzzleManager.UpdateColorOfPosition(target.row, target.m_AmountOfBallse - 1, row, m_AmountOfBallse, m_ColorPuzzleManager.GetColorOfPosition(target.row, target.m_AmountOfBallse - 1));
                    
                }
                else
                {
                    AudioManager.Play("ColorFailure");
                    Clicked = false;
                    m_ColorPuzzleManager.picked = false;
                    m_ColorPuzzleManager.pickedObject.GetComponent<CP_OnClick>().Clicked = false;
                    m_ColorPuzzleManager.pickedObject.GetComponent<CP_OnClick>().GoBack();
                    m_ColorPuzzleManager.pickedObject = null;
                    GoBack();
                }
            }
            else
            {
                AudioManager.Play("ColorFailure");
                Clicked = false;
                m_ColorPuzzleManager.picked = false;
                m_ColorPuzzleManager.pickedObject.GetComponent<CP_OnClick>().Clicked = false;
                m_ColorPuzzleManager.pickedObject.GetComponent<CP_OnClick>().GoBack();
                m_ColorPuzzleManager.pickedObject = null;
                GoBack();
            }



        }
    }

    void Awake()
    {
        _instance = gameObject;
        OriginalPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        originalRotation = gameObject.transform.rotation;
        m_ColorPuzzleManager = this.GetComponentInParent<ColorPuzzleManager>();

        //balls[0] = (GameObject)GetChildComponentByName<Transform>("position0").gameObject;
        //balls[1] = GetChildComponentByName<Transform>("position1").gameObject;
        //balls[2] = GetChildComponentByName<Transform>("position2").gameObject;
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
        } 
        else
        {
            
        }
        
    }

    public void GoBack()
    {
        iTween.MoveTo(_instance, OriginalPosition, 1);
        iTween.RotateTo(_instance, originalRotation.eulerAngles, 0.1f);
        Clicked = false;
    }

    public void BallGoBack()
    {
        iTween.MoveBy(ball, iTween.Hash("y", -100f,
                      "x",-6f,
                      "delay", 0.01f,
                      "islocal", false,
                      "time", 0.01f,
                      "easeType", "linear"));
    }

    public void GoBackSuccess()
    {
        iTween.MoveTo(_instance, OriginalPosition, 1);
        iTween.RotateTo(_instance, originalRotation.eulerAngles, 0.1f);
        Clicked = false;
        m_ColorPuzzleManager.RefreshMap();
    }
    private T GetChildComponentByName<T>(string name) where T : Component
    {
        foreach (T component in GetComponentsInChildren<T>(true))
        {
            if (component.gameObject.name == name)
            {
                return component;
            }
        }
        return null;
    }
}
