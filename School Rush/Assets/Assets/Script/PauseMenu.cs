using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject PanelMenu;
    public GameObject AboutPanel; 
    public GameObject SettingPanel;
    public GameObject ExitPanel;
    //public GameObject QuitButton;
    //public GameObject QuitButton;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void SettingButton() 
    {
        PanelMenu.SetActive(false);
        AboutPanel.SetActive(false);
        SettingPanel.SetActive(true);
        ExitPanel.SetActive(false);
    }

    public void AboutButton()
    {
        PanelMenu.SetActive(false);
        AboutPanel.SetActive(true);
        SettingPanel.SetActive(false);
        ExitPanel.SetActive(false);
    }

    public void QuitButton()
    {
        PanelMenu.SetActive(true);
        AboutPanel.SetActive(false);
        SettingPanel.SetActive(false);
        ExitPanel.SetActive(false);
    }

    public void ExitButton()
    {
        PanelMenu.SetActive(false);
        AboutPanel.SetActive(false);
        SettingPanel.SetActive(false);
        ExitPanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("menu 3d");
    }
}
