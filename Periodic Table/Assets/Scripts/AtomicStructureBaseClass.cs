using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomicStructureBaseClass : MonoBehaviour
{

    public int element = 0;

    public int electrons { get; set; }
    public int protons { get; set; }
    public int neutrons { get; set; }

    private int[] neutronCount = new int[] { 0, 0, 2, 4, 5, 6, 6, 7, 8, 10, 10 };

    public GameObject electron1;
    public GameObject electron2;
    public GameObject electron3;
    public GameObject electron4;
    public GameObject electron5;
    public GameObject electron6;
    public GameObject electron7;
    public GameObject electron8;
    public GameObject electron9;
    public GameObject electron10;

    public GameObject proton1;
    public GameObject proton2;
    public GameObject proton3;
    public GameObject proton4;
    public GameObject proton5;
    public GameObject proton6;
    public GameObject proton7;
    public GameObject proton8;
    public GameObject proton9;
    public GameObject proton10;

    public GameObject neutron1;
    public GameObject neutron2;
    public GameObject neutron3;
    public GameObject neutron4;
    public GameObject neutron5;
    public GameObject neutron6;
    public GameObject neutron7;
    public GameObject neutron8;
    public GameObject neutron9;
    public GameObject neutron10;

    public GameObject parentObj;

    private GameObject[] electronObjects = new GameObject[10];
    private GameObject[] protonObjects = new GameObject[10];
    private GameObject[] neutronObjects = new GameObject[10];

    private enum ShrinkState
    {
        Default,
        Shrink,
        Grow,
        Zero
    };

    private ShrinkState shrinkState;

    private float scaleX, scaleY, scaleZ;
    private float shrinkTime = 0.5f;

    // Use this for initialization
    void Start()
    {

        scaleX = 20;
        scaleY = 20;
        scaleZ = 20;

        electronObjects = new GameObject[] { electron1, electron2, electron3, electron4, electron5, electron6, electron7, electron8, electron9, electron10 };
        protonObjects = new GameObject[] { proton1, proton2, proton3, proton4, proton5, proton6, proton7, proton8, proton9, proton10 };
        neutronObjects = new GameObject[] { neutron1, neutron2, neutron3, neutron4, neutron5, neutron6, neutron7, neutron8, neutron9, neutron10 };

        element = 0;
        setAtomicNumber(element);
        emptyModel();

    }

    // Clears all particles from structure
    public void emptyModel()
    {
        for (int i = 0; i < 10; i++)
        {

            protonObjects[i].SetActive(false);
            neutronObjects[i].SetActive(false);
            electronObjects[i].GetComponent<Renderer>().enabled = false;

        }
    }

    public void setModel(int element)
    {
        emptyModel();

        //Show electrons
        for (int i = 0; i < electrons; i++)
        {
            electronObjects[i].SetActive(true);
            electronObjects[i].GetComponent<Renderer>().enabled = true;
        }

        //Show protons
        for (int i = 0; i < protons; i++)
        {
            protonObjects[i].SetActive(true);
        }

        //show neutrons
        for (int i = 0; i < neutrons; i++)
        {
            neutronObjects[i].SetActive(true);
        }

    }

    public void changeElement(int ele)
    {
        if (ele != element)
        {
            GameObject.Find("Elements").GetComponent<Highlighting>().atom_num = ele;
            startShrink();
            element = ele;
        }
    }

    // Used for the quiz
    public void showElement(int ele)
    {
        startShrink();
        element = ele;
    }

    public void setAtomicNumber(int n)
    {
        protons = n;
        electrons = n;
        neutrons = neutronCount[n];
    }

    public int getAtomicMass()
    {
        return protons + neutrons;
    }

    public int getCharge()
    {
        return protons - electrons;
    }

    public int getAtomicNumber()
    {
        return protons;
    }

    public int getNeutronCount()
    {
        return neutrons;
    }

    public int getElectronCount()
    {
        return electrons;
    }

    public bool getStable()
    {
        // Structure is stable
        if (neutronCount[protons] == neutrons)
        {
            return true;
        }

        // Structure is unstable
        else
        {
            return false;
        }
    }

    public Vector3 getNeutronPosition(int n)
    {
        return neutronObjects[n - 1].transform.position;
    }

    public Vector3 getProtonPosition(int n)
    {
        return protonObjects[n - 1].transform.position;
    }

    public Vector3 getElectronPosition(int n)
    {
        return electronObjects[n - 1].transform.position;
    }

    public void addProton()
    {
        protons++;
        setModel(element);
        GameObject.Find("Elements").GetComponent<Highlighting>().atom_num = protons;
    }

    public void addElectron()
    {
        electrons++;
        setModel(element);
    }

    public void addNeutron()
    {
        neutrons++;
        setModel(element);
    }

    public void removeElectron(GameObject target)
    {
        target.GetComponent<Renderer>().enabled = false;
        electrons--;
    }

    public void removeProton(GameObject target)
    {
        target.SetActive(false);
        protons--;
        GameObject.Find("Elements").GetComponent<Highlighting>().atom_num = protons;
    }

    public void removeNeutron(GameObject target)
    {
        target.SetActive(false);
        neutrons--;
    }

    void startShrink()
    {
        shrinkState = ShrinkState.Shrink;
    }

    void shrink()
    {
        for (int i = 0; i < 10; i++)
        {
            electronObjects[i].transform.localScale -= new Vector3(scaleX * Time.deltaTime / shrinkTime, scaleY * Time.deltaTime / shrinkTime, scaleZ * Time.deltaTime / shrinkTime);
            protonObjects[i].transform.localScale -= new Vector3(scaleX * Time.deltaTime / shrinkTime, scaleY * Time.deltaTime / shrinkTime, scaleZ * Time.deltaTime / shrinkTime);
            neutronObjects[i].transform.localScale -= new Vector3(scaleX * Time.deltaTime / shrinkTime, scaleY * Time.deltaTime / shrinkTime, scaleZ * Time.deltaTime / shrinkTime);
        }
    }

    void shrinkFinish()
    {
        for (int i = 0; i < 10; i++)
        {
            electronObjects[i].transform.localScale = new Vector3(0, 0, 0);
            protonObjects[i].transform.localScale = new Vector3(0, 0, 0);
            neutronObjects[i].transform.localScale = new Vector3(0, 0, 0);
        }
    }

    void startGrow()
    {
        shrinkState = ShrinkState.Grow;
    }

    void grow()
    {
        for (int i = 0; i < 10; i++)
        {
            electronObjects[i].transform.localScale += new Vector3(scaleX * Time.deltaTime / shrinkTime, scaleY * Time.deltaTime / shrinkTime, scaleZ * Time.deltaTime / shrinkTime);
            protonObjects[i].transform.localScale += new Vector3(scaleX * Time.deltaTime / shrinkTime, scaleY * Time.deltaTime / shrinkTime, scaleZ * Time.deltaTime / shrinkTime);
            neutronObjects[i].transform.localScale += new Vector3(scaleX * Time.deltaTime / shrinkTime, scaleY * Time.deltaTime / shrinkTime, scaleZ * Time.deltaTime / shrinkTime);
        }
    }

    void growFinish()
    {
        for (int i = 0; i < 10; i++)
        {
            electronObjects[i].transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            protonObjects[i].transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            neutronObjects[i].transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (shrinkState == ShrinkState.Shrink)
        {
            if (electronObjects[0].transform.localScale.x > 0.001f)
            {
                //parentObj.transform.localScale -= new Vector3(scaleX * Time.deltaTime / shrinkTime, scaleY * Time.deltaTime / shrinkTime, scaleZ * Time.deltaTime / shrinkTime);
                shrink();
            }
            else
            {
                //parentObj.transform.localScale = new Vector3(0, 0, 0);
                shrinkFinish();
                setAtomicNumber(element);
                setModel(element);
                startGrow();
            }
        }
        else if (shrinkState == ShrinkState.Grow)
        {
            if (electronObjects[0].transform.localScale.x < scaleX)
            {
                //parentObj.transform.localScale += new Vector3 (scaleX * Time.deltaTime / shrinkTime, scaleY * Time.deltaTime / shrinkTime, scaleZ * Time.deltaTime / shrinkTime);
                grow();
            }
            else
            {
                //parentObj.transform.localScale = new Vector3 (scaleX, scaleY, scaleZ);
                growFinish();
                shrinkState = ShrinkState.Default;
            }
        }
    }
}
