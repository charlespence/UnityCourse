using UnityEngine;
using System.Collections;

public class NumberWizard : MonoBehaviour {

	int max = 1000;
	int min = 1;
	int guess = 500;
	bool pastFirstGuess = false;

	void Start () {
		PrintInstructions();
		NextGuess();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			print ("Up arrow pressed");
			min = (max - min) <= 2 ? max : guess;
			NextGuess();
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow)){
			print ("Down arrow pressed");
			max = guess;
			NextGuess();
		}
		else if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
			GameOver();
		}
		else if(Input.GetKeyDown (KeyCode.R)){
			Application.LoadLevel("Main");
		}
	}
	
	void PrintInstructions(){
		print ("================ START ======================");
		print ("Welcome to Number Wizard");
		print ("Pick a number in your head, but don't tell me!");
		
		print ("The highest number you can pick is " + max );
		print ("The lowest number you can pick is " + min );
	}
	
	void GameOver(){
		print ("I WON! (" + guess + ")");
		print ("====== END - PRESS R TO RESTART =============");
		print ("");
	}
	
	void NextGuess(){
		guess = ((max + min) / 2);
		
		if(pastFirstGuess){
			if((max - min) <= 1){
				GameOver();
				return;
			}
			print ("Higher, lower, or equal to: " + guess + "?");
		}
		else{
			print ("Is the number higher, lower, or equal to: " + guess + "?\n(Up = higher, down = lower, return = equal to)");
			pastFirstGuess = true;
		}
	}
}
