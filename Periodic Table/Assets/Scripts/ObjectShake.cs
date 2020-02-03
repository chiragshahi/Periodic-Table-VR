using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{

    private Vector3 originPosition;
    private Quaternion originRotation;

    public float shake_intensity = 0.005f;
    public bool stable = true;

    void Update()
    {
        if (GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().getStable() == false)
        {
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .01f,
                originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .01f,
                originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .01f,
                originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .01f);
        }
    }

    void Start()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;

    }


}
