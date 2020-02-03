﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddElectronScript : MonoBehaviour
{

    // Use this for initialization

    public GameObject atomicScriptObject;
    private AtomicStructureBaseClass atomicScript;

    public bool isColliding;

    void Start()
    {

        atomicScript = atomicScriptObject.GetComponent<AtomicStructureBaseClass>();

    }

    void lerpParticle(GameObject particle, Vector3 destination)
    {
        float speed = 0.05f;
        particle.transform.position = Vector3.Lerp(particle.transform.position, destination, Time.deltaTime* speed);
    }

    void OnTriggerStay(Collider collision)
    {
        if (!(collision.gameObject.name.Contains("electron") || collision.gameObject.name.Contains("proton") || collision.gameObject.name.Contains("neutron")))
            return;
        FixedJoint joint = collision.gameObject.GetComponent<FixedJoint>();
        if(joint) return;
        if (isColliding) return;
        isColliding = true;
	
        if (collision.gameObject.name.Contains("electron"))
        {
            int n = atomicScript.getElectronCount();

            // If electron total is already max (10)
            if (n == 10)
            {
                GameObject.Find("Error").GetComponent<ErrorSound>().playError();
            }
            else
            {
                Vector3 destination = atomicScript.getElectronPosition(n + 1);
                lerpParticle(collision.gameObject, destination);
                atomicScript.addElectron();

                collision.gameObject.SetActive(false);
                collision.gameObject.GetComponent<StartingPosition>().ResetToSpawn();
                collision.gameObject.SetActive(true);
                
		GameObject.Find("Popping").GetComponent<PoppingSound>().playPop();
                return;
            }
        }
        if (collision.gameObject.name.Contains("neutron"))
        {
            int n = atomicScript.getNeutronCount();

            // If neutron total is already max (10)
            if (n == 10)
            {
                GameObject.Find("Error").GetComponent<ErrorSound>().playError();
            }

            else
            {
                Vector3 destination = atomicScript.getNeutronPosition(n + 1);
                lerpParticle(collision.gameObject, destination);
                atomicScript.addNeutron();
                collision.gameObject.SetActive(false);
                collision.gameObject.GetComponent<StartingPosition>().ResetToSpawn();
                collision.gameObject.SetActive(true);
                GameObject.Find("Popping").GetComponent<PoppingSound>().playPop();
            }
        }
        if (collision.gameObject.name.Contains("proton"))
        {
            int n = atomicScript.getAtomicNumber();

            // If proton total is already max (10)
            if (n == 10)
            {
                GameObject.Find("Error").GetComponent<ErrorSound>().playError();
            }

            else
            {
                Vector3 destination = atomicScript.getProtonPosition(n + 1);
                lerpParticle(collision.gameObject, destination);
                atomicScript.addProton();
                collision.gameObject.SetActive(false);
                collision.gameObject.GetComponent<StartingPosition>().ResetToSpawn();
                collision.gameObject.SetActive(true);
                GameObject.Find("Popping").GetComponent<PoppingSound>().playPop();
            }
        }
    }

// Update is called once per frame
void Update()
{
        isColliding = false;
}
}