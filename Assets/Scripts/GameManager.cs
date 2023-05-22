using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static bool paused = false;
    private bool overlayVisible = false;
    private int score = 0;
    private GameObject currentOverlay = null;

    [SerializeField] private GameObject PausedUI;
    [SerializeField] private GameObject ScoreUI;
    [SerializeField] private GameObject InventoryUI;
    public TMP_Text scoreCounter;
    public InventoryManager inventoryManager;


    // Lifecycle
    private void Start()
    {
        StartGame();
    }

    public bool isPaused () { return paused; }

    private void pauseGame() { paused = true; }

    public void addScore(int addition)
    {
        score += addition;
        scoreCounter.text = score.ToString();
        inventoryManager.AddCoins(addition);
        Debug.Log(score);
    }

    public void Reset()
    {
        paused = false;
        score = 0;
        Time.timeScale = 1f;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGame()
    {
        Reset();
    }

    // Game Pause Behaviors

    private void ShowPausedMenu()
    {
        if (PausedUI && !overlayVisible) {
            overlayVisible = true;
            currentOverlay = PausedUI;
            
            PausedUI.SetActive(true);
        }
    }

    private void HidePausedMenu()
    {
        if (PausedUI && overlayVisible)
        {
            overlayVisible = false;
            currentOverlay = null;
            PausedUI.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        if (!paused) return;
        HidePausedMenu();
        ResumeGameActivity();
    }

    private void ResumeGameActivity()
    {
        paused = false;
        Time.timeScale = 1f;
    }
    public void PauseGameActivity()
    {
        paused = true;
        Time.timeScale = 0f;
    }
    public void PauseGame()
    {
        if (isPaused()) return;
        PauseGameActivity();
        ShowPausedMenu();
    }

    public void ExitInventory()
    {
        if (InventoryUI)
        {
            overlayVisible = false;
            currentOverlay = null;
            InventoryUI.SetActive(false);
            ResumeGameActivity();
        }
    }

    public void ShowInventoryUI()
    {
        if (overlayVisible)
        {
            Debug.Log("Will not override overlay!");
            return;
        }
        if(InventoryUI)
        {
            PauseGameActivity();
            overlayVisible = true;
            currentOverlay = InventoryUI;
            InventoryUI.SetActive(true);
        }
    }

    public void ToggleInventoryUI()
    {
        if(currentOverlay == InventoryUI)
        {
            ExitInventory();
        }
        else if(currentOverlay == null)
        {
            ShowInventoryUI();
        }
        else
        {
            Debug.Log("multiple overlays prevented!");
        }
    }

    public void HandleEscape()
    {

        if (overlayVisible)
        {
            if(currentOverlay == InventoryUI)
            {
                ExitInventory();
             
            }else if (currentOverlay == PausedUI)
            {
                ResumeGame();
            }
            return;
        }

      PauseGame();
    }

    // Return to menu

    public void ReturnToMainMenu()
    {
        PauseGame();
        SceneManager.LoadScene(0);
    }
}
