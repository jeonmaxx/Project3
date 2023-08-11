using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Sleeping : PlayerNear
{
    public InputActionReference inputAction;
    private InputAction action;
    public DayTime time;

    public GameObject sleepAsking;
    public PlayerMovement movement;

    public Image blackBackground;
    public TextMeshProUGUI sleepText;
    public float backgroundAlpha = 0;
    public bool increasing;
    public bool sleeping;

    public void Start()
    {
        action = inputAction.action;
        sleepAsking.SetActive(false);

        blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, 0);
        sleepText.color = new Color(sleepText.color.r, sleepText.color.g, sleepText.color.b, 0);
    }

    private void Update()
    {
        CalcDistance();
        action.started += _ => OnInteract();


        if(increasing && sleeping)
        {
            if (backgroundAlpha < 1)
            {
                backgroundAlpha += Time.deltaTime * 0.5f;
            }
            else
            {
                StartCoroutine(SleepScreen());
            }
        }
        if(!increasing && sleeping)
        {
            StartCoroutine(SleepScreen());
            if (backgroundAlpha >= 0)
            {
                backgroundAlpha -= Time.deltaTime * 0.5f;
            }
            else
            {
                movement.enabled = true;
                sleeping = false;
            }
        }
        blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, backgroundAlpha);
        sleepText.color = new Color(sleepText.color.r, sleepText.color.g, sleepText.color.b, backgroundAlpha);
    }

    private void OnInteract()
    {
        if(isPlayerNear)
        {
            sleepAsking.SetActive(true);
            movement.enabled = false;
        }    
    }

    public void YesButton()
    {
        increasing = true;
        sleeping = true;
        sleepAsking.SetActive(false);
    }

    public void NoButton()
    {
        movement.enabled = true;
        sleepAsking.SetActive(false);
    }

    public IEnumerator SleepScreen()
    {
        time.collector.dayTime = 0;
        time.currentTime = 0;
        yield return new WaitForSeconds(2);
        increasing = false;
    }
}
