using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public enum QteKind { Bar }
public class QTE : MonoBehaviour
{
    public QteKind kind;
    public RectTransform goodZone;
    public RectTransform player;
    public int playerSpeed;
    public Vector2 xPosition;
    public Vector2 playerZone;
    private float xPos;
    public Vector2 position;
    void Start()
    {
        switch (kind)
        {
            case QteKind.Bar:
                ZonePos(xPosition);
                goodZone.anchoredPosition = new Vector3(xPos, goodZone.anchoredPosition.y, 0);
                break;
            default: break;
        }
    }

    public void Update()
    {
        PlayerMoving();
    }

    private float ZonePos(Vector2 position)
    {
        return xPos = Random.Range(position.x, position.y);
    }

    private void PlayerMoving()
    {
        //einfache Links rechts bewegung
        //klappt noch nicht
        //kleiner größer zeichen fehler?
        position = player.anchoredPosition;
        if (position.x < playerZone.x && position.x >= playerZone.y)
        {
            position.x += playerSpeed * Time.deltaTime;
        }
        else if(position.x <= playerZone.y && position.x > playerZone.x)
        {
            position.x -= playerSpeed * Time.deltaTime;
        }
        player.anchoredPosition = position;
    }
}
