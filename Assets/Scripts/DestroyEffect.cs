using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour {

    public int destroytime;
    
    // Use this for initialization
	void Start () {

        Destroy(gameObject, destroytime);

	}
	
}
