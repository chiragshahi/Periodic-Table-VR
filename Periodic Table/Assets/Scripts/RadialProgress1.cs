using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//using vh = VirtualHand;

public class RadialProgress1 : MonoBehaviour
{
    public GameObject LoadingText;
    public Text ProgressIndicator;
    public Image LoadingBar;
    float currentValue = 0;
    public float speed;
    public VirtualHand vh;
    public VirtualHand vh2;

    //VirtualHand.VirtualHandState state;

    public int currentLevel = 0;
    public string[] levelNames = new string[2] { "Quiz Room", "Periodic Table" };

    private bool isTouching = false;

    private void OnTriggerEnter(Collider other)
    {
        isTouching = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isTouching = false;
    }

    // Use this for initialization
    void Start()
    {
        LoadingText.SetActive(true);
    }

    // Update is called once per frame
    void OnTriggerStay()
    {
        if (VirtualHand.VirtualHandState.Touching == vh.state && currentValue < 100 && isTouching)
        {
            currentValue += speed * Time.deltaTime;
            ProgressIndicator.text = ((int)currentValue).ToString() + "%";
            LoadingText.SetActive(true);
        }

        else if (VirtualHand.VirtualHandState.Touching == vh2.state && currentValue < 100 && isTouching)
        {
            currentValue += speed * Time.deltaTime;
            ProgressIndicator.text = ((int)currentValue).ToString() + "%";
            LoadingText.SetActive(true);
        }

        else if (VirtualHand.VirtualHandState.Touching == vh.state && currentValue >= 100 && isTouching)
        {
            LoadingText.SetActive(false);
            ProgressIndicator.text = "Let's Play";
            currentLevel = (currentLevel + 1) % 2;
            SceneManager.LoadScene(currentLevel);
            //SteamVR_LoadLevel.Begin(levelNames[currentLevel]);
        }

        else if (VirtualHand.VirtualHandState.Touching == vh2.state && currentValue >= 100 && isTouching)
        {
            LoadingText.SetActive(false);
            ProgressIndicator.text = "Let's Play";
            currentLevel = (currentLevel + 1) % 2;
            SceneManager.LoadScene(currentLevel);
            //SteamVR_LoadLevel.Begin(levelNames[currentLevel]);
        }
    }

    private void Update()
    {
        LoadingBar.fillAmount = currentValue / 100;
    }
}