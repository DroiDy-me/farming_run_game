using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPickScript : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    private RectTransform indPos;

    private void Start()
    {
        indPos = indicator.GetComponent<RectTransform>();
        indChangePos(GameManager.mapIndPos);
    }

    public void cityPicked()
    {
        GameManager.mapPicked = 1;
        GameManager.mapPick();
        indChangePos(GameManager.mapIndPos);
    }

    public void townPicked()
    {
        GameManager.mapPicked = 2;
        GameManager.mapPick();
        indChangePos(GameManager.mapIndPos);
    }

    public void naturePicked()
    {
        GameManager.mapPicked = 3;
        GameManager.mapPick();
        indChangePos(GameManager.mapIndPos);
    }

    private void indChangePos(float pos)
    {
        indPos.anchoredPosition = new Vector2(-100, pos);
    }
}
