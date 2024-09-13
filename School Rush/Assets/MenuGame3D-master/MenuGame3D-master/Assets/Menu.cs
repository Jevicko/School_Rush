using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject panel;
    public GameObject AboutPanel; 
    public GameObject SettingPanel;
    public GameObject ExitPanel;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(true);
        AboutPanel.SetActive(false);
        SettingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void SettingButton() 
    {
        panel.SetActive(false);
        AboutPanel.SetActive(false);
        SettingPanel.SetActive(true);
        ExitPanel.SetActive(false);
    }

    public void AboutButton()
    {
        panel.SetActive(false);
        AboutPanel.SetActive(true);
        SettingPanel.SetActive(false);
        ExitPanel.SetActive(false);
    }    

    public void QuitButton()
    {
        panel.SetActive(true);
        AboutPanel.SetActive(false);
        SettingPanel.SetActive(false);
        ExitPanel.SetActive(false);
    }

    public void ExitButton()
    {
        panel.SetActive(false);
        AboutPanel.SetActive(false);
        SettingPanel.SetActive(false);
        ExitPanel.SetActive(true);
    }

    public void OutGame()
    {
        Application.Quit();
        Debug.Log("Tombol Keluar Telah Ditekan!...");
    }
}
