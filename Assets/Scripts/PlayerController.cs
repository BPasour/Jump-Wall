using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jump;
	public AudioSource jumpAudio;
	public AudioSource levelAudio;

	private Rigidbody rb;

	// Use this for initialization
	void Start () 

	{
		rb = GetComponent<Rigidbody> ();


	}
	void OnEnable()
	{
		levelAudio.Play ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Controls ();

	}
	void Controls ()
	{
		if (Input.GetKey ("a")) 
		{
			rb.AddForce (-speed, 0f, 0f);
		}
		if (Input.GetKey ("d")) 
		{
			rb.AddForce (speed, 0f, 0f);
		}
		if (Input.GetKey ("w")) 
		{
			rb.AddForce (0f, 0f, speed);
		}
		if (Input.GetKey ("s")) 
		{
			rb.AddForce (0f, 0f, -speed);
		}
		if (Input.GetKeyDown ("space")&&Mathf.Abs(rb.velocity.y)<.15)
		{
			rb.velocity = new Vector3 (rb.velocity.x, jump, rb.velocity.z);
			jumpAudio.Play ();
		}
	}

}
