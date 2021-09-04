using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour {

    private bool isActive = true;
    public void SetActive(bool b) { isActive = b; }

    public float ProjectileOffsetX;
    public float ProjectileOffsetY;
    public GameObject ammoUI;
    public Animator ammoAnimator;
    private Text ammoTxt;
    
    private Weapon weapon;
    private Animator animator;

    private int inMag;
    private int magSize;
    private float reloadTime;
    private float fireDeley;

    public float maxAnimMult;
    public float minAnimMult;
    private bool isGunOut = false;


    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        weapon = PlayerData.instance.activeWeapon;
        ammoTxt = ammoUI.GetComponentInChildren<Text>();

        if (weapon == null || weapon.weaponName == "Unarmed")
        {
            fireReady = false;
            UpdateHud();
            return;
        }

        inMag = weapon.magSize;
        magSize = weapon.magSize;
        reloadTime = 10 * 1 / weapon.reloadSpeed;
        fireDeley = 10 * 1 / weapon.fireRate;



        //This requires consistant file names. Every car should have its shoot animation named as [car name]_HandgunLoop
        float attackAnimLength = animator.GetClipLength(PlayerData.instance.activeCar.name + "_" + "HandgunLoop");
        float animMult = 1 / (fireDeley / attackAnimLength);
        if(animMult > maxAnimMult)
        {
            animator.SetFloat("AttackSpeed", maxAnimMult);
        }
        else if(animMult < minAnimMult)
        {
            animator.SetBool("OverrideLoopHandgun", true);
        }
        else
            animator.SetFloat("AttackSpeed", animMult);

        UpdateHud();
    }

    void Update ()
    {
        if (!isActive)
            return;

        if (inMag <= 0)
        {
            isReloading = true;
            Reload();
        }

        if (Input.GetButton("Shoot") && !isReloading)
        {
            if (fireReady)
            {
                animator.SetBool("DoLoopHandgun", true);
                if (isGunOut && fireReady)
                {
                    fireReady = false;
                    Shoot();
                }
            }
        }
        else
        {
            animator.SetBool("DoLoopHandgun", false);
        }
    }

    //Called by animation events
    void OnHandgunOut()
    {
        isGunOut = true;
        fireReady = false;
        Shoot();
    }
    void OnHandgunIn()
    {
        isGunOut = false;
        animator.SetBool("DoLoopHandgun", false);
    }


    private void UpdateHud()
    {
        ammoAnimator.SetBool("doFlash", (!fireReady || isReloading));
        ammoTxt.text = inMag.ToString() + "/ " + magSize.ToString();
    }

    bool fireReady = true;
    private void Shoot()
    {
        StartCoroutine(AttackWait());
        inMag -= 1;
        Vector3 projPos = gameObject.transform.position;

        switch (weapon.type)
        {
            case WeaponType.Unarmed:
                break;
            case WeaponType.Handgun:
                Instantiate(weapon.projectile, new Vector3(projPos.x + ProjectileOffsetX, projPos.y + ProjectileOffsetY, projPos.z), Quaternion.identity);
                break;
            case WeaponType.Shotgun:
                Quaternion rotation = Quaternion.identity;
                int bulletCount = 4;
                float spread = 15f;
                float bulletAngle = -spread;
                for (int i = 1; i <= bulletCount; i++)
                {
                    rotation.eulerAngles = new Vector3(0, 0, -bulletAngle);
                    Instantiate(weapon.projectile, new Vector3(projPos.x + ProjectileOffsetX, projPos.y + ProjectileOffsetY, projPos.z), rotation);
                    bulletAngle += 2 * spread / (bulletCount-1);
                }
                break;
            case WeaponType.Uzi:
                Instantiate(weapon.projectile, new Vector3(projPos.x + ProjectileOffsetX, projPos.y + ProjectileOffsetY, projPos.z), Quaternion.identity);
                break;
            default:
                Debug.LogError("WEAPON TYPE: " + weapon.type + " has not been implemented yet.");
                break;
        }
        StartCoroutine("AttackWait");
        UpdateHud();
    }
    IEnumerator AttackWait()
    {
        yield return new WaitForSeconds(fireDeley);
        
        fireReady = true;
        UpdateHud();
    }

    // Reload() is called whenever inMag is 0
    bool isReloading = false;
    public void Reload()
    {
        StartCoroutine("ReloadWait");
    }
    IEnumerator ReloadWait()
    {
        yield return new WaitForSeconds(reloadTime);
        inMag = magSize;
        isReloading = false;
        UpdateHud();
    }
}
