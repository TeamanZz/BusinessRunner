using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Question 
{
    public string question;
    public string trueText;

    public List<string> falseText = new List<string>();
}
