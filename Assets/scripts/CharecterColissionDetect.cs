using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterColissionDetect : MonoBehaviour
{

    playerControl plControl;
    [HideInInspector] public Rigidbody rb;


    private void Start()
    {
        plControl = GetComponentInParent<playerControl>();
        rb = gameObject.GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && plControl.gameOver == false)
        {
            plControl.dirtParticles.Play();
            plControl.isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstackle") && plControl.gameOver == false)
        {
            GameOver();
        }

    }

    private void GameOver()
    {
        plControl.gameOver = true;
        Debug.Log("Game Over!");
        plControl.playerAnim.SetBool("Death_b", true);
        plControl.playerAnim.SetInteger("DeathType_int", 1);
        plControl.dirtParticles.Stop();
        plControl.explosionParticle.Play();
        plControl.music.Stop();
        plControl.playerSounds.PlayOneShot(plControl.crashSound, 1.0f);
        GameManager.scoreUpdate(System.DateTime.Now.ToString("hh:mm") + " " +
                                    System.DateTime.Now.ToString("MM/dd/yyyy") + ": " +
                                    Mathf.Round(plControl.Score * 100f) / 100f, plControl.Score);
        plControl.GameOver.SetActive(true);
        plControl.scoreTextEnd.text = Mathf.Round(plControl.Score * 100f) / 100f + "";
        GameManager.save();
    }
}
