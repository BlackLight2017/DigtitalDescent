﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeScript : MonoBehaviour {
	//----------------------------------------------------------------------------------------------------
	// Sets up references to other objects.
	//----------------------------------------------------------------------------------------------------
	public Canvas PauseMenu;
	private SelectOnInput Select;
	public GameObject Resume;
	public GameObject Restart;
	public GameObject Quit;

	//----------------------------------------------------------------------------------------------------
	// Use this for initialization.
	//----------------------------------------------------------------------------------------------------
	void Start()
	{
		Select = PauseMenu.GetComponent<SelectOnInput>();
	}

	//----------------------------------------------------------------------------------------------------
	// Used to let objects move and makes the pause menu dissapear.
	//----------------------------------------------------------------------------------------------------
	public void ResumeGame()
	{
		if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
			PauseMenu.enabled = false;
			Resume.SetActive(false);
			Restart.SetActive(false);
			Quit.SetActive(false);
			Select.enabled = false;
		}
	}

}
