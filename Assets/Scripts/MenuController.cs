using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    int CurrentSceneIndex;

    public TMP_Text reneverseConnectionStatus;
    public TMP_InputField emailInput;

    void Start()
    {
        
        if(ReneController.userConnected)
        {
            StartCoroutine(CheckForRenev());
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void OnReneverseEmailSubmit()
    {
        if (!emailInput)
        {
            return;
        }
        if (emailInput.text.Length == 0)
        {
            emailInput.placeholder.GetComponent<TMP_Text>().text = "Enter Email First!";
            return;
        }
        string email = emailInput.text;
        initiateReneverseConnection(email);
        StartCoroutine(CheckForRenev());
    }

    bool IsUserConnected()
    {
        return ReneController.userConnected;
    }

    private IEnumerator CheckForRenev()
    {
       
            yield return new WaitUntil(IsUserConnected);
        reneverseConnectionStatus.text = "Connected Succesfully!";
        
    }

    private void initiateReneverseConnection(string email)
    {
        reneverseConnectionStatus.text = "Connecting to Reneverse...";
        ReneController.ConnectToReneverse(email);
    }



    public void StartGame()
    {
        Debug.Log("Starting");
        SceneManager.LoadScene(CurrentSceneIndex + 1); // load next scene
    }

    public void InitiateQuit()
    {

        // confirm quit
        Application.Quit();
    }
}
