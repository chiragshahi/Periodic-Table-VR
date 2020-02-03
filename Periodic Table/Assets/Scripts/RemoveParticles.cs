using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveParticles : MonoBehaviour {

    // Use this for initialization
    void Start () {
     
    }

    // Enables particle selected to be interactable
    public void interaction(GameObject target)
    {

        GameObject.Find("Popping").GetComponent<PoppingSound>().playPop();


        if (target.gameObject.name.Contains("Electron")){
            GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().removeElectron(target);
            return;
        }

        else if (target.gameObject.name.Contains("Proton")){
            GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().removeProton(target);
            return;
        }

        else if (target.gameObject.name.Contains("Neutron")){
            GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().removeNeutron(target);
            return;
        }

    }
	
	// Update is called once per frame
	void Update () {

    }
}
