using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowcaseUI : MonoBehaviour
{
    // Public Variables
    public GameObject showcaseUI;
    public Button showcaseButton1, showcaseButton2;
    // Private Variables

    // Start is called before the first frame update
    void Start()
    {
        showcaseButton1.onClick.AddListener(() => switchScenes(1));
        showcaseButton2.onClick.AddListener(() => switchScenes(2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Onclick function to turn UI on and off
    public void activateUI()
    {
        if (!showcaseUI.activeSelf)
        {
            showcaseUI.SetActive(true);
        } else
        {
            showcaseUI.SetActive(false);
        }
    }

    // Scene transition
    public void switchScenes(int number)
    {
        switch (number)
        {
            case 1:
                SceneManager.LoadScene("Scroller_1_1");
                break;
            case 2:
                SceneManager.LoadScene("Scroller_3_1");
                break;
            default:
                SceneManager.LoadScene("Scroller_1_1");
                break;
        }
    }
}
