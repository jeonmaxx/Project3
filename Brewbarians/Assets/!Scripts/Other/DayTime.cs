using System.Collections;
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

    public void Awake()
    {
        coroutineDone = false;
    }

    public void Update()
    {
        if(!coroutineDone)
            StartCoroutine(ChangeNightSky());

        if(currentTime >= maxDayTime)
            StopCoroutine(ChangeNightSky());

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
        if(currentTime == maxDayTime)
        {
            //pass out
        }
    }
}
