using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choosable
{
    /// <summary>
    /// The chance for this item to be selected
    /// </summary>
    public float chance;

    /// <summary>
    /// The item to be selected
    /// </summary>
    public GameObject obj;
}