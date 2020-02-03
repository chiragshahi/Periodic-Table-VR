using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionAudio : MonoBehaviour {

    AudioSource audio;
    private string[] questionNums = { "Question-0", "Question-1", "Question-2", "Question-3", "Question-4", "Question-5", "Question-6", "Question-7",
        "Question-8", "Question-9", "Question-10", "Question-11","Question-11", "Question-11", "Question-11" };
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}

    public void playQuestionAudio()
    {
        int questionNum = GameObject.Find("QuizScript").GetComponent<Quiz>().arrayNum;
        audio.clip = Resources.Load<AudioClip>(questionNums[questionNum]);
        audio.Play();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
