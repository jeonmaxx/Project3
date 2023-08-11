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

    public void Start()
    {
        StartCoroutine(ChangeNightSky());
    }

    public void Update()
    {
        currentTime = collector.dayTime;
        ArrowRotation();
    }

    public void ArrowRotation()
    {
        float angle = -(currentTime * (180 / maxDayTime));

        TimeArrow.eulerAngles = new Vector3(
        TimeArrow.transform.eulerAngles.x,
        TimeArrow.transform.eulerAngles.y,
        angle);

        //if (currentTime >= (maxDayTime * 0.7f))
        //{
        //    nightTime.color = new Color(nightTime.color.r, nightTime.color.g, nightTime.color.b, night);
        //    if (night <= ((currentTime * maxDayTime) / 130))
        //    {
        //        night += (Time.deltaTime * 0.1f);
        //    }
        //    nightTime.color = new Color(nightTime.color.r, nightTime.color.g, nightTime.color.b, night);
        //}
    }

    public IEnumerator ChangeNightSky()
    {
        yield return new WaitForEndOfFrame();
        night = (currentTime * maxDayTime) / 130;
        if (currentTime >= (maxDayTime * 0.7f))
        {
            Debug.Log(night);
            nightTime.color = new Color(nightTime.color.r, nightTime.color.g, nightTime.color.b, night);
            yield return new WaitForEndOfFrame();
            night += (Time.deltaTime * 0.1f);
            nightTime.color = new Color(nightTime.color.r, nightTime.color.g, nightTime.color.b, night);
        }
    }

    public void PassOut()
    {
        if(currentTime == maxDayTime)
        {
            //pass out
        }
    }
}
