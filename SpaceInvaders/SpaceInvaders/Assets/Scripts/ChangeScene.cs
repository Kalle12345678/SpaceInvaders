using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
  public void PlayGame()
    {
        print("Play knappen funkar");
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        print("Quit knappen funkar");
        Application.Quit();
    }
}