Feature: CreateTuplesFeature
	In order to represent 3D space
	As a Ray Tracer
	I want the ability to create tuples (points and vectors)
 
Background:
	Given a = tuple 4.3 -4.2 3.1

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

Scenario: Identically initialized tuples are equal
	And b = tuple 4.3 -4.2 3.1
	Then a = b is true

Scenario: Differently initialized tuples are not equal
	And b = tuple 5.4 -3.3 1.7
	Then a = b is false

Scenario Outline: A tuple can be a point or a vector
	And a.w = <w>
	Then a.x = 4.3
	And a.y = -4.2
	And a.z = 3.1
	And a.IsPoint = <p>
	And a.IsVector = <v>

	Examples: Weighted as Point
		| w   | p     | v     |
		| 1.0 | true  | false |

	Examples: Weighted as Vector
		| w   | p     | v     |
		| 0.0 | false | true  |

Scenario Outline: Tuple equality depends on x, y, z and w
	And b = tuple <x> <y> <z>
	And b.w = <w>
	Then a = b is <e>

	Examples: Identical (x, y, z), w varies
		| x   | y    | z   | w   | e     |
		| 4.3 | -4.2 | 3.1 | 1.0 | false |
		| 4.3 | -4.2 | 3.1 | 0.0 | true  |

	Examples: Varying (x, y, z), w constant
		| x   | y    | z   | w   | e     |
		| 4.3 | -4.2 | 3.1 | 0.0 | true  | #first validate +ve expectation
		| 3.4 | -2.4 | 1.3 | 0.0 | false |
		| 4.3 | -4.2 | 3.2 | 0.0 | false |
