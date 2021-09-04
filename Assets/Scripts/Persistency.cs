using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistency : MonoBehaviour
{
    static List<int> ids = new List<int>();

    public int id;
    public bool isFirst;

    private void Awake()
    {
        if (ids.Contains(this.id) && !isFirst)
            Destroy(this.gameObject);
        else
        {
            isFirst = true;
            ids.Add(this.id);
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
