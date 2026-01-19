using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ElementData
{
    public string atomicNumber;
    public string symbol;
    public string name;
    public string atomicMass;
    public List<int> shells; 
}
[System.Serializable]
public class FullDElementDetails
{
    public string stt;
    public string name;
    public string symbol;
    public string type;
    public string method;
    public string atomicNumber;
    public string atomicMass;
    public string period;
    public string group;
    public string imagePath;
}
