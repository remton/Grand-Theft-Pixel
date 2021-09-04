using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        GameEvents.instance.onCarStop += Stop;
    }

    private void Stop()
    {
        animator.SetTrigger("Stop");
    }

    private void OnDestroy()
    {
        GameEvents.instance.onCarStop -= Stop;
    }
}
