using UnityEngine.EventSystems;
using UnityEngine;
using System;
using System.Collections;

public class clickOnCharecterHandler : MonoBehaviour, IPointerClickHandler
{
    public int charecterID;
    public float rotationSpeed = 1;
    [SerializeField] private GameObject pickedMarker;

    void Start()
    {
        sphereChangePos();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.charIndPos = new Vector3(transform.position.x,2.4f, -3f);
        sphereChangePos();
        GameManager.charecterPicked = charecterID;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }

    private void sphereChangePos()
    {
        pickedMarker.transform.position = GameManager.charIndPos;
    }
}
