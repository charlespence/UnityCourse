using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;

	private AudioSource audioSource;

	void Awake(){
		GameObject.DontDestroyOnLoad(gameObject);
		print ("Don't destroy on load: " + name);
	}

	void Start () {
		audioSource = GetComponent<AudioSource> ();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode){
		AudioClip currentLevelMusic = levelMusicChangeArray [scene.buildIndex];
		print ("Playing Clip: " + currentLevelMusic);
		if (currentLevelMusic) {
			audioSource.clip = currentLevelMusic;
			audioSource.loop = true;
			audioSource.Play ();
		}
	}
}
