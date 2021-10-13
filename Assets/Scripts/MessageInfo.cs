using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Message Info", menuName = "Info")][Serializable]
public class MessageInfo: ScriptableObject
{
    public string Title;
    public List<Message> messages = new List<Message>();
}
