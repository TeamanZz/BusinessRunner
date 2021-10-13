using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question Person", menuName = "Info")]
public class QuestionDATA : ScriptableObject
{
    public int numberOfQuestions;
    public List<Question> questions = new List<Question>();
}