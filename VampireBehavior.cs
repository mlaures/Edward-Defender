using UnityEngine;
using System.Collections;

public class VampireBehavior : MonoBehaviour {

	public float moveSpeed;
	private Transform player;
	private int direction;
	public Animator die;
	public bool dead;
	public AudioSource splat;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		dead = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, player.position, moveSpeed * Time.deltaTime);
		if (transform.position.x < player.position.x) {
			direction = 1;
		}
		if (transform.position.x > player.position.x) {
			direction = -1;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Stake") {
			StartCoroutine (death());
		}
	}

	IEnumerator death () {
		die.SetBool ("dead", true);
		splat.Play ();
		yield return new WaitForSeconds (0.47f);
		Destroy (this.gameObject);
	}
}
