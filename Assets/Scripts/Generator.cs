using UnityEngine;

public class Generator : MonoBehaviour {

	public GameObject [] blocks;
	public float spacing;
	public GameObject player;
	public Material wall;
	public Material platform;

	private Rigidbody rb;
	private Vector3 startPos;
	private GameObject [] wallblocks; 
	// Use this for initialization
	void Start () 
	{
		startPos = player.transform.position;
		rb = player.GetComponent<Rigidbody> ();
		Generate ();
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		/*
		if (player.transform.position.y > 50f) 
		{
			Eliminate ();

			Generate ();
		}
*/
	}

	public void Generate ()
	{
		for (int y = 0; y < 16; y++)

			for (int i = 0; i < 200/spacing; i++) 
			{
				if((y%2)==0)
					Instantiate (blocks[Random.Range(0,blocks.Length)], new Vector3 (i * spacing +spacing/3  - 95f, 3f*(y+1f), -1f), Quaternion.identity);
				else if ((y%3)==0)
					Instantiate (blocks[Random.Range(0,blocks.Length)], new Vector3 (i * spacing -spacing/3 - 95f, 3f*(y+1f), -1f), Quaternion.identity);					
				else		
					Instantiate (blocks[Random.Range(0,blocks.Length)], new Vector3 (i * spacing -spacing - 95f, 3f*(y+1f), -1f), Quaternion.identity);					
				
			}
		wallblocks = GameObject.FindGameObjectsWithTag ("block");
		for (int i = 0; i < wallblocks.Length; i++) 
		{
			if (wallblocks[i].transform.position.x>97.5f||wallblocks[i].transform.position.x<-97.5f)
				Destroy(wallblocks[i]);
		}
	}
	public void Eliminate ()
	{
		for (int i = 0; i < wallblocks.Length; i++) 
		{
			Destroy (wallblocks [i]);
		}
		player.transform.position = startPos;
		rb.velocity = new Vector3 (0, 0, 0);
		spacing++;
		wall.color = Random.ColorHSV (0f, .2f, .4f, .6f, 0.1f, .5f);
		platform.color = Random.ColorHSV (0f, .2f, .6f, .8f, 0.1f, .5f);
	}
}
