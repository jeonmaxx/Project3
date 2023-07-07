using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public enum QteKind { Bar }
public class QTE : MonoBehaviour
{
    [SerializeField] private QteKind kind;
    [SerializeField] private RectTransform goodZone;
    [SerializeField] private RectTransform player;
    [SerializeField] private int playerSpeed;
    [SerializeField] private Vector2 xPosition;
    [SerializeField] private Vector2 playerZone;
    private float xPos;
    [SerializeField] private Vector2 position;
    public bool moving = true;
    private bool movingRight = true;
    [SerializeField] private PlayerQte playerQte;
    [SerializeField] private PlayerInput input;

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
        if(moving)
            PlayerMoving();
    }

    private float ZonePos(Vector2 position)
    {
        return xPos = Random.Range(position.x, position.y);
    }

    private void PlayerMoving()
    {
        //einfache Links rechts bewegung

        position = player.anchoredPosition;
        if (position.x <= playerZone.x)
        {
            movingRight = true;
        }
        else if (position.x >= playerZone.y)
        {
            movingRight = false;
        }

        if (movingRight)
        {
            position.x += playerSpeed * Time.deltaTime;
        }
        else if(!movingRight)
        {
            position.x -= playerSpeed * Time.deltaTime;
        }

        player.anchoredPosition = position;
    }

    private void OnQte()
    {
        if (moving)
            moving = false;
        //else if(!moving)
        //    moving = true;

        if (playerQte.greenZone) Debug.Log("Green Zone hit!");
        else if (playerQte.yellowZone) Debug.Log("Yellow Zone hit!");
        else Debug.Log("No Zone hit :(");
    }
}
