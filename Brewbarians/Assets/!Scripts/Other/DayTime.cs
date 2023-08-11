using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayTime : MonoBehaviour
{
    public float maxDayTime = 10;
    public float currentTime;
    public Transform TimeArrow;
    public Image nightTime;

    public PointsCollector collector;
    public float night;

    public bool coroutineDone;

    public GameObject bed;
    public int SceneIndexBed;

    public Image blackScreen;
    public TextMeshProUGUI wakingUpText;
    private float alpha;
    private bool passedOut;

    private bool startCheck;

    public SceneTester loadNext;
    private GameObject player;
    private PlayerMovement playerMovement;

    private bool sleeping;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>(); 
        coroutineDone = false;
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, 0);
        if(bed != null )
            wakingUpText.color = new Color(wakingUpText.color.r, wakingUpText.color.g, wakingUpText.color.b, 0);
        startCheck = false;
    }

    private void FixedUpdate()
    {
        if (!coroutineDone)
            StartCoroutine(ChangeNightSky());

        if (bed != null && !startCheck && !passedOut)
        {
            if (currentTime == maxDayTime)
            {
                StartCoroutine(WakingUpWaiting());
                Debug.Log("Coroutine started");
            }
            else
            {
                startCheck = true;
                Debug.Log("startCheck done");
            }
        }
        else if(passedOut && bed != null && !startCheck)
        {
            if (alpha > 0)
            {
                alpha -= Time.deltaTime * 0.3f;
            }
            else if(alpha <= 0)  
            {
                startCheck = true;
                Debug.Log("startCheck done");
            }
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
            wakingUpText.color = new Color(wakingUpText.color.r, wakingUpText.color.g, wakingUpText.color.b, alpha);
        }
        else if(bed == null && !startCheck)
        {
            startCheck = true;
            Debug.Log("startCheck done");
        }
    }

    private IEnumerator WakingUpWaiting()
    {
        alpha = 1;
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, 1);
        wakingUpText.color = new Color(wakingUpText.color.r, wakingUpText.color.g, wakingUpText.color.b, 1);        
        player.transform.position = bed.transform.position;
        collector.dayTime = 0;
        currentTime = 0; 
        yield return new WaitForSeconds(2.5f);
        passedOut = true;
    }

    public void Update()
    {
        if(!coroutineDone)
            StartCoroutine(ChangeNightSky());

        if (currentTime < maxDayTime)
            currentTime = collector.dayTime;
        else
            collector.dayTime = maxDayTime;

        ArrowRotation();

        if (currentTime >= (maxDayTime * 0.7f) && coroutineDone)
        {
            if (night <= ((currentTime * maxDayTime) / 130))
                night += (Time.deltaTime * 0.1f);

            nightTime.color = new Color(nightTime.color.r, nightTime.color.g, nightTime.color.b, night);
        }

        if(currentTime == 0)
        {
            night = 0;
            nightTime.color = new Color(nightTime.color.r, nightTime.color.g, nightTime.color.b, night);
        }

        if(startCheck)
            PassOut();
    }

    public void ArrowRotation()
    {
        float angle = -(currentTime * (180 / maxDayTime));

        TimeArrow.eulerAngles = new Vector3(
        TimeArrow.transform.eulerAngles.x,
        TimeArrow.transform.eulerAngles.y,
        angle);
    }

    public IEnumerator ChangeNightSky()
    {
        yield return new WaitForEndOfFrame();
        if (currentTime > (maxDayTime * 0.7f))
        {
            night = (currentTime * maxDayTime) / 130;
            Debug.Log(night);
            nightTime.color = new Color(nightTime.color.r, nightTime.color.g, nightTime.color.b, night);
        }

        coroutineDone = true;
        StopCoroutine(ChangeNightSky());
    }

    public void PassOut()
    {
        PassingOut();
        WakingUp();
    }

    public void PassingOut()
    {
        if (currentTime == maxDayTime)
        {
            if (!sleeping)
            {
                StartCoroutine(SleepAnim());
            }
            else
            {
                if (alpha < 1)
                {
                    alpha += Time.deltaTime * 0.5f;
                }
                else
                {
                    if (bed != null)
                        StartCoroutine(BlackScreen());
                    else if (bed == null)
                    {
                        loadNext.SceneChangeButton(SceneIndexBed);
                    }
                }
                blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
                wakingUpText.color = new Color(wakingUpText.color.r, wakingUpText.color.g, wakingUpText.color.b, alpha);
            }
            
        }
    }

    public void WakingUp()
    {
        if(passedOut)
        {            
            if (alpha >= 0)
            {
                alpha -= Time.deltaTime * 0.3f;
            }
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
            wakingUpText.color = new Color(wakingUpText.color.r, wakingUpText.color.g, wakingUpText.color.b, alpha);
        }       
    }

    private IEnumerator BlackScreen()
    {
        if (bed != null)
        {
            playerMovement.animator.SetBool("IsSleeping", false);
            playerMovement.enabled = true;
            player.transform.position = bed.transform.position;
            collector.dayTime = 0;
            currentTime = 0;
            yield return new WaitForSeconds(2);
            passedOut = true;
            sleeping = false;
            StopCoroutine(BlackScreen());
        }
    }

    public IEnumerator SleepAnim()
    {
        playerMovement.animator.SetBool("IsSleeping", true);
        playerMovement.enabled = false;
        yield return new WaitForSeconds(3);
        sleeping = true;
        StopCoroutine(SleepAnim());
    }
}
