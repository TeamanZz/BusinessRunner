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

    private Color defaultColor;
    public Color stepColor;

    public GameObject dollarsParticles;

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
        isQuestion = true;
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
                dollarsParticles.SetActive(true);
                TapHandler.Instance.CanMoveOf();
                QuestionPanel.Instance.OpenQuestionPanel();
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