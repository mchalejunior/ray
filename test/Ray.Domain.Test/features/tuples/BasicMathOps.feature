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


#Length (aka Magnitude).
#Conceptually: A vector encodes direction and distance. The distance is 
#	called magnitude. It's how far you would travel in a straight line if 
#	you walked from one end of the vector to the other.
#Mathematically: Pythagoras theorem. Square root of the sum of the sides squared.
Scenario Outline: Computing the length of a vector
	Given a1 = vector <x> <y> <z>
	Then magnitude of a1 equals <magnitude>

	Examples: Scaling UP
		| x  | y  | z  | magnitude   |
		| 1  | 0  | 0  | 1.0         |
		| 0  | 1  | 0  | 1.0         |
		| 0  | 0  | 1  | 1.0         |
		| 1  | 2  | 3  | 3.741657387 | # root 14
		| -1 | -2 | -3 | 3.741657387 | # root 14


#Normalize a vector.
#Conceptually: The above scenarios - magnitude and scaling up / down are acting
#	on unit vectors (unit vectors have length = 1). If we first normalize all vectors, 
#	before applying further calculations then everything is anchored to a common scale.
#	This is important, otherwise everything would scale differently and visually the
#	scene would be inconsistent, at best! So normalizing a vector is calculating it's
#	equivalent unit vector (same direction, length = 1).
#Mathematically: You normalize a tuple by dividing each of its components by it's magnitude.
Scenario Outline: Normalizing with whole numbers
	Given a1 = vector <xIn> <yIn> <zIn>
	Then a1 normalized = vector <xOut> <yOut> <zOut>

	Examples: Specified result
		| xIn | yIn | zIn | xOut | yOut | zOut |
		| 4   | 0   | 0   | 1    | 0    | 0    |

Scenario Outline: Normalizing with decimals
	Given a1 = vector <xIn> <yIn> <zIn>
	Then a1 normalized is calculated from <magnitude>

	Examples: Known input magnitude
		| xIn | yIn | zIn | magnitude   |
		| 1   | 2   | 3   | 3.741657387 | # root 14

Scenario: Normalized Vector has Length 1
	Given a1 = vector 1 2 3
	And a2 = a1 normalized
	Then a2 is a unit vector


#Dot Product (a.k.a. scalar product or inner product)
#Conceptually: Used when intersecting rays with objects and to compute surface shading.
#	The smaller the dot product, the larger the angle between the vectors.
#	E.g. dot product of 1 means the vectors are identical, -1 means they point in opposit directions.
#Mathematically: The sum of the products of the corresponding components of each vector.
#	E.g. a1.X * a2.X + a1.Y * a2.Y...
#	If the two vectors are unit vectors, the dot product is the cosine of the angle between them.
Scenario: The dot product of two tuples
	Given a1 = vector 1 2 3
	And a2 = vector 2 3 4
	Then dot of a1, a2 equals 20


#Cross Product
#Conceptually: Another vector op. Unlike Dot, it returns another vector instead of a scalar.
#	Your new vector is perpendicular to both of the original vectors.
#	Aside: Used with Triangles and View Transformations.
#Mathematically: More complex formula that I'll ignore for now. Other interesting points...
#	Order is important. Reversing the order changes the direction (negates).
Scenario Outline: The cross product of two tuples
	Given a1 = vector <x1> <y1> <z1>
	And a2 = vector <x2> <y2> <z2>
	Then cross of a1, a2 equals vector <xOut> <yOut> <zOut>

	Examples: Scaling UP
		| x1 | y1 | z1 | x2 | y2 | z2 | xOut | yOut | zOut |
		| 1  | 2  | 3  | 2  | 3  | 4  | -1   | 2    | -1   |
		| 2  | 3  | 4  | 1  | 2  | 3  | 1    | -2   | 1    |

