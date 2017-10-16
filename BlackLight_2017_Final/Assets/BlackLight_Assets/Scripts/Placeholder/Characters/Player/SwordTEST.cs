﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput; 
public class SwordTEST : MonoBehaviour
{
    //----------------------------------------------------------------------------------------------------
    // Sets up references to other objects and creates variables
    //----------------------------------------------------------------------------------------------------
    Enemy EnemyScript;
    RangedEnemy RangedEnemyScript;
    public AudioSource swingsound;

    // the damage of the sword 
    public float fDamage = 15.0f;
    // the amount of time until the sword can attack again
    public float m_fAttackTime = 1.0f;

    private bool Attacking = false;
    private bool RangedAttacking = false;

    //----------------------------------------------------------------------------------------------------
    // FixedUpdate is called once per frame, this function allows the player to attack using the xboxcontroller.
    // When the player attacks an enemy, the enemy takes damage. 
    //----------------------------------------------------------------------------------------------------
    private void FixedUpdate()
    {
        // the attacktime is deducted by delta time 
        m_fAttackTime -= Time.deltaTime;
        if (XCI.GetButton(XboxButton.X))
        {
            // when the attack time is zero the player can attack 
            if (Attacking && m_fAttackTime <= 0)
            {
                // enemy takes damage 
                EnemyScript.TakeDamage(fDamage);
                // one is added to attack time to reset timer
                m_fAttackTime = 1;
            }
            // when the attack time is zero the player can attack 
            if (RangedAttacking && m_fAttackTime <= 0)
            {               
                // enemy takes damage 
                RangedEnemyScript.TakeDamage(fDamage);
                // one is added to attack time to reset timer
                m_fAttackTime = 1;
            }
        }
    }
    //----------------------------------------------------------------------------------------------------
    // OnTriggerEnter checks if the player is collided with another gameobject, if enemies exist in the game
    // the player can collide and do damage to the enemies. 
    //----------------------------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        // checks if colliding object has an enemy script or ranged enemyscript
        EnemyScript = other.GetComponent<Enemy>();
        if (EnemyScript != null)
        {
            // if the player hits the enemy is logged
            Debug.Log("HitEnemy");
            // if the enemies health is above 0 the player can still attack
            if (EnemyScript.m_fHealth > 0)
            {
                Attacking = true;
            }
            // else the player cannot attack the enemy
            else
                Attacking = false;
        }
        else
            Attacking = false;

        RangedEnemyScript = other.GetComponent<RangedEnemy>();
        if (RangedEnemyScript != null)
        {
            // if the player hits the enemy is logged
            Debug.Log("HitEnemy");
            // if the enemies health is above 0 the player can still attack
            if (RangedEnemyScript.m_fHealth > 0)
            {
                RangedAttacking = true;
            }
            // else the player cannot attack the enemy
            else
                RangedAttacking = false;
        }
        else
            RangedAttacking = false;
    }
}
