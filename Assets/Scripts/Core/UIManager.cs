using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject TitleScreenObject;
    public GameObject OptionsMenuObject;
    public GameObject PauseMenuObject;
    public GameObject GameOverObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideTitleScreenUI()
    {
        TitleScreenObject.SetActive(false);
    }

    public void ShowTitleScreenUI()
    {
        TitleScreenObject.SetActive(true);
    }

    public void ShowPauseMenu()
    {

    }

    public void HidePauseMenu()
    {

    }

    public void ShowOptionsMenu()
    {
        OptionsMenuObject.SetActive(true);
    }

    public void HideOptionsMenu()
    {

    }

    public void ShowGameOver()
    {

    }

    public void HideGameOver()
    {

    }
}
