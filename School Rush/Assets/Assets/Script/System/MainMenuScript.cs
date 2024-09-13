using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
   public void StartButton()
   {
       SceneManager.LoadScene("Home");
   }

   public void ExitButton()
   {
        Application.Quit();
        Debug.Log("Quit Game!");
   }
}