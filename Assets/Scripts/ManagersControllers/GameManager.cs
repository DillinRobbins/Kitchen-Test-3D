using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float timer = 180f;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject endGameUI;
    [SerializeField] GameObject pauseUI;

    public static UnityAction OnEndGame;

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;

        timerText.text = Mathf.FloorToInt(timer).ToString();

        if(timer <= 0)
        {
            OnEndGame?.Invoke();
            Time.timeScale = 0f;
            endGameUI.SetActive(true);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
    } 

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }

    public void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
