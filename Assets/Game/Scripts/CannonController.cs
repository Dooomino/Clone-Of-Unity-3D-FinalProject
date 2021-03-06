﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject player;

    private GameObject stand;
    private GameObject outerBarrelLeft;
    private GameObject innerBarrelLeft;

    private GameObject muzzleFlashLeft;

    private GameObject outerBarrelRight;
    private GameObject innerBarrelRight;
    
    private GameObject muzzleFlashRight;
    private GameObject fireLocation;
    private Animator animatorController;
    public LayerMask playerLayer;
    private bool foundPlayer = false;

    public GameObject muzzleFlash;
    public GameObject playerExplosion;
    public GameObject firingLocation;
    public GameObject firingOrigin;
    // Start is called before the first frame update
    void Start()
    {
        stand = this.transform.GetChild(0).gameObject;
        outerBarrelLeft = stand.transform.GetChild(0).gameObject;
        innerBarrelLeft = outerBarrelLeft.transform.GetChild(0).gameObject;
        muzzleFlashLeft = outerBarrelLeft.transform.GetChild(1).gameObject;


        outerBarrelRight = stand.transform.GetChild(1).gameObject;
        innerBarrelRight = outerBarrelRight.transform.GetChild(0).gameObject;
        muzzleFlashRight = outerBarrelRight.transform.GetChild(1).gameObject;

        animatorController = this.gameObject.GetComponent<Animator>();
        
    }

    public void fire(){
        //Make explosions in front of the cannon
        Instantiate(muzzleFlash, muzzleFlashLeft.transform);
        Instantiate(muzzleFlash, muzzleFlashRight.transform);


        Instantiate(playerExplosion, player.transform.position, Quaternion.identity);
        player.GetComponent<Stats>().takeDamage(50);
    }
    void FixedUpdate(){
        foundPlayer = false;

        //Check if the cannon can see the layer
        RaycastHit hitInfo;
        if(Physics.SphereCast(firingOrigin.transform.position, 5.0f, firingLocation.transform.up, out hitInfo, 45, playerLayer)){
            Debug.DrawLine(firingOrigin.transform.position,  hitInfo.transform.position,  Color.green);
            foundPlayer = true;
            
            
        }else{
            Debug.DrawLine(firingOrigin.transform.position,  firingLocation.transform.up* 45, Color.red);
        }
        animatorController.SetBool("foundEnemy", foundPlayer);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
