using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicVolume : MonoBehaviour
{
    private AudioSource music;
    [SerializeField] AudioClip[] musicList;

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        music.clip = musicList[Random.Range(0,musicList.Length)];
        music.Play();
    }

    private void Update()
    {
        music.volume = GameManager.volume;
    }
}
