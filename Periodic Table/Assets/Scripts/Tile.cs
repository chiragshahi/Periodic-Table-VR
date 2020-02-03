using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Tile : MonoBehaviour {

    public GameObject atomicScriptObject;
    private AtomicStructureBaseClass atomicScript;
    public Text massLabel;
    public Text chargeLabel;
    public Text numProtonsLabel;
    public Text element;
    private string[] elementsName = new string[] {"", "H", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne"};

	// Use this for initialization
	void Start () {
        atomicScript = atomicScriptObject.GetComponent<AtomicStructureBaseClass>();
	}
	
	// Update is called once per frame
	void Update () {
        // Update mass label
        int mass = atomicScript.getAtomicMass();
        massLabel.text = mass.ToString();

        // Update charge label
        int charge = atomicScript.getCharge();
        string sign = "";
        if (charge < 0)
            sign = "-";
        else if (charge > 0)
            sign = "+";
        charge = Math.Abs(charge);

        chargeLabel.text = sign + charge.ToString();

        // Update number of protons
        int protons = atomicScript.getAtomicNumber();
        numProtonsLabel.text = protons.ToString();

        // Update name of element
        element.text = elementsName[protons];
	}
}
