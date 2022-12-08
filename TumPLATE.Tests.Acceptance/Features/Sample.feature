Feature: Sample
calling the api
	 
	Scenario: calling the root
		When when I call the root
		Then a response with an array of fruits, environment and sentinel should be returned
