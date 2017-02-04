using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NumberWizard : MonoBehaviour {

	public Text guessText;
	public int maxGuessesAllowed = 7;
	
	int max = 1000;
	int min = 1;
	int guess = 500;
	
	void Start(){
		StartGame();
	}
	
	void StartGame(){
		max = max + 1;
		NextGuess();	
	}
			
	public void GuessHigher(){
		min = guess;
		NextGuess();
	}
	
	public void GuessLower(){
		max = guess;
		NextGuess();	
	}
		
	void NextGuess(){
		guess = Random.Range(min, max);
		maxGuessesAllowed = maxGuessesAllowed - 1;
		if(maxGuessesAllowed <= 0) {
			Application.LoadLevel("Win");
		}
		guessText.text = guess.ToString();
	}
}
