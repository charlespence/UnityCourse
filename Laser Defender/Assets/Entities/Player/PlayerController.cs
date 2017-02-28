using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float playerSpeed = 5.0f;
	public float padding = .5f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 0.15f;
	public float health = 250f;
	
	float xMin;
	float xMax;
	
	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
	}

	void Update () {
		CheckForFire();
		CheckForMovement();
	}
	
	void Fire(){
		Vector3 startPosition = transform.position + new Vector3(0, .55f, 0);
		GameObject beam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0, projectileSpeed, 0);
	}
		
	void CheckForFire(){
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.0001f, firingRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
	}
	
	void CheckForMovement(){
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
			transform.position += Vector3.left * playerSpeed * Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
			transform.position += Vector3.right * playerSpeed * Time.deltaTime;
		}
		
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	
	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if(missile){
			Debug.Log("Player Hit!");
			health -= missile.GetDamage();
			missile.Hit();
			if(health <= 0){
				Destroy(gameObject);
				
			}
		}
	}
}
