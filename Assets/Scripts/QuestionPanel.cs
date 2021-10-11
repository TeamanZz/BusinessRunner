using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPanel : MonoBehaviour
{
    public static QuestionPanel Instance;
    public GameObject questionPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void CloseQuestionPanel()
    {
        questionPanel.SetActive(false);
    }

    public void OpenQuestionPanel()
    {
        questionPanel.SetActive(true);
    }
}
