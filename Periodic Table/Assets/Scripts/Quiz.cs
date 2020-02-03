using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

    // Button used to submit answers.
    public GameObject submitButton;
    public Text submitAnswerText;

    // Button used to solve atomic structure questions
    public Button seeSolutionButton;

    // Buttons used to answer choices
    public Button answerChoice1;
    public Button answerChoice2;
    public Button answerChoice3;
    public Button answerChoice4;

    // Text for the button choices
    public Text answerText1;
    public Text answerText2;
    public Text answerText3;
    public Text answerText4;

    // Used to display the quiz question
    public Text questionText;

    // Score used to keep track of how many quiz questions the user has completed correctly
    private int score;

    // Used to keep track of which question number the user is on out of the 10 to be asked.
    private int questionNumber;

    // Used to keep track of which random array question is being asked.
    public int arrayNum;

    // Array of quiz questions
    private string[] questions = new string[15];
    private bool[] picked = new bool[15];

    // Array of element names
    private string[] elements = new string[10];

    // Used to keep track of answer choices for buttons
    private string[] answerChoices = new string[4];

    // Used to keep track of the correct answer choice for a multiple choice question
    private int correctAnswer;

    // Used to keep track of the desired mass, protons, and charge for the create element model questions
    private int mass;
    private int charge;
    private int protons;

    // Use this for initialization
    void Start()
    {

        // Disable all answer choice buttons
        answerChoice1.gameObject.SetActive(false);
        answerChoice2.gameObject.SetActive(false);
        answerChoice3.gameObject.SetActive(false);
        answerChoice4.gameObject.SetActive(false);
        seeSolutionButton.gameObject.SetActive(false);

        // Array of 15 questions. Last 4 are the same. It will pick a random element to show.
        questions = new string[15] { "Create Hydrogen with a charge of -2.", "Create Neon with a mass of 12.",
                                    "Create Beryllium with 0 charge.", "Create Boron with a mass of 7 and a charge of +1.",
                                    "Create Carbon with a mass of 10 and a charge of -1.", "Create the stable version of Nitrogen with a charge of 0.",
                                    "What is the 2nd element on the periodic table?", "What is the 8th element on the periodic table?", "What is the 4th element on the periodic table?",
                                    "What is the 9th element on the periodic table?", "What is the 5th element on the periodic table?", "Which element’s stable atomic structure is being shown?",
                                    "Which element’s stable atomic structure is being shown?", "Which element’s stable atomic structure is being shown?",
                                    "Which element’s stable atomic structure is being shown?"};

        elements = new string[10] { "H", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne" };

        answerChoices = new string[4] { "", "", "", "" };

    }

    // Once the user selects Start Quiz button
    public void startQuiz()
    {

        // Initlize score to zero.
        score = 0;

        // Initilize question number to one.
        questionNumber = 1;

        // Initlize array of picked questions already to all false.
        for (int i = 0; i < 15; i++)
        {
            picked[i] = false;
        }

        // Start timer
        GameObject.Find("TimerText").GetComponent<QuizTimer>().startTime();
        submitButton.gameObject.SetActive(true);
        submitAnswerText.text = "Submit Answer";

        // Generates a random number to pick a question from the array.
        setArrayNum();
        displayQuestion();
    }

    private void setArrayNum()
    {
        arrayNum = Random.Range(0, 15);
    }

    // Displays a random question during the quiz
    void displayQuestion() { 

        // If the question has already been asked, pick a new one.
        while (picked[arrayNum] == true) {
            setArrayNum();
        }

        // Show question text and mark question as visited.
        questionText.text = questions[arrayNum];
        picked[arrayNum] = true;
        questionNumber += 1;

        submitAnswerText.text = "Submit Answer";

        // Resets atomic model to empty
        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().showElement(0);

	    // Play question audio
	    GameObject.Find("QuestionAudio").GetComponent<QuestionAudio>().playQuestionAudio();

        // Switch statement to handle each question.
        switch (arrayNum)
        {

            // Question: "Create H (Hydrogen) with a charge of -2."
            case 0:
                Debug.Log("Question: " + arrayNum);

                charge = -2;
                mass = 1;
                protons = 1;

                modelBasedQuestion();
                break;

            // Question: "Create Ne (Neon) with a mass of 12."
            case 1:
                Debug.Log("Question: " + arrayNum);

                charge = 0;
                mass = 12;
                protons = 10;

                modelBasedQuestion();
                break;

            // Question: "Create Be (Beryllium) with 0 charge." 
            case 2:
                Debug.Log("Question: " + arrayNum);

                charge = 0;
                mass = 9;
                protons = 4;

                modelBasedQuestion();
                break;

            // Question: "Create B (Boron) with a mass of 7 and a charge of +1."
            case 3:
                Debug.Log("Question: " + arrayNum);

                charge = 1;
                mass = 7;
                protons = 5;

                modelBasedQuestion();
                break;

            // Question: "Create C (Carbon) with a mass of 10 and a charge of -1."
            case 4:
                Debug.Log("Question: " + arrayNum);

                charge = -1;
                mass = 10;
                protons = 6;

                modelBasedQuestion();
                break;

            // Question: "Create the stable version of Nitrogen with a charge of 0."
            case 5:
                Debug.Log("Question: " + arrayNum);

                charge = 0;
                mass = 14;
                protons = 7;

                modelBasedQuestion();
                break;

            // Question: "What is the 2nd element on the table?"
            case 6:
                Debug.Log("Question: " + arrayNum);
                answerChoices = new string[4] { "H", "B", "He", "O" };
                correctAnswer = 3;
                multipleChoiceQuestion();
                break;

            // Question: "What is the 8th element on the table?"
            case 7:
                Debug.Log("Question: " + arrayNum);
                answerChoices = new string[4] { "O", "F", "N", "Ne" };
                correctAnswer = 1;
                multipleChoiceQuestion();
                break;

            // Question: "What is the 4th element on the table?"
            case 8:
                Debug.Log("Question: " + arrayNum);
                answerChoices = new string[4] { "H", "B", "Li", "Be" };
                correctAnswer = 4;
                multipleChoiceQuestion();
                break;

            // Question: "What is the 9th element on the table?"
            case 9:
                Debug.Log("Question: " + arrayNum);
                answerChoices = new string[4] { "B", "F", "He", "Li" };
                correctAnswer = 2;
                multipleChoiceQuestion();
                break;

            // Question: "What is the 5th element on the table?"
            case 10:
                Debug.Log("Question: " + arrayNum);
                answerChoices = new string[4] { "N", "Li", "B", "Ne" };
                correctAnswer = 3;
                multipleChoiceQuestion();
                break;

            // All of the below are the same question. It will pick a random element to show.
            // Question: "Which element’s stable atomic structure is being shown?"
            case 11:
                Debug.Log("Question: " + arrayNum);

                correctAnswer = 1;
                chooseRandomElement();
                multipleChoiceQuestion();
                break;

            case 12:
                Debug.Log("Question: " + arrayNum);

                correctAnswer = 4;
                chooseRandomElement();
                multipleChoiceQuestion();
                break;

            case 13:
                Debug.Log("Question: " + arrayNum);

                correctAnswer = 4;
                chooseRandomElement();
                multipleChoiceQuestion();
                break;

            case 14:
                Debug.Log("Question: " + arrayNum);

                correctAnswer = 2;
                chooseRandomElement();
                multipleChoiceQuestion();
                break;

            default:
                break;
        }
    }

    // Used to show the solution is the user gives up on a model building question.
    public void showSolution()
    {

        submitAnswerText.text = "Next Question";
        seeSolutionButton.gameObject.SetActive(false);
        score -= 1;

        Debug.Log("Show solution for: " + arrayNum);
        switch (arrayNum)
        {
            // Question: "Create H (Hydrogen) with a charge of -2." //charge -2, mass = 1, protons = 1
            case 0:
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().setAtomicNumber(1);
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addElectron();
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addElectron();
                break;

            // Question: "Create Ne (Neon) with a mass of 12." //charge = 0, mass = 12, protons = 12
            case 1:
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().setAtomicNumber(0);
                for(int i = 0; i < 10; i++)
                {
                    GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addProton();
                    GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addElectron();
                }
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addNeutron();
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addNeutron();
                break;

            // Question: "Create Be (Beryllium) with 0 charge." //charge = 0, mass = 9, protons = 4;
            case 2:
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().setAtomicNumber(4);
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().showElement(4);
                break;

            // Question: "Create B (Boron) with a mass of 7 and a charge of +1." //charge = 1, mass = 7, protons = 5
            case 3:
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().setAtomicNumber(0);
                for (int j = 0; j < 4; j++)
                {
                    GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addProton();
                    GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addElectron();
                }
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addProton();
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addNeutron();
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addNeutron();
                break;

            // Question: "Create C (Carbon) with a mass of 10 and a charge of -1." //charge = -1, mass = 10, protons = 6
            case 4:
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().setAtomicNumber(0);
                for (int k = 0; k < 6; k++)
                {
                    GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addProton();
                    GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addElectron();
                }
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addElectron();
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addNeutron();
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addNeutron();
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addNeutron();
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().addNeutron();
                break;

            // Question: "Create the stable version of Nitrogen with a charge of 0." //charge = 0, mass = 14, protons = 7
            case 5:
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().setAtomicNumber(7);
                GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().showElement(7);
                break;

            default:
                break;
        }

    }

    // Checks to see if the atomic structure model has the correct charge, mass, and proton count
    private bool modelCorrect()
    {
        if(GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().getAtomicNumber() == protons)
        {
            if (GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().getCharge() == charge)
            {
                if (GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().getAtomicMass() == mass)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void nextQuestion()
    {
        // Display the next question if the user is not submitting the last question.
        if (questionNumber < 11)
        {
            setArrayNum();
            displayQuestion();
        }

        // The user is submitting question 10.
        else
        {
            Debug.Log("Quiz ended.");
            stopQuiz();
        }
    }

    // Is called when submit button is pressed.
    public void submitAnswer() {

        // If the model is correctly made on one of the model based questions (questions 0-5).
        if ((modelCorrect() == true) || (submitAnswerText.text == "Next Question"))
        {
            GameObject.Find("ClickSound").GetComponent<ClickSound>().playClick();
            score += 1;
            nextQuestion();
        }

        // If the model is incorrect
        else
        {
            GameObject.Find("Error").GetComponent<ErrorSound>().playError();
            nextQuestion();
        }

    }

    // Used to set up buttons for a multiple choice questions
    private void multipleChoiceQuestion() {

        // Disable submit button and show solution buttons
        submitButton.gameObject.SetActive(false);
        seeSolutionButton.gameObject.SetActive(false);

        // Enable all answer choice buttons
        answerChoice1.gameObject.SetActive(true);
        answerChoice2.gameObject.SetActive(true);
        answerChoice3.gameObject.SetActive(true);
        answerChoice4.gameObject.SetActive(true);

        // Set all buttons text to the answer choices given
        answerText1.text = answerChoices[0];
        answerText2.text = answerChoices[1];
        answerText3.text = answerChoices[2];
        answerText4.text = answerChoices[3];
    }

    // Used to set up buttons for a mode based questions
    private void modelBasedQuestion() {
        // Disable all answer choice buttons
        answerChoice1.gameObject.SetActive(false);
        answerChoice2.gameObject.SetActive(false);
        answerChoice3.gameObject.SetActive(false);
        answerChoice4.gameObject.SetActive(false);

        // Enable submit and see solution buttons
        submitButton.gameObject.SetActive(true);
        seeSolutionButton.gameObject.SetActive(true);
    }

    // Called when an answer choice button is pressed
    public void answerChoicePressed(int choice) {

        // If answer selected is incorrect
        if (choice != correctAnswer)
        {
            GameObject.Find("Error").GetComponent<ErrorSound>().playError();
            nextQuestion();
        }
        else
        {
            GameObject.Find("ClickSound").GetComponent<ClickSound>().playClick();

            // Pick next question.
            score += 1;
            nextQuestion();
        }

    }

    // Chooses a random element and displays it on the atomic structure model.
    private void chooseRandomElement()
    {
        int element = Random.Range(0, 10);
        GameObject.Find("AtomicStructure").GetComponent<AtomicStructureBaseClass>().showElement(element + 1);


        int i = 0;
        // Generates four random elements for the array
        while (i < 4)
        {
            int choice = Random.Range(0, 10);
            
            for (int j = 0; j < 4; j++)
            {
                // Checks if random element is already in the array or is the answer choice
                if ((elements[choice] == answerChoices[j]) || (choice == element))
                {
                    break;
                }
                // Add random element to the array
                else if (j == 3)
                {
                    answerChoices[i] = elements[choice];
                    i++;
                    break;
                }
            }
        }

        // Sets array of choices to the correct answer
        answerChoices[correctAnswer - 1] = elements[element];

        return;
    }

    // Once the user selects Stop Quiz button
    public void stopQuiz()
    {

        // Disable all answer choice buttons
        answerChoice1.gameObject.SetActive(false);
        answerChoice2.gameObject.SetActive(false);
        answerChoice3.gameObject.SetActive(false);
        answerChoice4.gameObject.SetActive(false);
        seeSolutionButton.gameObject.SetActive(false);

        // Stop timer
        GameObject.Find("TimerText").GetComponent<QuizTimer>().stopTime();

        // Remove any questions shown
        questionText.text = "Quiz complete!\nScore: " + score + "/10";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
