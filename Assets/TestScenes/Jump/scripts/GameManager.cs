using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Posix;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public Text scoreText;
	public Text highScoreText;
	public Text iterationText;

	private int score;
	public int Score {
		get {
			return score;
		}
		set {
			score = value;
			if (score > highScore) {
				HighScore = score;
			}
			scoreText.text = string.Format("{0:D5}", score);
		}
	}

	private int highScore;
	public int HighScore {
		get {
			return highScore;
		}
		set {
			highScore = value;
			highScoreText.text = string.Format("{0:D5}", highScore);
		}
	}

	private int iteration;
	public int Iteration {
		get {
			return iteration;
		}
		set {
			iteration = value;
			iterationText.text = string.Format("{0:D2}", iteration);
		}
	}

	public static GameManager Instance { get; private set; }

	void Awake () {
		Instance = this;
	}

	void Start (){
		Score = 0;
		HighScore = 0;
		Iteration = 0;
	}
}
