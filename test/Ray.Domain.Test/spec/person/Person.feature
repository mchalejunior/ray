Feature: CelebrateBirthday
	In order to grow older
	As a regular human
	I want to celebrate my birthday
 
Scenario: Grow older
	Given I am 30 years old now
	When I celebrate my birthday
	Then my age should increase to 31
