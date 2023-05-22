using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOverManager : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject levelComplete;
    // Start is called before the first frame update
    void Start()
    {
        levelComplete.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LevelComplete()
    {
        levelComplete.SetActive(true);
        int LevelNumber = SceneManager.GetActiveScene().buildIndex;
        //gameManager.LoadNextLevel();
        // showLevelComplete() => next Level, or Score Scene & game complete screen with credits-> main  menu
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.PauseGameActivity();
        Debug.Log(collision.name);
        if (collision.gameObject.tag == "Player")
        {
            LevelComplete();
        }
    }
}
