using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzleManager : MonoBehaviour
{
    public GameObject Instance;
    
    public bool picked = false;
    public Sprite BallColor1;
    public Sprite BallColor2;
    public Sprite BallColor3;

    private GameObject m_flask1;
    private GameObject m_flask2;
    private GameObject m_flask3;
    private GameObject m_flask4;

    private static bool isActive = false;

    int[,] StartMap = new int[4, 3] { {1,2,0}, { 1, 2, 0}, { 1, 2, 0}, { 1, 2, 3} };
    int[,] CurrentMap = new int[4, 3];
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
        StartPuzzle();
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
        GenerateMap();
    }

    public static bool IsActive()
    {
        return isActive;
    }

    private void GenerateMap()
    {
        
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                string parentName = "Flask" + (i+1);
                string childName = "position" + j;
                if (StartMap[i,j] == 0)
                {
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
