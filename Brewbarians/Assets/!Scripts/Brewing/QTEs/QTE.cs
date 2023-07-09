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
    public bool moving = false;
    private bool movingRight = true;
    public bool done = false;
    [SerializeField] private PlayerQte playerQte;
    [SerializeField] private PlayerInput input;

    public BrewingManager manager;

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

        transform.localScale = Vector3.zero;
        LeanTween.init(1600);
    }

    public void Update()
    {
        if (moving)
        {
            transform.localScale = Vector3.one;
            PlayerMoving();
        }
        else
            transform.LeanScale(Vector3.zero, 0.5f).setEaseOutExpo();
    }

    public void QteMethode()
    {
        moving = true;
        done = false;
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

    public void OnQte()
    {
        if (moving)
            moving = false;
        //else if(!moving)
        //    moving = true;

        if (manager.checking)
        {
            if (playerQte.greenZone)
            {
                Debug.Log("Green Zone hit!");
                manager.bonusPoints += 2;
                done = true;
                manager.checking = false;
            }
            else if (playerQte.yellowZone)
            {
                Debug.Log("Yellow Zone hit!");
                manager.bonusPoints += 1;
                done = true;
                manager.checking = false;
            }
            else
            {
                done = true;
                manager.checking = false;
            }
        }
    }
}
