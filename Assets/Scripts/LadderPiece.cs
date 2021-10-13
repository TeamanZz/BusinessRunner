using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LadderPiece : MonoBehaviour
{
    private LadderGenerator ladderGenerator;
    private LadderReconstructor ladderReconstructor;
    public bool wasStepped;
    public bool isQuestion;
    public Tween tween;

    public Material defaultMaterial;

    private Color defaultColor;
    public Color stepColor;

    public List<Material> moneyMaterials = new List<Material>();

    private void Awake()
    {
        ladderGenerator = transform.parent.GetComponent<LadderGenerator>();
        defaultColor = transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
    }

    public void MakePieceAsQuestion()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material = moneyMaterials[0];
        transform.GetChild(1).GetComponent<MeshRenderer>().material = moneyMaterials[1];
        transform.GetChild(2).GetComponent<MeshRenderer>().material = moneyMaterials[2];
        transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
        transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.white;
        transform.GetChild(2).GetComponent<MeshRenderer>().material.color = Color.white;
        isQuestion = true;
    }

    public void MakePieceAsNotQuestion()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultMaterial;
        transform.GetChild(1).GetComponent<MeshRenderer>().material = defaultMaterial;
        transform.GetChild(2).GetComponent<MeshRenderer>().material = defaultMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<LadderReconstructor>(out ladderReconstructor))
        {
            ladderGenerator.BuildNewLadderPiece();
        }
        else if (!wasStepped)
        {
            tween = transform.DOMoveY(transform.position.y - 0.15f, 0.8f);

            wasStepped = true;

            if (isQuestion)
            {
                TapHandler.Instance.CanMoveOf();
                Manager.Instance.CreateQuestions();
                // QuestionPanel.Instance.OpenQuestionPanel();
                isQuestion = false;
            }
            else
            {
                transform.GetChild(0).GetComponent<MeshRenderer>().material.color = stepColor;
                transform.GetChild(1).GetComponent<MeshRenderer>().material.color = stepColor;
                transform.GetChild(2).GetComponent<MeshRenderer>().material.color = stepColor;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Main Character")
        {
            tween = transform.DOMoveY(transform.position.y + 0.15f, 1);
            transform.GetChild(0).GetComponent<MeshRenderer>().material.color = defaultColor;
            transform.GetChild(1).GetComponent<MeshRenderer>().material.color = defaultColor;
            transform.GetChild(2).GetComponent<MeshRenderer>().material.color = defaultColor;
        }
    }

    public void PauseTween()
    {
        tween.Pause();
    }

    public void PlayTween()
    {
        tween.Play();
    }
}