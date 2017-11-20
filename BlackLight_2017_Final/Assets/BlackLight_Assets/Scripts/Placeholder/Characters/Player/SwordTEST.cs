﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
public class SwordTEST : MonoBehaviour
{
    //----------------------------------------------------------------------------------------------------
    // Sets up references to other objects and creates variables
    //----------------------------------------------------------------------------------------------------

    public TrailRenderer Trails;
    public Enemy EnemyScript;
    
    public RangedEnemy RangedEnemyScript;
    public AudioSource swingsound;
    public GameObject Sword;
    // public Animation SwingWeapon;
    public Material[] material;
    public MeshRenderer rend;
    // the damage of the sword 
    public float fDamage = 15.0f;
    // the amount of time until the sword can attack again
    public float m_fAttackTimer;
	public float m_fCoolDownTime;

	public float m_fSwordColorTimer;
    public float m_fSwordColorCoolDown;
    public float m_fSwordDelay;
    public float m_fColorChange = 0.1f;
    //public float m_fAttackTime; 
    private bool m_bSwordColor = false;
    public bool m_bAttacking = false;
    public bool m_bRangedAttacking = false;

	private bool m_bClicked = false;
	private bool m_bDamage = false;
	public float m_fAttackDelay;
	public float m_fAttackTimerDelay;

	void start()
    {
        rend = gameObject.GetComponent<MeshRenderer>();
        rend.sharedMaterial = material[1];
        gameObject.GetComponent<TrailRenderer>().enabled = false;
    }


    private bool isAttacking = false;
  //  private float swordDamageTimer = 0.0f;
  //  private float swordDamageDelay = 0.0f;

//   public void StartAttack(float a_delay)
//   {
//       //you wait 0.5 seconds and then add damage
//       isAttacking = true;
//       swordDamageDelay = a_delay;
//       swordDamageTimer = 0.0f;
//
//   }

    //----------------------------------------------------------------------------------------------------
    // FixedUpdate is called once per frame, this function allows the player to attack using the xboxcontroller.
    // When the player attacks an enemy, the enemy takes damage. 
    //----------------------------------------------------------------------------------------------------
    private void FixedUpdate()
    {
        //  if(isAttacking)
        //  {
        //      swordDamageTimer += Time.fixedDeltaTime;
        //      
        //      if(swordDamageTimer > swordDamageDelay)
        //      {
        //          //apply the damage
        //          Debug.Log("DAMAGING!");
        //		if(EnemyScript)
        //			EnemyScript.TakeDamage(fDamage);
        //		if (RangedEnemyScript)
        //			RangedEnemyScript.TakeDamage(fDamage);
        //		isAttacking = false;
        //      }
        //  }



		///  Sword.GetComponent<Renderer>().material.color = Color.gray;

		// the attacktime is deducted by delta time 
		m_fAttackTimer += Time.deltaTime;
        m_fSwordColorTimer += Time.deltaTime;
        
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    m_bSwordColor = true;

        //}
        //  if (XCI.GetButtonUp(XboxButton.X) || Input.GetKeyUp(KeyCode.Mouse0))
        //  {
        //      gameObject.GetComponentInChildren<TrailRenderer>().enabled = false;
        //
        //  }
        if (XCI.GetButtonDown(XboxButton.X) || Input.GetKeyDown(KeyCode.Mouse0))
        {
			m_bClicked = true;
            if (m_fSwordColorTimer >= m_fSwordColorCoolDown)
            {
                m_bSwordColor = true;
                ColorChange();
                m_fSwordColorTimer = 0;
            }
            
            //  swingsound.Play();

            //          Sword.GetComponent<Renderer>().material.color = Color.red;
            // when the attack time is zero the player can attack 
            if (m_bAttacking && m_fAttackTimer >= m_fCoolDownTime)
            {
				if (m_bDamage)
				{
					//   enemy takes damage 
					//EnemyScript.TakeDamage(fDamage);
					// reset timer
					m_fAttackTimer = 0;
					m_fAttackTimerDelay = 0;
					m_bDamage = false;
				}
				//  m_bSwordColor = true;
				//   SwingWeapon.Play(); 
            }
            // when the attack time is zero the player can attack 
            if (m_bRangedAttacking && m_fAttackTimer >= m_fCoolDownTime)
            {
				if (m_bDamage)
				{
					// enemy takes damage 
					//RangedEnemyScript.TakeDamage(fDamage);
					// reset timer
					m_fAttackTimer = 0;
					m_fAttackTimerDelay = 0;
					m_bDamage = false;
				}
				// m_bSwordColor = true;
				// Sword.GetComponent<Renderer>().material.color = Color.red;
            }
        }
		else if(m_fAttackTimerDelay >= 1.0f)
		{
			m_bClicked = false;
		}

		if (m_fAttackTimerDelay >= m_fAttackDelay)
		{
			m_bDamage = true;
		}

			if (m_bClicked)
		{
			m_fAttackTimerDelay += Time.deltaTime;
		}
		if (!m_bClicked)
		{
			m_fAttackTimerDelay = 0.0f;
		}

        //Sword.GetComponent<Renderer>().material.color = Color.gray;
        if (m_bSwordColor == true)
        {
            ///    Sword.GetComponent<Renderer>().material.color = Color.red;
            gameObject.GetComponentInChildren<GameObject>().SetActive(true);

            rend.sharedMaterial = material[1];
            m_fColorChange -= Time.deltaTime;
        }
        if (m_fColorChange <= 0)
        {
            m_bSwordColor = false;
            ///     Sword.GetComponent<Renderer>().material.color = Color.grey;
            gameObject.GetComponentInChildren<GameObject>().SetActive(false);

            rend.sharedMaterial = material[0];

            m_fColorChange += 0.1f;
        }
		
    }
    public void ColorChange()
    {
      //if (m_bSwordColor == true)
      //{
      //      /    Sword.GetComponent<Renderer>().material.color = Color.red;

      //          rend.sharedMaterial = material[1];
      //          m_fColorChange -= Time.deltaTime; 
      //}
      if (m_fColorChange <= 0)
      {
                m_bSwordColor = false;
           ///     Sword.GetComponent<Renderer>().material.color = Color.grey;

                rend.sharedMaterial = material[0];

                m_fColorChange += 0.1f;
      }
    }
    //----------------------------------------------------------------------------------------------------
    // OnTriggerEnter checks if the player is collided with another gameobject, if enemies exist in the game
    // the player can collide and do damage to the enemies. 
    //----------------------------------------------------------------------------------------------------
    private void OnTriggerStay(Collider other)
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
                m_bAttacking = true;
            }
            // else the player cannot attack the enemy
            else
                m_bAttacking = false;
        }
        else
            m_bAttacking = false;

        RangedEnemyScript = other.GetComponent<RangedEnemy>();
        if (RangedEnemyScript != null)
        {
            // if the player hits the enemy is logged
            Debug.Log("HitEnemy");
            // if the enemies health is above 0 the player can still attack
            if (RangedEnemyScript.m_fHealth > 0)
            {
                m_bRangedAttacking = true;
            }
            // else the player cannot attack the enemy
            else
                m_bRangedAttacking = false;
        }
        else
            m_bRangedAttacking = false;
    }
        
   
	private void OnTriggerExit()
	{
		m_bAttacking = false;
		m_bRangedAttacking = false;
		EnemyScript = null;
		RangedEnemyScript = null;
	}
}
