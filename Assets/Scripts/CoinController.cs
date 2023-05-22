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
        Debug.Log("Coin Collected");
        GetComponent<AudioSource>().PlayOneShot(AudioClip);
        spriteRenderer.enabled = false;
        manager.addScore(1);
        Destroy(gameObject, AudioClip.length);
       
    }
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }*/
}