using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

	public float startSpacing;
	public Text countDown;
	public Text startText;
	public Text scoreText;
	public float startTime;
	public GameObject player;
	public GameObject levelManager;
	public AudioSource lowTime;
	public AudioClip tick;
	public AudioClip explosion;
	public AudioClip winBoing;

	private float currCountdownValue;
	private Vector3 startPos;
	private Rigidbody rb;
	private float score;
	private WaitForSeconds wait = new WaitForSeconds (2.0f);
	private WaitForSeconds wait2 = new WaitForSeconds (2.1f);
	private WaitForSeconds seconds = new WaitForSeconds (1.0f);
	private bool win = false;
	private int level = 1;
	private float highScore;
	private string highScoreText;
	private float timeLeft;

	// Use this for initialization
	void Start ()
	{
		startSpacing = GetComponent<Generator> ().spacing;
		timeLeft = 0;
		highScore = 0f;
		highScoreText = "";
		lowTime.clip = tick;
		startPos = player.transform.position;
		rb = player.GetComponent<Rigidbody> ();
		rb.isKinematic = true;
		player.gameObject.SetActive (false);
		score = 0f;
		countDown.text = "";
		startText.text = "";
		scoreText.text = "";
		StartCoroutine (StartText ());
	}
	// Update is called once per frame
	void LateUpdate ()
	{
		WinSet ();
		ScoreUpdate ();
	}

	IEnumerator StartText()
	{
		startText.text = "Welcome to\nExtreme Ledge Hopper!";
		yield return wait;
		startText.text = "Get to the Top\nBefore Time Runs Out!";
		yield return wait;
		startText.text = "Use WASD to Navigate\nUse Space to Jump\nUse Z to Zoom Out";
		yield return wait;
		startText.text = "";
		player.gameObject.SetActive (true);
		rb.isKinematic = false;
		StartCoroutine(StartCountdown());
	}

	IEnumerator StartCountdown()
	{
		currCountdownValue = startTime;
        lowTime.clip = tick;
		while (currCountdownValue >= 0 && win == false)
		{
			countDown.text = "Time Left: " + currCountdownValue.ToString () + "\nLevel: " + level.ToString ();
			yield return seconds;
			currCountdownValue--;
			if (currCountdownValue <= 10)
				lowTime.Play ();
		}

		if (win == true)
			StartCoroutine (Win ());
		if (currCountdownValue <= 0)
			StartCoroutine (Lose ());
	}
	IEnumerator Win()
	{
		StopCoroutine (StartCountdown ());
		timeLeft += currCountdownValue + 1;
		lowTime.clip = winBoing;
		lowTime.Play ();
		startText.text = "YOU\nWIN!";
		level++;
		GetComponent<Generator> ().Eliminate ();
		player.gameObject.SetActive (false);
		rb.isKinematic = true;
		yield return wait2;
		GetComponent<Generator> ().Generate ();
		startText.text = "Level: " + level.ToString ();
		yield return wait2;
		player.gameObject.SetActive (true);
		rb.isKinematic = false;
		currCountdownValue = startTime;
		startText.text = "";
		StartCoroutine(StartCountdown());
	}
	IEnumerator Lose()
	{
		StopCoroutine (StartCountdown ());
		startText.text = "YOU\nLOSE!";
		player.gameObject.SetActive (false);
		rb.isKinematic = true;
		lowTime.clip = explosion;
		lowTime.Play ();
		yield return wait;
		if (level >= 2)
		{
			GetComponent<Generator> ().Eliminate ();
			GetComponent<Generator> ().spacing = startSpacing;
			GetComponent<Generator> ().Generate ();
		}
		if (score > highScore)
		{
			highScore = score;
			startText.text = "New High Score!\n" + highScore.ToString ();
			highScoreText = "High Score: " + highScore.ToString ();
		}
		timeLeft = 0;
		level = 1;
        score = 0f;
		yield return wait;
		player.transform.position = startPos;
		startText.text = "Try Again";
		yield return wait;
		player.gameObject.SetActive (true);
		rb.isKinematic = false;
		currCountdownValue = startTime;
		startText.text = "";
        StartCoroutine(StartCountdown());
	}
	void WinSet()
	{
		if (player.transform.position.z >= 1f)
			win = true;
		else
			win = false;
	}
	void ScoreUpdate()
	{
		scoreText.text = "Score: " + score.ToString () + "\n" + highScoreText;
        if ((player.transform.position.y - 1) % 3 == 0)
		{
			score = ((level - 1) * 150) + timeLeft + (player.transform.position.y - 1) / 3 * 10;
			scoreText.text = "Score: " + score.ToString () + "\n" + highScoreText;
            return;
		}
		else
			scoreText.text = scoreText.text;
	}
}
