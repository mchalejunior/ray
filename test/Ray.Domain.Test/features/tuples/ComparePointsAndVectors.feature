Feature: TuplesFeature
	In order to represent 3D space
	As a Ray Tracer
	I want the ability to create tuples (points and vectors)
 
Background:
	Given a <- tuple 4.3 -4.2 3.1

Scenario: A tuple with w=1.0 is a point
	And a.w = 1.0
	Then a.x = 4.3
	And a.y = -4.2
	And a.z = 3.1
	And a.IsPoint = true
	And a.IsVector = false

Scenario: A tuple with w=0.0 is a vector
	And a.w = 0.0
	Then a.x = 4.3
	And a.y = -4.2
	And a.z = 3.1
	And a.IsPoint = false
	And a.IsVector = true

Scenario Outline: A tuple can be a point or a vector
	And a.w = <w>
	Then a.x = 4.3
	And a.y = -4.2
	And a.z = 3.1
	And a.IsPoint = <p>
	And a.IsVector = <v>

	Examples: of a point
		| w   | p     | v     |
		| 1.0 | true  | false |

	Examples: of a vector
		| w   | p     | v     |
		| 0.0 | false | true  |