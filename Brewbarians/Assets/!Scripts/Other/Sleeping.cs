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
    private Transform ui;
    public PlayerMovement movement;

    public Image blackBackground;
    public TextMeshProUGUI sleepText;
    public float alpha = 0;

    public void Start()
    {
        action = inputAction.action;
        ui = GameObject.Find("UI").transform;
        sleepAsking.SetActive(false);

        blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, 0);
        sleepText.color = new Color(sleepText.color.r, sleepText.color.g, sleepText.color.b, 0);
        blackBackground.enabled = false;
        sleepText.enabled = false;
    }

    private void Update()
    {
        CalcDistance();
        action.started += _ => OnInteract();
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
        StartCoroutine(SleepScreen());
        time.collector.dayTime = 0;
        time.currentTime = 0;
        movement.enabled = true;
        sleepAsking.SetActive(false);
    }

    public void NoButton()
    {
        movement.enabled = true;
        sleepAsking.SetActive(false);
    }

    public IEnumerator SleepScreen()
    {
        blackBackground.enabled = true;
        sleepText.enabled = true;
        if(alpha < 1)
        {
            alpha += Time.deltaTime * 0.1f;
            blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, alpha);
            sleepText.color = new Color(sleepText.color.r, sleepText.color.g, sleepText.color.b, alpha);
        }
        yield return new WaitForSeconds(3);
        if (alpha > 0)
        {
            alpha -= Time.deltaTime * 0.1f;
            blackBackground.color = new Color(blackBackground.color.r, blackBackground.color.g, blackBackground.color.b, alpha);
            sleepText.color = new Color(sleepText.color.r, sleepText.color.g, sleepText.color.b, alpha);
        }
    
    }
}
