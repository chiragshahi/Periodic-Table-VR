using UnityEngine;

public class Highlighting : MonoBehaviour
{
    public int atom_num = 0;
    public GameObject periodic;
    public GameObject hydrogen;
    public GameObject hellium;
    public GameObject lithium;
    public GameObject beryllium;
    public GameObject boron;
    public GameObject carbon;
    public GameObject nitrogen;
    public GameObject oxygen;
    public GameObject fluorine;
    public GameObject neon;

    private int previous_number = 0;

    // Use this for initialization
    void Start()
    {
        atom_num = GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().element;
    }

    /// <summary>
    /// resize the tiles of the table
    /// </summary>
    /// <param name="atom_num"></param>
    /// <param name="flag">1 for enlarge, -1 for shrink</param>
    private void resize(int atom_num, int flag)
    {
        switch (atom_num)
        {
            case 0:
                break;
            case 1:
                hydrogen.transform.localScale += flag * new Vector3(0.16f, 0.16f, 0.0f);
                hydrogen.transform.position += flag * new Vector3(0, 0, 0.15f);
                break;
            case 2:
                hellium.transform.localScale += flag * new Vector3(0.16f, 0.16f, 0.0f);
                hellium.transform.position += flag * new Vector3(0, 0, 0.15f);
                break;
            case 3:
                lithium.transform.localScale += flag * new Vector3(0.16f, 0.16f, 0.0f);
                lithium.transform.position += flag * new Vector3(0, 0, 0.15f);
                break;
            case 4:
                beryllium.transform.localScale += flag * new Vector3(0.16f, 0.16f, 0.0f);
                beryllium.transform.position += flag * new Vector3(0, 0, 0.15f);
                break;
            case 5:
                boron.transform.localScale += flag * new Vector3(0.16f, 0.16f, 0.0f);
                boron.transform.position += flag * new Vector3(0, 0, 0.15f);
                break;
            case 6:
                carbon.transform.localScale += flag * new Vector3(0.16f, 0.16f, 0.0f);
                carbon.transform.position += flag * new Vector3(0, 0, 0.15f);
                break;
            case 7:
                nitrogen.transform.localScale += flag * new Vector3(0.16f, 0.16f, 0.0f);
                nitrogen.transform.position += flag * new Vector3(0, 0, 0.15f);
                break;
            case 8:
                oxygen.transform.localScale += flag * new Vector3(0.16f, 0.16f, 0.0f);
                oxygen.transform.position += flag * new Vector3(0, 0, 0.15f);
                break;
            case 9:
                fluorine.transform.localScale += flag * new Vector3(0.16f, 0.16f, 0.0f);
                fluorine.transform.position += flag * new Vector3(0, 0, 0.15f);
                break;
            case 10:
                neon.transform.localScale += flag * new Vector3(0.16f, 0.16f, 0.0f);
                neon.transform.position += flag * new Vector3(0, 0, 0.15f);
                break;
            default:
                break;
        }
    }

    // Should player error sound when touching greyed out area. NOT DOING THAT YET :(
    public void tableSelected()
    {
        periodic.GetComponent<AudioSource>().Play();
    }

    private void setSelected(int atom_num)
    {
        if (atom_num != previous_number)
        {
            GameObject.Find("Popping").GetComponent<PoppingSound>().playPop();
            resize(previous_number, -1);
            resize(atom_num, 1);
            previous_number = atom_num;
        }
    }

    // Update is called once per frame
    void Update()
    {
        setSelected(atom_num);
    }
}
