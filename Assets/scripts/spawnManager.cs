using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePref;

    private playerControl playercontrol;

    float timer = 3;

    private Vector3 spawnPos = new Vector3(25,0,0);


    // Start is called before the first frame update
    void Start()
    {
        playercontrol = GameObject.Find("player").GetComponent<playerControl>();
    }

    void spawnObastackle()
    {
       if(!playercontrol.gameOver)
        {
            int index = Random.Range(0, obstaclePref.Length);
            Instantiate(obstaclePref[index], spawnPos, obstaclePref[index].transform.rotation);
        }

        timer += Random.Range(0.8f, 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timer)
        {
            spawnObastackle();
        }
    }
}
