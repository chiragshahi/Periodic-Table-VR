using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizTimer : MonoBehaviour {

    private int minutes;
    private int seconds;

    public Text timer;
    public GameObject play;
    public GameObject stop;

    void Start()
    {
        stop.gameObject.SetActive(false);
        play.gameObject.SetActive(true);
    }

    public void reset()
    {
        minutes = 0;
        seconds = 0;

        timer.text = "0:00";
    }

    public void startTime()
    {
        reset();
        stop.gameObject.SetActive(true);
        play.gameObject.SetActive(false);
        StartCoroutine(RunTimer());
    }

    public void stopTime()
    {
        stop.gameObject.SetActive(false);
        play.gameObject.SetActive(true);
        StopAllCoroutines();
    }

    private IEnumerator RunTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            seconds += 1;

            if (seconds == 60)
            {
                minutes += 1;
                seconds = 0;
            }

            if (seconds < 10)
            {
                timer.text = minutes.ToString() + ":0" + seconds.ToString();
            }
            else
            {
                timer.text = minutes.ToString() + ":" + seconds.ToString();
            }
        }
    }

}
