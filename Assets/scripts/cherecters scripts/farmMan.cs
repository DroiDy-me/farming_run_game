using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class farmMan : MonoBehaviour
{
    playerControl plControl;

    private void Start()
    {
        plControl = GameObject.Find("player").GetComponent<playerControl>();
        if (GameManager.charecterPicked == 1) getComponents();
        plControl.playerRb = gameObject.GetComponent<Rigidbody>();
        plControl.playerAnim = gameObject.GetComponent<Animator>();
    }

    protected void getComponents()
    {
        plControl.playerRb = gameObject.GetComponent<Rigidbody>();
        plControl.playerAnim = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && plControl.gameOver == false)
        {
            plControl.fall();
        }
        else if (collision.gameObject.CompareTag("Obstackle") && plControl.gameOver == false)
        {
            plControl.GameOver();
        }
    }
}
