using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CoinController : MonoBehaviour
{
    public AudioClip AudioClip = null;
    public AudioSource coinClip;
    public GameManager manager;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update

    public static string COIN_TEMPLATE_ID = "0e7f37bd-7075-4cc1-a927-935ed9cd51a7";

    void Start()
    {
        coinClip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ReneController.MintCoin();
            Debug.Log("Coin Collected");
            GetComponent<AudioSource>().PlayOneShot(AudioClip);
            spriteRenderer.enabled = false;
            manager.addScore(1);
            Destroy(gameObject, AudioClip.length);
        }
       
    }
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }*/
}
