using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    public static Manager Instance;

    [Header("Data settings")]
    public DATA data;
    public QuestionDATA stage;
    public int PersonLevel = 0;

    [Header("Questions settings")]
    private QuestionWindows questionWindows;
    public int stageNumber;
    public int numberOfQuestions;
    public bool created = false;
    public List<GameObject> questions = new List<GameObject>();

    public void Awake()
    {
        Instance = this;
        questionWindows = FindObjectOfType<QuestionWindows>();
        stageNumber = 0;
        stage = data.stages[stageNumber];
        numberOfQuestions = stage.numberOfQuestions;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1) && created == false)
        {
            CreateQuestions();
        }
    }

    public void Play()
    {
        if (created == false)
            CreateQuestions();
    }

    public void CreateQuestions()
    {
        created = true;
        int questNumber = Random.Range(0, stage.questions.Count);
        questionWindows.Open();
        Debug.Log(questNumber);
        Debug.Log(stage.questions.Count);
        questionWindows.Initialization(numberOfQuestions, stage.questions[questNumber]);

    }

    public void Answer(bool answer)
    {
        if (answer == true)
            LevelUp();
        else
            Demotion();
    }

    public void Demotion()
    {
        if (stageNumber > 0)
        {
            stageNumber--;
            stage = data.stages[stageNumber];
            numberOfQuestions = stage.numberOfQuestions;
        }

    }

    public void LevelUp()
    {
        if (stageNumber < data.stages.Count - 1)
        {
            stageNumber++;
            stage = data.stages[stageNumber];
            numberOfQuestions = stage.numberOfQuestions;
        }
    }
}
