﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SceneDialogType
{
	Welcome = 0
}

public abstract class TakeManagerBase : MonoBehaviour 
{
	[SerializeField]
	protected SceneDialogType sceneDialogType;
	[SerializeField]
	public List<TakeScene> takes;
	protected int currentTake;
	protected bool IsTakeOver
	{
		get{ return currentTake >= takes.Count; }
	}

	public bool IsTakeActive
	{
		get;
		private set;
	}

	protected TakeScene CurrentScene
	{
		get{ return takes[currentTake];}
	}

	protected virtual void StartScene ()
	{
		IsTakeActive = true;
		StartCurrentTake ();
	}

	protected virtual void StartCurrentTake ()
	{
		CurrentScene.gameObject.SetActive (true);
		CurrentScene.StartScene (CheckForNextTake,CameraManager.Instance);
	}

	protected void CheckForNextTake () 
	{
		currentTake++;
		
		if (IsTakeOver)
			EndScene();
		else
			StartCurrentTake();
	}

	protected void FinishActivity ()
	{
		IsTakeActive = false;
	}

	protected abstract void EndScene ();
}