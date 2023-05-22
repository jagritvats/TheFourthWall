using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{

    public Rigidbody2D playerRigidBody;

    private bool jumpPressed = false;
    private float horizontalVelocity = 0f;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.HandleEscape();
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            gameManager.ToggleInventoryUI();
        }

        if (gameManager.isPaused())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) )
        {
            jumpPressed = true;
        }

        horizontalVelocity = Input.GetAxis("Horizontal");

        
    }

    private void FixedUpdate()
    {
        if (gameManager.isPaused())
        {
            return;
        }
        if ( jumpPressed )
        {
            playerRigidBody.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
            jumpPressed = false;
        }

        playerRigidBody.velocity = playerRigidBody.velocity + new Vector2(horizontalVelocity, 0) * Time.deltaTime * 6;
    }
}
