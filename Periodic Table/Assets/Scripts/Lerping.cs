using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using vh = VirtualHand;
public class Lerping : MonoBehaviour
{

    public Vector3 newPosition;
    public float smooth = 2;
    public GameObject particle;

    vh.VirtualHandState state;

    // Use this for initialization
    void Start()
    {
        newPosition = transform.position;
        Debug.Log("hello");
        Debug.Log(particle.GetComponent<MeshFilter>().mesh.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        positionChanging();
        Debug.Log(particle.GetType());
    }

    void positionChanging()
    {
        Vector3 position_Electron = new Vector3(0, 0, 0);  //5th electron position
        Vector3 position_Neutron = new Vector3(0.017f, -0.017f, -0.005f);  //5th neutron position
        Vector3 position_Proton = new Vector3(-0.013f, 0.013f, -0.013f); //5th proton position

        //move particle to correct position when a person holds it. 
        if (state == vh.VirtualHandState.Holding && particle.GetComponent<MeshFilter>().mesh.ToString() == "electron_solo")
        {
            newPosition = position_Electron;
        }
        else if (state == vh.VirtualHandState.Holding && particle.GetComponent<MeshFilter>().mesh.ToString() == "neutron_solo")
        {
            newPosition = position_Neutron;
        }
        else if (state == vh.VirtualHandState.Holding && particle.GetComponent<MeshFilter>().mesh.ToString() == "proton_solo")
        {
            newPosition = position_Proton;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * smooth);
    }
}
