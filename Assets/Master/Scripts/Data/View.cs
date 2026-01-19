using System;
using UnityEngine;

public enum ViewID
{
    Home,
    SC01,
    SC02,
    SC03,
}

[Serializable]
public class View
{
    public ViewID ViewID;
    public GameObject ViewObject;
}

