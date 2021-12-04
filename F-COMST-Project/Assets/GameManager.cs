using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject LowerLeftN;
    public GameObject LowerRightN;
    public GameObject HigherLeftN;
    public GameObject HigherRightN;

    public ExplodingCow explodingCow;
    public GameObject GHOUL;
    public int seconds = 0;
    int i = 0;
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

        //DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Play("music");
        StartCoroutine(time());
    }

    // Update is called once per frame
    void Update()
    {

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
                StartCoroutine(FinishPuzzle2());
                break;
            case 3:
                HigherLeftN.SetActive(true);
                AudioManager.Play("Success");
                break;
            case 4:
                StartCoroutine(SuccesFinalAnim());
                
                break;
            default:
                break;
        }
        i++;
        if (Instance.i == 1)
        {
            GHOUL.SetActive(true);
            AudioManager.DelayAndPlay("Ghoul spawn", 5f);
        }
        if (Instance.i == 3)
        {
            AudioManager.DelayAndPlay("MOOO",2.5f);
            Instance.explodingCow.EnableCow();
        }
        if(Instance.i == 4)
        {
            GHOUL.SetActive(false);
            StartCoroutine(Finish());
        }
    }

    public static void FinishPuzzleStatic(int key)
    {
        Instance.FinishPuzzle(key);
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

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    IEnumerator FinishPuzzle2()
    {
        yield return new WaitForSeconds(2.5f);
        LowerRightN.SetActive(true);
        AudioManager.Play("Success");
    }

    IEnumerator SuccesFinalAnim()
    {
        yield return new WaitForSeconds(1.5f);
        HigherRightN.SetActive(true);
        AudioManager.Play("SuccessFinal");
    }
    void timeCount()
    {
        seconds += 1;
    }

}
