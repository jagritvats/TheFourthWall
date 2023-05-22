using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingController : MonoBehaviour
{

    [SerializeField]GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision occured");
        if (collision.gameObject.tag == "Player")
        {

            gameManager.RestartLevel();
        }
    }
}
