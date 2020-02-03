using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{

    public AudioClip selectionSound;
    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void playClick()
    {
        // Play pop sound
        source.PlayOneShot(selectionSound, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}