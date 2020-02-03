using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoppingSound : MonoBehaviour {

    public AudioClip selectionSound;
    private AudioSource source;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
    }

    public void playPop()
    {
        // Play pop sound
        source.PlayOneShot(selectionSound, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
