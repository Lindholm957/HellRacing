using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Game.GamePlay;
using Multiplayer;
using Photon.Pun;
using UnityEngine;

/// <summary>
/// Base class game controller.
/// </summary>
public class GameController :MonoBehaviour
{
	[SerializeField] private List<StartPosition> _startPositions = new List<StartPosition>();
	public static GameController Instance;
	public static bool RaceIsStarted { get { return true; } }
	public static bool RaceIsEnded { get { return false; } }

	public List<StartPosition> StartPositions => _startPositions;

	List<CarController> Cars = new List<CarController>();
	int CurrentCarIndex = 0;

	protected virtual void Awake ()
	{
		Instance = this;
		
		//Find all cars in current game.
		Cars.AddRange (GameObject.FindObjectsOfType<CarController> ());
		Cars = Cars.OrderBy (c => c.name).ToList();

		foreach (var car in Cars)
		{
			var userControl = car.GetComponent<UserControl>();
			var audioListener = car.GetComponent<AudioListener>();

			if (userControl == null)
			{
				userControl = car.gameObject.AddComponent<UserControl> ();
			}

			if (audioListener == null)
			{
				audioListener = car.gameObject.AddComponent<AudioListener> ();
			}

			userControl.enabled = false;
			audioListener.enabled = false;
		}
	}
}
