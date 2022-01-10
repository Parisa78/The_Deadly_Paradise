using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//serializer
public class Dialogue 
{
    public string name;
    public string[] sentences;

    public Dialogue(string name, string[] sentences)
    {
        this.name = name;
        this.sentences = sentences;
    }


}
