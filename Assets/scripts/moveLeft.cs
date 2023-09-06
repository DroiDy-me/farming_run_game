using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLeft : MonoBehaviour
{

    private float leftBound = -15;

    private playerControl playercontrol;

    // Start is called before the first frame update
    void Start()
    {
        playercontrol = GameObject.Find("player").GetComponent<playerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playercontrol.gameOver == false)
        {
            transform.Translate(Vector3.left * playercontrol.speed * Time.deltaTime);
        }

        if(transform.position.x < leftBound && CompareTag("Obstackle"))
        {
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !playercontrol.gameOver)
        {
            playercontrol.speed = 20;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playercontrol.speed = 10;
        }
    }
}
