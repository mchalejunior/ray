Feature: BasicTuplesMathFeature
	In order to move around 3D space
	As a Ray Tracer
	I want the ability to perform basic math on points and vectors
 
#Take a point and add a vector. This tells you "where you would be if you
#followed the vector from that point". Adding point (w=1) and vector (w=0)
#gives a resultant w of 1 i.e. a new point (as above - "where you would be").
Scenario: Adding two tuples
	Given a1 = tuple 3 -2 5 1
	And a2 = tuple -2 3 1 0
	Then a1 plus a2 = tuple 1 1 6 1


#Take first point and subtract it from second point.
#Use-case in Light and Shading. What you end up with is the vector
#that points from point 2 to point 1 e.g. to your light source.
Scenario: Subtracting two points
	Given a1 = tuple 3 -2 5 1
	And a2 = tuple -2 3 1 1
	Then a1 minus a2 = tuple 5 -5 4 0


#Take a point and subtract a vector. Very similar to Scenario: Adding two tuples.
#Conceptually the difference here is that you're moving backwards!
Scenario: Subtracting two tuples
	Given a1 = tuple 3 -2 5 1
	And a2 = tuple -2 3 1 0
	Then a1 minus a2 = tuple 5 -5 4 1


#Take a vector and subtract another vector.
#Conceptually this is the change in directon between the two vectors (another vector).
Scenario: Subtracting two vectors
	Given a1 = tuple 3 -2 5 0
	And a2 = tuple -2 3 1 0
	Then a1 minus a2 = tuple 5 -5 4 0


#The opposite of a vector.
#Conceptually: Given a vector that points from a surface toward a light source,
#	what vector points from the light source back to the surface?
Scenario: Subtracting a vector from the zero vector
	Given a1 = vector 0 0 0
	And a2 = vector 1 -2 3
	Then a1 minus a2 = vector -1 2 -3


#Negate a vector. Exactly same as Scenario "Subtracting a vector from the zero vector".
#	But we'll provide a Negate funtion, which is easier to think about than a Zero vector.
#Conceptually: Given a vector that points from a surface toward a light source,
#	what vector points from the light source back to the surface?
Scenario: Negating a tuple
	Given a1 = tuple 1 -2 3 -4
	Then -a1 = tuple -1 2 -3 4


#Scale multiplication and division.
#Conceptually: What point lies 3.5 times farther in that direction?
Scenario Outline: Multiplying a tuple by a scalar or fraction
	Given a1 = tuple <xIn> <yIn> <zIn> <wIn>
	Then a1 multiplied by <scale> = tuple <xOut> <yOut> <zOut> <wOut>

	Examples: Scaling UP
		| xIn | yIn | zIn | wIn | scale | xOut | yOut | zOut | wOut  |
		| 1   | -2  | 3   | -4  | 3.5   | 3.5  | -7.0 | 10.5 | -14.0 |

	Examples: Scaling DOWN
		| xIn | yIn | zIn | wIn | scale | xOut | yOut | zOut | wOut  |
		| 1   | -2  | 3   | -4  | 0.5   | 0.5  | -1.0 | 1.5  | -2.0  |


#Equivalent to multiplying by 0.5, as above, but easier to describe as a division.
Scenario: Dividing a tuple by a scalar
	Given a1 = tuple 1 -2 3 -4
	Then a1 divided by 2.0 = tuple 0.5 -1.0 1.5 -2.0

