using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class HighlightObject : MonoBehaviour
{
    public float animationTime = 1f;
    public float threshold = 1.5f;

    //private HighlightController controller;
    private Material material;
    private Color normalColor;
    private Color selectedColor;
    //public VirtualHand vh;
    private Collider collider1;
    private Collider collider2;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        //controller = FindObjectOfType<HighlightController>();

        normalColor = material.color;
        selectedColor = new Color(
          Mathf.Clamp01(normalColor.r * threshold),
          Mathf.Clamp01(normalColor.g * threshold),
          Mathf.Clamp01(normalColor.b * threshold)
        );
    }

    private void Start()
    {
        //StartHighlight();
    }

    public void StartHighlight(GameObject gameObject)
    {
        iTween.ColorTo(gameObject, iTween.Hash(
          "color", selectedColor,
          "time", animationTime,
          "easetype", iTween.EaseType.linear,
          "looptype", iTween.LoopType.pingPong
        ));
    }

    public void StopHighlight(GameObject gameObject)
    {
        iTween.Stop(gameObject);
        material.color = normalColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        collider1 = GetComponents<SphereCollider>()[0];
        //collider2 = GetComponents<SphereCollider>()[1];
        StartHighlight(collider1.gameObject);
	//StopHighlight(collider2.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        collider1 = GetComponents<SphereCollider>()[0];
        StopHighlight(collider1.gameObject);
    }
}
