/*
Copyright ©2017. The University of Texas at Dallas. All Rights Reserved. 

Permission to use, copy, modify, and distribute this software and its documentation for 
educational, research, and not-for-profit purposes, without fee and without a signed 
licensing agreement, is hereby granted, provided that the above copyright notice, this 
paragraph and the following two paragraphs appear in all copies, modifications, and 
distributions. 

Contact The Office of Technology Commercialization, The University of Texas at Dallas, 
800 W. Campbell Road (AD15), Richardson, Texas 75080-3021, (972) 883-4558, 
otc@utdallas.edu, https://research.utdallas.edu/otc for commercial licensing opportunities.

IN NO EVENT SHALL THE UNIVERSITY OF TEXAS AT DALLAS BE LIABLE TO ANY PARTY FOR DIRECT, 
INDIRECT, SPECIAL, INCIDENTAL, OR CONSEQUENTIAL DAMAGES, INCLUDING LOST PROFITS, ARISING 
OUT OF THE USE OF THIS SOFTWARE AND ITS DOCUMENTATION, EVEN IF THE UNIVERSITY OF TEXAS AT 
DALLAS HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

THE UNIVERSITY OF TEXAS AT DALLAS SPECIFICALLY DISCLAIMS ANY WARRANTIES, INCLUDING, BUT 
NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
PURPOSE. THE SOFTWARE AND ACCOMPANYING DOCUMENTATION, IF ANY, PROVIDED HEREUNDER IS 
PROVIDED "AS IS". THE UNIVERSITY OF TEXAS AT DALLAS HAS NO OBLIGATION TO PROVIDE 
MAINTENANCE, SUPPORT, UPDATES, ENHANCEMENTS, OR MODIFICATIONS.
*/

using UnityEngine;
using System.Collections;

public class VirtualHand : MonoBehaviour {

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

    public bool returnObjectHome = false;

    // Enumerate states of virtual hand interactions
    public enum VirtualHandState {
		Open,
		Touching,
        Closed,
		Holding
	};

	// Inspector parameters
	[Tooltip("The tracking device used for tracking the real hand.")]
	public CommonTracker tracker;

	[Tooltip("The interactive used to represent the virtual hand.")]
	public Affect hand;

	[Tooltip("The button required to be pressed to grab objects.")]
	public CommonButton button;

	[Tooltip("The speed amplifier for thrown objects. One unit is physically realistic.")]
	public float speed = 1.0f;

	// Private interaction variables
	public VirtualHandState state;
	FixedJoint grasp;

    // Called at the end of the program initialization
    void Start () {

        // Set initial state to open
        state = VirtualHandState.Open;

		// Ensure hand interactive is properly configured
		hand.type = AffectType.Virtual;
	}

    public void returnObject( )
    {
        Debug.Log("Destroy grasp.");

        // Get rigidbody of grasped target
        Rigidbody target = grasp.GetComponent<Rigidbody>();
        // Break grasp
        DestroyImmediate(grasp);

        state = VirtualHandState.Closed;
    }

    // FixedUpdate is not called every graphical frame but rather every physics frame
    void FixedUpdate ()
	{

        // If state is open
        if (state == VirtualHandState.Open) {
			
			// If the hand is touching something
			if (hand.triggerOngoing) {

				// Change state to touching
				state = VirtualHandState.Touching;
			}

			// Process current open state
			else {

				// Nothing to do for open
			}
		}

		// If state is touching
		else if (state == VirtualHandState.Touching) {

			// If the hand is not touching something
			if (!hand.triggerOngoing) {

				// Change state to open
				state = VirtualHandState.Open;
			}

			// If the hand is touching something and the button is pressed
			else if (hand.triggerOngoing && button.GetPress ()) {

                // Fetch touched target
                Collider target = hand.ongoingTriggers[0];

                // If a particle is to be removed from the structure 
                if (target.gameObject.name.Contains("Electron") || target.gameObject.name.Contains("Proton") || target.gameObject.name.Contains("Neutron"))
                {
                    GameObject.Find("RemoveParticles").GetComponent<RemoveParticles>().interaction(target.gameObject);
                    state = VirtualHandState.Closed;
                }

                /*
		    else if (target.gameObject.name.Contains("CircularProgressBar")) {
		    Debug.Log("sgdyugd");
		    state = VirtualHandState.Closed;
		    }
            */

                // If the button is pressed for the quiz to begin
                else if (target.gameObject.name.Contains("Start Quiz Button"))
                {
                    GameObject.Find("ClickSound").GetComponent<ClickSound>().playClick();
                    GameObject.Find("QuizScript").GetComponent<Quiz>().startQuiz();
                    state = VirtualHandState.Closed;
                }

                // If the submit button is pressed to answer a question
                else if (target.gameObject.name.Contains("Submit Answer Button"))
                {
                    GameObject.Find("QuizScript").GetComponent<Quiz>().submitAnswer();
                    state = VirtualHandState.Closed;
                }

                // If the see solution button is pressed
                else if (target.gameObject.name.Contains("See Solution Button"))
                {
                    GameObject.Find("ClickSound").GetComponent<ClickSound>().playClick();
                    GameObject.Find("QuizScript").GetComponent<Quiz>().showSolution();
                    state = VirtualHandState.Closed;
                }

                // If answer choice 1 button is pressed
                else if (target.gameObject.name.Contains("Answer Choice 1 Button"))
                { 
                    GameObject.Find("QuizScript").GetComponent<Quiz>().answerChoicePressed(1);
                    state = VirtualHandState.Closed;
                }

                // If answer choice 2 button is pressed
                else if (target.gameObject.name.Contains("Answer Choice 2 Button"))
                {
                    GameObject.Find("QuizScript").GetComponent<Quiz>().answerChoicePressed(2);
                    state = VirtualHandState.Closed;
                }

                // If answer choice 3 button is pressed
                else if (target.gameObject.name.Contains("Answer Choice 3 Button"))
                {
                    GameObject.Find("QuizScript").GetComponent<Quiz>().answerChoicePressed(3);
                    state = VirtualHandState.Closed;
                }

                // If answer choice 4 button is pressed
                else if (target.gameObject.name.Contains("Answer Choice 4 Button"))
                {
                    GameObject.Find("QuizScript").GetComponent<Quiz>().answerChoicePressed(4);
                    state = VirtualHandState.Closed;
                }

                // Checks if the user has selected one of the selectable elements on the Periodic Table.
                // It then adjusts the Highlighting value and model's value accordingly.
                // It will NOT create a joint and will keep the element stationary (except highlighting feature which is called).

                // Check for Hydrogen
                else if (target.gameObject == hydrogen) {

                    // Dismiss element from table if it is already selected
                    if (GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().element == 1)
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(0);
                    }

                    // If element is not already selected, set it to Hydrogen
                    else
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(1);
                    }

                    // Changes state to closed so the user will have to release the trigger before selecting anything else
                    state = VirtualHandState.Closed;
                }

                // Check for Hellium
                else if (target.gameObject == hellium) {

                    // Dismiss element from table if it is already selected
                    if (GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().element == 2)
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(0);
                    }

                    // If element is not already selected, set it to Hellium
                    else
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(2);
                    }

                    // Changes state to closed so the user will have to release the trigger before selecting anything else
                    state = VirtualHandState.Closed;

                }

                // Check for Lithium
                else if (target.gameObject == lithium) {

                    // Dismiss element from table if it is already selected
                    if (GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().element == 3)
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(0);
                    }

                    // If element is not already selected, set it to Lithium
                    else
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(3);
                    }

                    // Changes state to closed so the user will have to release the trigger before selecting anything else
                    state = VirtualHandState.Closed;
                }

                // Check for Beryllium
                else if (target.gameObject == beryllium) {

                    // Dismiss element from table if it is already selected
                    if (GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().element == 4)
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(0);
                    }

                    // If element is not already selected, set it to Beryllium
                    else
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(4);
                    }

                    // Changes state to closed so the user will have to release the trigger before selecting anything else
                    state = VirtualHandState.Closed;
                }

                // Check for Boron
                else if (target.gameObject == boron) {

                    // Dismiss element from table if it is already selected
                    if (GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().element == 5)
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(0);
                    }

                    // If element is not already selected, set it to Boron
                    else
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(5);
                    }

                    // Changes state to closed so the user will have to release the trigger before selecting anything else
                    state = VirtualHandState.Closed;
                }

                // Check for Carbon
                else if (target.gameObject == carbon) {

                    // Dismiss element from table if it is already selected
                    if (GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().element == 6)
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(0);
                    }

                    // If element is not already selected, set it to Carbon
                    else
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(6);
                    }

                    // Changes state to closed so the user will have to release the trigger before selecting anything else
                    state = VirtualHandState.Closed;
                }

                // Check for Nitrogen
                else if (target.gameObject == nitrogen) {

                    // Dismiss element from table if it is already selected
                    if (GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().element == 7)
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(0);
                    }

                    // If element is not already selected, set it to Nitrogen
                    else
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(7);
                    }

                    // Changes state to closed so the user will have to release the trigger before selecting anything else
                    state = VirtualHandState.Closed;
                }

                // Check for Oxygen
                else if (target.gameObject == oxygen) {

                    // Dismiss element from table if it is already selected
                    if (GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().element == 8)
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(0);
                    }

                    // If element is not already selected, set it to Oxygen
                    else
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(8);
                    }

                    // Changes state to closed so the user will have to release the trigger before selecting anything else
                    state = VirtualHandState.Closed;
                }

                // Check for Flourine
                else if (target.gameObject == fluorine) {

                    // Dismiss element from table if it is already selected
                    if (GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().element == 9)
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(0);
                    }

                    // If element is not already selected, set it to Flourine
                    else
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(9);
                    }

                    // Changes state to closed so the user will have to release the trigger before selecting anything else
                    state = VirtualHandState.Closed;
                }

                // Check for Neon
                else if (target.gameObject == neon) {

                    // Dismiss element from table if it is already selected
                    if (GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().element == 10)
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(0);
                    }

                    // If element is not already selected, set it to Neon
                    else
                    {
                        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().changeElement(10);
                    }

                    // Changes state to closed so the user will have to release the trigger before selecting anything else
                    state = VirtualHandState.Closed;
                }

                // If user has selected an interactable object NOT on the Periodic Table.
                else
                {

                    // Create a fixed joint between the hand and the target
                    grasp = target.gameObject.AddComponent<FixedJoint>();
                    // Set the connection
                    grasp.connectedBody = hand.gameObject.GetComponent<Rigidbody>();

                    // Change state to holding
                    state = VirtualHandState.Holding;
                }
			}

			// Process current touching state
			else {

				// Nothing to do for touching
			}
		}

        // If state is closed
        else if (state == VirtualHandState.Closed)
        {
            if (!button.GetPress())
            {
                state = VirtualHandState.Open;
            }
            else
            {
                //do nothing
            }
        }

		// If state is holding
		else if (state == VirtualHandState.Holding) {

			// If grasp has been broken
			if (grasp == null) {
				
				// Update state to open
				state = VirtualHandState.Open;
			}

            /*
            else if (returnObjectHome == true)
            {
                Debug.Log("Destroy grasp.");

                // Get rigidbody of grasped target
                Rigidbody target = grasp.GetComponent<Rigidbody>();
                // Break grasp
                DestroyImmediate(grasp);

                returnObjectHome = false;
                state = VirtualHandState.Closed;
            }
            */
				
			// If button has been released and grasp still exists
			else if (!button.GetPress () && grasp != null) {

				// Get rigidbody of grasped target
				Rigidbody target = grasp.GetComponent<Rigidbody> ();
				// Break grasp
				DestroyImmediate (grasp);

				// Apply physics to target in the event of attempting to throw it
				target.velocity = hand.velocity * speed;
				target.angularVelocity = hand.angularVelocity * speed;

				// Update state to open
				state = VirtualHandState.Open;
			}

			// Process current holding state
			else {

				// Nothing to do for holding
			}
		}
	}
}