using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    public GameObject menuOverlay;
    public TMPro.TMP_Text message;

    public void Awake()
    {
        Time.timeScale = 1;
    }

    public void ShowStageCleared()
    {
        Time.timeScale = 0;
        menuOverlay.SetActive(true);
        message.SetText("Stage Cleared!");
        StartCoroutine(LoadMainMenu(5f));
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0;
        menuOverlay.SetActive(true);
        message.SetText("You died...");
        StartCoroutine(LoadMainMenu(3f));
    }

    protected IEnumerator LoadMainMenu(float fadeSeconds)
    {
        yield return new WaitForSecondsRealtime(fadeSeconds);
        SceneManager.LoadScene("MainMenu");
    }
}