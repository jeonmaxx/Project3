using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenBrewing : PlayerNear
{
    public PlayerInput input;
    public RectTransform brewMenu;

    public bool menuOpen = false;
    public bool choosing = false;

    public PlayerMovement movement;

    public void Awake()
    {
        brewMenu.transform.localScale = Vector3.zero;
    }

    public void Update()
    {
        CalcDistance();

        if (menuOpen && !isPlayerNear)
        {
            brewMenu.LeanScale(Vector3.zero, 0.5f).setEaseOutExpo();
            menuOpen = false;
        }
        else if (menuOpen && isPlayerNear)
        {
            brewMenu.LeanScale(Vector3.one, 0.5f).setEaseOutExpo();
            movement.enabled = false;
        }
        else if (!menuOpen)
        {
            brewMenu.LeanScale(Vector3.zero, 0.5f).setEaseOutExpo();
            movement.enabled = true;
        }
    }

    public void OnInteract()
    {
        if (isPlayerNear && !menuOpen || choosing)
        {
            menuOpen = true;
        }
        else
        {
            menuOpen = false;
        }
    }


}
