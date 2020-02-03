using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsObj : MonoBehaviour {

    void OnTriggerEnter(Collider outOfBounds)
    {

        if (!(outOfBounds.gameObject.name.Contains("electron") || outOfBounds.gameObject.name.Contains("proton") || outOfBounds.gameObject.name.Contains("neutron")))
        {
            return;
        }

        else
        {

            outOfBounds.gameObject.SetActive(false);

            outOfBounds.gameObject.GetComponent<HighlightObject>().StopHighlight(outOfBounds.gameObject);
            outOfBounds.gameObject.GetComponent<StartingPosition>().ResetToSpawn();

            outOfBounds.gameObject.SetActive(true);
        }
    }
}
