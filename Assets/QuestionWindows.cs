using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionWindows : MonoBehaviour
{
    public GameObject content;
    public GameObject closedBut;
    [HideInInspector] public Manager manager;

    [Header("Instatiate settings")]
    public GridLayoutGroup group;
    public GameObject questIconPrefab;
    public TextMeshProUGUI questText;

    public List<QuestionPanel> panels = new List<QuestionPanel>();
    public void Awake()
    {
        closedBut.SetActive(false);
        manager = FindObjectOfType<Manager>();
        content.SetActive(false);
        //group = FindObjectOfType<GridLayoutGroup>();
    }

    public void Open()
    {
        content.SetActive(true);
    }

    public void Initialization(int number, Question question)
    {
        Debug.Log("Initialization" + number + question);
        questText.text = question.question;

        GameObject truepanel = Instantiate(questIconPrefab, group.transform.parent);
        truepanel.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        QuestionPanel pan = truepanel.GetComponent<QuestionPanel>();
        pan.Initialization(question.trueText, true, this);
        truepanel.transform.parent = group.transform;
        panels.Add(pan);
        RandomizeFalseVariant(number, question);

    }

    public void RandomizeFalseVariant(int number, Question question)
    {
        Debug.Log(number + " = number");
        List<string> falseAnswer = new List<string>(question.falseText);
        for (int i = 0; i < number - 1; i++)
        {
            GameObject questPanel = Instantiate(questIconPrefab, group.transform.parent);
            questPanel.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
            QuestionPanel que = questPanel.GetComponent<QuestionPanel>();

            int variant = Random.Range(0, falseAnswer.Count);
            que.Initialization(falseAnswer[variant], false, this);
            panels.Add(que);
            falseAnswer.Remove(falseAnswer[variant]);

            questPanel.transform.parent = group.transform;

            Invoke("RandomizePosition", 0.1f);
        }

        Invoke("RandomizePosition", 0.01f);
    }

    public void RandomizePosition()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            Vector3 obj;
            int random = Random.Range(0, panels.Count);
            obj = panels[random].gameObject.transform.position;
            Debug.Log(panels[i].gameObject.transform.position);

            panels[random].gameObject.transform.position = panels[i].gameObject.transform.position;
            panels[i].gameObject.transform.position = obj;
            Debug.Log(panels[i].gameObject.transform.position + "" + i + 1);
        }
    }


    public void CheckTheAnswer(bool answer, QuestionPanel question)
    {
        if (answer == true)
            question.ConclusionColors();
        else
        {
            question.ConclusionColors();
            foreach (var pan in panels)
            {
                pan.OutputWrongColor();
            }
        }
        manager.Answer(answer);
        closedBut.SetActive(true);
    }

    public void CloseWindows()
    {
        foreach (var pan in panels)
        {
            Destroy(pan.gameObject);
        }
        panels.Clear();
        manager.created = false;
        closedBut.SetActive(false);
        content.SetActive(false);
    }
}
