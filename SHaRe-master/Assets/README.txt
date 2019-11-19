Simple Menu:

Scene includes:
		 	Menu Camera

			Main
				-> Canvas
					-> StartGame Button
					-> Language Button
					-> optional button
			Language
				-> Canvas
					-> one button for every language

MenuAnimationScript.cs:

	-	Controlls animation of Main Menu panel.
		Pressing "Language" will shift panel offscreen allowing for pressing the language buttons 

	-	languageChosen(int index) will be called upon selecting language

	-	TODO:	- connect language selection to ACTUAL language changes
				- connect startGame() method to game functionality
				- adding functionality to optional button