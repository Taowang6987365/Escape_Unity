using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool canOpen;
    public GameObject[] doors;
    public GameObject Player;
    public GameObject cameraManager;

    private PlayerRay pr;
    private float minRange = 5.0f;//range to detect the door
    private bool isLevelLoaded;
    private string OpenDoorMessage;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject loadingScr;
    [SerializeField] private GameObject interactPannel;
    [SerializeField] private Transform startPos;
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;

    //Only one GameObject exsist in the game
    private static GameManager instance;
   
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        OpenDoorMessage = "Press E to Open Door";
        isLevelLoaded = true;

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(cameraManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLevelLoaded)
        {
            setUp();
            isLevelLoaded = false;
        }
        DoorOpenMsg();
    }

    /// <summary>
    /// Display the message when player close to the Door
    /// </summary>
    private void DoorOpenMsg()
    {
        if(pr.hit.collider != null)
        {
            if (pr.distance <= minRange && pr.hit.collider.gameObject.CompareTag("Door"))
            {
                interactPannel.SetActive(true);
                canOpen = true;
            }
            else
            {
                interactPannel.SetActive(false);
                canOpen = false;
            }
        }
    }

    public void Load_Scene()
    {
        GameObject go = pr.hit.collider.gameObject;
        Door_Choice(go);
    }

    public void Level_Load(int sceneIndex)
    {
        isLevelLoaded = true;
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScr.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }

        //detect if the scene is loaded or not
        isLevelLoaded = operation.isDone;
    }


    /// <summary>
    /// load next scene depend on the door player chooses
    /// </summary>
    /// <param name="go"></param>
    private void Door_Choice(GameObject go)
    {
        switch (go.name)
        {
            case "room":
                Display_Load(2);
                break;

            case "hall":
                Display_Load(1);
                break;

            case "upstairs":
                Display_Load(2);
                break;

            case "washroom":
                Display_Load(3);
                break;

            case "basement":
                Display_Load(4);
                break;

            default:
                break;
        }
  
    }

    private void Display_Load(int loadIndex)
    {
        text.text = OpenDoorMessage;
        Level_Load(loadIndex);
    }

    /// <summary>
    /// set up all the DontDestroyOnLoad gameobject status
    /// </summary>
    public void setUp()
    {
        startPos = GameObject.FindGameObjectWithTag("startPos").transform;
        pr = GameObject.Find("Main Camera").GetComponent<PlayerRay>();
        //Find all the doors in the scene
        doors = GameObject.FindGameObjectsWithTag("Door");
        canvas = GameObject.FindGameObjectWithTag("canvas");
        if(canvas != null)
        {
            loadingScr = canvas.transform.GetChild(1).gameObject;
            slider = loadingScr.transform.GetChild(0).gameObject.GetComponent<Slider>();
            interactPannel = canvas.transform.GetChild(0).gameObject;
            text = interactPannel.transform.GetChild(0).gameObject.GetComponent<Text>();
        }
        
        //set player start position
        Player.transform.position = startPos.position;
        Player.transform.rotation = startPos.rotation;
    }
}
