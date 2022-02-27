using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnStartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}
