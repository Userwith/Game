using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unity_Chant : MonoBehaviour {

	// Use this for initialization
	public AudioClip run;
	public AudioClip walk;
	private Animator animator;
	private AudioSource runaudio;
	int i;
	float speed = 0;
	float direction = 0;
	float ant = 0.25f;
	float rot = 2f;
	void Start () {
		animator = GetComponent<Animator>();
		runaudio = GetComponent<AudioSource>();
		i = 0;
	}


	void Update ()
	{
		if (animator == null) return;
	

		i++;
		if (i == 20) {
			i = 0;
		}
		/*
           controler
		*/
		 if (Input.GetKey (KeyCode.W)) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				animator.SetBool ("Jump", true);
				animator.SetBool ("Rest",false);
				Debug.Log ("fuck");
				i = 0;
			} else if(animator.GetBool("Jump")&&i >15) {
				animator.SetBool ("Rest",true);
				animator.SetBool ("Jump", false);
			}
			if (Input.GetKey (KeyCode.LeftShift)) {
				speed += 6 * ant * Time.deltaTime;
				if (speed > 8) {
					speed = 8;
				}
			} else {
				speed += ant * Time.deltaTime;
				if (speed >= 2) {
					speed -= 3 * ant * Time.deltaTime;
				}
				if (speed >= 3) {
					speed -= 20 * ant * Time.deltaTime;
				}
			}
		} else if (Input.GetKey (KeyCode.S)) {
			if (speed > 0) {
				speed -= 50 * ant * Time.deltaTime;
			} else {
				speed -= ant * Time.deltaTime;
				if (speed < -1.25) {
					speed +=  3 * ant * Time.deltaTime;
				}
			}
		}
		else{
			if (speed > 0) {
				speed -= 30 * ant * Time.deltaTime;
				if (speed < 0) {
					speed = 0;
				}
			} else if (speed < 0) {
				speed += 30 * ant * Time.deltaTime;
				if (speed > 0) {
					speed = 0;
				}
			}
		}
		if (Input.GetKey (KeyCode.A)) {
			direction -= rot * Time.deltaTime;
			if (direction < -1) {
				direction += rot * Time.deltaTime;
			}
		} else if (Input.GetKey (KeyCode.D)) {
			direction += rot * Time.deltaTime;
			if (direction > 1) {
				direction -= rot * Time.deltaTime;
			}
		} else {
			if (direction < -0.1) {
				direction += rot * Time.deltaTime;
			} else if (direction > 0.1) {
				direction -= rot * Time.deltaTime;
			} else {
				direction = 0;
			}
		}
		animator.SetFloat ("Speed", speed);
		animator.SetFloat ("Direction", direction);
		transform.eulerAngles += new Vector3 (0, 100*direction*Time.deltaTime, 0);
		transform.position += transform.forward*speed*Time.deltaTime;

		/*
         audiosource
		*/
		if (speed >= 5 && runaudio.clip == walk) {
			runaudio.clip = run;
			runaudio.Play ();
		} else if (speed < 5 && speed > 0 && runaudio.clip == run) {
			runaudio.clip = walk;
			runaudio.Play ();
		} else if (speed < 5 && speed > 0 && !runaudio.isPlaying) {
			runaudio.clip = walk;
			runaudio.Play ();
		} else if (speed == 0 && runaudio.isPlaying) {
			runaudio.Pause ();
		} else if (speed < 0&& !runaudio.isPlaying) {
			runaudio.clip = walk;
			runaudio.Play ();
		}

     
	}
}
