using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Car car;
    private Weapon weapon;
    private Animator animator;
    private AnimatorOverrideController animatorOverride;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        //sets variables
        car = PlayerData.instance.activeCar;
        weapon = PlayerData.instance.activeWeapon;
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //Sets the animations for the car
        animatorOverride = car.animOverride;
        animator.runtimeAnimatorController = animatorOverride;
        
    }
}
