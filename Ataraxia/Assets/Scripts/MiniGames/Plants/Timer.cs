﻿using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour 
{
	[SerializeField]
	private bool isDecreaseTime = true;
	[SerializeField]
	private AtaraxiaText label;
	[SerializeField]
	private float totalTime = 59.0F;
	private float currentTime = 0.0F;
	private float descountTime = 1.0F;
	private float storedTime = 0.0F;

	public bool IsTimeOver
	{
		get{ return (int)storedTime == (int)totalTime+1;}
	}

	public float TotalTime
	{
		get{return totalTime;}
	}

	public void StartTimer ()
	{
		if(isDecreaseTime)
			currentTime = totalTime;
		CreateDelay ();
	}

	private void CreateDelay ()
	{
		storedTime = Time.realtimeSinceStartup + descountTime;
	}

	private void Update ()
	{
		if(IsTimeOver)
		{
			label.Text = string.Empty;
			return;
		}
		else if(Time.realtimeSinceStartup > storedTime)
		{
			ManageTime ();
			CreateDelay ();
			label.Text = currentTime.ToString ();
		}	
	}

	private void ManageTime ()
	{
		if(isDecreaseTime)
			currentTime -= descountTime;
		else 
			currentTime += descountTime;
	}
}