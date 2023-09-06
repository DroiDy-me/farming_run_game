using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundRepeat : MonoBehaviour
{
    private Vector3 startingPos;
    private float repeatWidth;

    [SerializeField] private Sprite city;
    [SerializeField] private Sprite town;
    [SerializeField] private Sprite nature;

    private SpriteRenderer img;

    // Start is called before the first frame update
    void Start()
    {
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        startingPos = transform.position;
        img = GetComponent<SpriteRenderer>();
        switch (GameManager.mapPicked) 
        {
            case 1:
                img.sprite = city;
                break;
            case 2:
                img.sprite = town;
                break;
            case 3:
                img.sprite = nature;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startingPos.x - repeatWidth)
        {
            transform.position = startingPos;
        }
    }
}
