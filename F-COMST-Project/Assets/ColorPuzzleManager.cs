using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorPuzzleManager : MonoBehaviour
{
    public GameObject Instance;
    
    public bool picked = false;
    public GameObject pickedObject;
    public Sprite BallColor1;
    public Sprite BallColor2;
    public Sprite BallColor3;

    private GameObject m_flask1;
    private GameObject m_flask2;
    private GameObject m_flask3;
    private GameObject m_flask4;

    private static bool isActive = false;
    private bool finished = false;
    private int[,] StartMap = new int[4, 3] { {3,3,0}, { 3, 1, 0}, { 1, 2, 1}, { 2, 2, 0} };
    private int[,] CurrentMap = new int[4, 3];
    public SuccessLightsource successLight;
    public SuccessLight successLightBody;
    private void Awake()
    {
        
        m_flask1 = GetChildComponentByName<Transform>("Flask1").gameObject;
        m_flask2 = GetChildComponentByName<Transform>("Flask2").gameObject;
        m_flask3 = GetChildComponentByName<Transform>("Flask3").gameObject;
        m_flask4 = GetChildComponentByName<Transform>("Flask4").gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(map[0, 1]);
        //Debug.Log(map[0, 0]);
        //Debug.Log(map[1, 0]);
        //Debug.Log(map[1, 1]);
        //Debug.Log(map[3, 1]);
        //StartPuzzle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPuzzle()
    {
        GenerateMap();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isActive = true;
        Instance.SetActive(true);
    }

    public void ExitPuzzle()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isActive = false;
        Instance.SetActive(false);
    }

    public void ResetPuzzle()
    {
        StartMap = new int[4, 3] { { 3, 3, 0 }, { 3, 1, 0 }, { 1, 2, 1 }, { 2, 2, 0 } };
        GenerateMap();
    }

    public static bool IsActive()
    {
        return isActive;
    }
    public bool IsFinished()
    {
        return finished;
    }
    public int GetColorOfPosition(int row, int column)
    {
        return CurrentMap[row, column];
    }

    public void UpdateColorOfPosition(int o_row, int o_column, int n_row, int n_column, int color)
    {
        CurrentMap[o_row, o_column] = 0;
        CurrentMap[n_row, n_column] = color;
        
    }

    private bool CheckCompletion()
    {
        for (int i = 0; i < 4; i++)
        {
            int value = GetColorOfPosition(i, 0);
            for (int j = 0; j < 3; j++)
            {
                if (GetColorOfPosition(i,j) != value)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public void RefreshMap()
    {
        for (int i = 0; i < 4; i++)
        {
            int amountOfBalls = 0;
            for (int j = 0; j < 3; j++)
            {
                amountOfBalls++;
                string parentName = "Flask" + (i + 1);
                string childName = "position" + j;
                if (CurrentMap[i, j] == 0)
                {
                    amountOfBalls--;
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).sprite = null;
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).color = new Color(1f, 1f, 1f, 0f);
                }
                else if (CurrentMap[i, j] == 1)
                {
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).sprite = BallColor1;
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).color = new Color(1f, 1f, 1f, 1f);
                }
                else if (CurrentMap[i, j] == 2)
                {
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).sprite = BallColor2;
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).color = new Color(1f, 1f, 1f, 1f);
                }
                else if (CurrentMap[i, j] == 3)
                {
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).sprite = BallColor3;
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).color = new Color(1f, 1f, 1f, 1f);
                }
                GetChildComponentByName<CP_OnClick>(parentName).row = i;
                GetChildComponentByName<CP_OnClick>(parentName).m_AmountOfBallse = amountOfBalls;
            }
        }
        if(CheckCompletion())
        {
            finished = true;
            GameManager.FinishPuzzleStatic(1);
            successLight.changecolorsuccess();
            successLightBody.lightsuccess();
            ExitPuzzle();
        }
    }

    private void GenerateMap()
    {
        
        for (int i = 0; i < 4; i++)
        {
            
            int amountOfBalls = 0;
            for (int j = 0; j < 3; j++)
            {
                amountOfBalls++;
                string parentName = "Flask" + (i+1);
                string childName = "position" + j;
                if (StartMap[i,j] == 0)
                {
                    amountOfBalls--;
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).sprite = null;
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).color = new Color(1f, 1f, 1f, 0f);
                }
                else if (StartMap[i, j] == 1)
                {
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).sprite = BallColor1;
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).color = new Color(1f, 1f, 1f, 1f);
                } else if (StartMap[i, j] == 2)
                {
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).sprite = BallColor2;
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).color = new Color(1f, 1f, 1f, 1f);
                } else if (StartMap[i, j] == 3)
                {
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).sprite = BallColor3;
                    GetChildComponentByName<UnityEngine.UI.Image>(parentName, childName).color = new Color(1f, 1f, 1f, 1f);
                }
                GetChildComponentByName<CP_OnClick>(parentName).row = i;
                GetChildComponentByName<CP_OnClick>(parentName).m_AmountOfBallse = amountOfBalls;
            }
        }
        CurrentMap = StartMap;
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

    private T GetChildComponentByName<T>(string parent, string name) where T : Component
    {
        foreach (T component in GetComponentsInChildren<T>(true))
        {
            if (component.gameObject.name == parent)
            {
                foreach (T componentChild in component.GetComponentsInChildren<T>(true))
                {
                    if (componentChild.gameObject.name == name)
                    {
                        return componentChild;
                    }
                }
            }
        }
        return null;
    }
}
