using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public int minMoneyIncreaseValue;
    public int maxMoneyIncreaseValue;

    public TextMeshProUGUI moneyText;

    public Image progressBarFill;

    public TextMeshProUGUI currentWorkerPosition;
    public TextMeshProUGUI nextWorkerPosition;

    [Header("Data settings")]
    public DATA data;
    public QuestionDATA stage;
    public int PersonLevel = 0;

    [Header("Questions settings")]
    private QuestionWindows questionWindows;
    public int numberOfQuestions;
    public bool created = false;
    public List<GameObject> questions = new List<GameObject>();

    public void Awake()
    {
        Instance = this;
        questionWindows = FindObjectOfType<QuestionWindows>();
        stage = data.stages[0];
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
            StartCoroutine(LevelUp());
        else
            Demotion();
    }

    public void Demotion()
    {

        numberOfQuestions = stage.numberOfQuestions;

    }

    public IEnumerator LevelUp()
    {



        numberOfQuestions = stage.numberOfQuestions;

        moneyText.text = Random.Range(minMoneyIncreaseValue, maxMoneyIncreaseValue).ToString() + " $";

        var currentValue = progressBarFill.fillAmount;
        var newValue = progressBarFill.fillAmount + Random.Range(0.1f, 0.25f);
        DOTween.To(() => progressBarFill.fillAmount, x => progressBarFill.fillAmount = x, newValue, 0.5f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.5f);
        if (newValue >= 1)
        {
            progressBarFill.fillAmount = 0;
            currentWorkerPosition.text = "Ур. 2 Мл. Специалист";
            nextWorkerPosition.text = "Ур. 3 Специалист";
        }

    }

}