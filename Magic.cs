using UnityEngine;
using System.Collections;

public class Magic : MonoBehaviour {

	public int magicLife;
	public float magicSpeed;
	private Rigidbody2D myBody;
	private Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform> ();
		StartCoroutine (life ());
		myBody = GetComponent<Rigidbody2D> ();
		myBody.AddRelativeForce (Vector3.MoveTowards(transform.position, player.position, magicSpeed));
	}
	
	// Update is called once per frame
	void Update () {
		if (myBody.velocity.magnitude < 500) {
			myBody.AddRelativeForce ((transform.position - player.position) * -1 * magicSpeed);
				//Vector3.MoveTowards (transform.position, player.position, magicSpeed));
		}
	}

	IEnumerator life () {
		yield return new WaitForSeconds (magicLife);
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Destroy (this.gameObject);
		}
	}

}
