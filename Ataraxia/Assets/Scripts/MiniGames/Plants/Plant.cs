﻿using System;
using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour 
{
	[SerializeField]
	private CharacterDectector characterDetector;
	[SerializeField]
	private MeshTransform meshTransform;
	[SerializeField]
	private int healRate = 10;
	[SerializeField]
	private int minLifeToStart = 30;
	[SerializeField]
	private int maxLifeToStart = 60;
	[SerializeField]
	private int minLife = 0;
	[SerializeField]
	private int maxLife = 100;
	[SerializeField]
	private float delayToDeteriorate = 3.0F;
	[SerializeField]
	private int deteriorateRate = 7;
	private float timeToDeteriorate;
	private int life;
	private Transform myTransform;

	public Action Deteriorated;
	public Action Healed;
	public Action<Vector3,Plant> GetWateringPosition;

	public Vector3 Position
	{
		get{ return myTransform.position;}
	}

	public float CurrentLife
	{
		get;
		private set;
	}

	public int MinLife
	{
		get{ return minLifeToStart; }
	}

	public int MaxLife
	{
		get{ return maxLife; }
	}

	public int Life
	{
		get{ return life; }
	}

	private void Awake ()
	{
		life = UnityEngine.Random.Range(minLife,maxLife);
	}

	private void Start ()
	{
		myTransform = transform;
		timeToDeteriorate = Time.realtimeSinceStartup + delayToDeteriorate;
		ChangeMorp ();
	}

	private void Update ()
	{
		if(Time.realtimeSinceStartup > this.timeToDeteriorate)
		{
			timeToDeteriorate = Time.realtimeSinceStartup + delayToDeteriorate;
			Deteriorate ();
			ChangeMorp ();
		}
	}

	private void Deteriorate ()
	{
		life-=deteriorateRate;
		if(life < minLife)
			life = minLife;

		if(Deteriorated != null)
			Deteriorated ();
	}

	private void ChangeMorp ()
	{
		CurrentLife = (float)life / (float)maxLife;
		meshTransform.SetMorph( CurrentLife );
	}

	private void Heal ()
	{
		life += healRate;
		if(life > maxLife)
			life = maxLife;

		if(Healed != null)
			Healed ();
	}

	private void OnMouseDown ()
	{
		if (characterDetector.CanWatering)
			Recovery ();
		else if(GetWateringPosition != null)
			GetWateringPosition( characterDetector.Position,this );
	}

	private void Recovery ()
	{
		Heal ();
		ChangeMorp ();
	}
}