Feature: apiTest

@mytag
Scenario: POST request test
	Given the user sends a post request with url as "https://restful-booker.herokuapp.com/auth"
	Then user should get a succes response with 200s code


Scenario: GET request test
	Given the user sends a get request with url as "https://reqres.in/api/users/2"
	Then user should get a succes response

#Scenario: GET request that fail ####
#	Given thee user sends a get request with url as "https://restful-booker.herokuapp.com/booking/2"
#	Then userr should get a succes response 