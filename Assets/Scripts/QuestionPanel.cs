using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionPanel : MonoBehaviour
{
    public static QuestionPanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    public QuestionWindows windows;

    public Color startColor;
    public Gradient colorGrad;
    public Button button;
    public TextMeshProUGUI questionText;
    public Image coloringImage;
    public Image youAnswer;
    public Sprite[] sprits;


    public bool answer;
    public bool active = false;

    public void Initialization(string question, bool significance, QuestionWindows wind)
    {
        youAnswer.sprite = sprits[2];

        Color color = new Vector4(0.1725f, 0.1725f, 0.1725f, 1);

        coloringImage.color = color;
        windows = wind;
        questionText.text = question;
        answer = significance;
        button.onClick.AddListener(() => windows.CheckTheAnswer(significance, this));
    }

    public void ConclusionColors()
    {
        if (active == true)
            return;

        if (answer == true)
        {
            //transform.localScale = new Vector3(1f, 1f, 1f);
            coloringImage.color = colorGrad.Evaluate(0);
            youAnswer.sprite = sprits[0];
            youAnswer.color = Color.white;
        }
        else
        {
            //transform.localScale = new Vector3(1f, 1f, 1f);
            coloringImage.color = colorGrad.Evaluate(100);
            youAnswer.sprite = sprits[1];
            youAnswer.color = Color.white;
        }
        button.IsDestroyed();
    }

    public void OutputWrongColor()
    {
        if (active == true)
            return;


        if (answer == true)
        {
            coloringImage.color = colorGrad.Evaluate(0);
            youAnswer.sprite = sprits[0];
        }

        button.IsDestroyed();
    }
}