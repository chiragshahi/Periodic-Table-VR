using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPosition : MonoBehaviour {

    private Vector3 spawnPos;

    void Start()
    {
        spawnPos = transform.position;
    }


    public void ResetToSpawn()
    {
        gameObject.GetComponent<HighlightObject>().StopHighlight(gameObject);
        transform.position = spawnPos;
    }
}
