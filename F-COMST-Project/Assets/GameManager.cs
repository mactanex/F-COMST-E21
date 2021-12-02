using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject LowerLeftN;
    public GameObject LowerRightN;
    public GameObject HigherLeftN;
    public GameObject HigherRightN;

    public int seconds = 0;
    int i = 1;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
        // Start is called before the first frame update
        void Start()
    {
        //AudioManager.Play("music");
        StartCoroutine(time());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            
            FinishPuzzle(i);
                
        }
    }

    public void FinishPuzzle(int key)
    {
        switch (key)
        {
            case 1:
                LowerLeftN.SetActive(true);
                AudioManager.Play("Success");
                break;
            case 2:
                LowerRightN.SetActive(true);
                AudioManager.Play("Success");
                break;
            case 3:
                HigherLeftN.SetActive(true);
                AudioManager.Play("Success");
                break;
            case 4:
                HigherRightN.SetActive(true);
                AudioManager.Play("SuccessFinal");
                break;
            default:
                break;
        }
        i++;
    }
    public static int GetSeconds()
    {
        return Instance.seconds;
    }

    IEnumerator time()
    {
        while (true)
        {
            timeCount();
            yield return new WaitForSeconds(1);
        }
    }
    void timeCount()
    {
        seconds += 1;
    }

    public static bool ActivateCow()
    {
        if (Instance.i == 3)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
