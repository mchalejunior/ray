Feature: MatrixTransformationsFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to move and resize shapes using matrix transformations
 

# Move a point. 
Scenario: Multiplying by a translation matrix
	Given firstMatrix equals Translation Matrix 5 -3 2
	#Tuple t1 is a Point (W=1)
	And t1 equals tuple -3 4 5 1
	Then firstMatrix multiplied by t1 equals tuple 2.0 1.0 7.0 1.0

# Move a point in the opposite direction.  
Scenario: Multiplying by the inverse of a translation matrix
	Given firstMatrix equals Translation Matrix 5 -3 2
	And secondMatrix equals inverse of firstMatrix
	And t1 equals tuple -3 4 5 1
	Then secondMatrix multiplied by t1 equals tuple -8.0 7.0 3.0 1.0
 
#NOTE: You can't translate (move) a Vector - it already has direction baked in.
Scenario: Translation does not affect vectors
	Given firstMatrix equals Translation Matrix 5 -3 2
	#Tuple t1 is a Vector (W=0)
	And t1 equals tuple -3 4 5 0
	Then firstMatrix multiplied by t1 equals tuple -3.0 4.0 5.0 0.0

# Scale. Normally this would be for many points e.g. point around a sphere.
#  If you scale all the points, where would each individual point now be after scaling.
Scenario: A scaling matrix applied to a point
	Given firstMatrix equals Scaling Matrix 2 3 4
	And t1 equals tuple -4 6 8 1
	Then firstMatrix multiplied by t1 equals tuple -8.0 18.0 32.0 1.0

#NOTE: You can scale a Vector - make it bigger / smaller in size.
Scenario: A scaling matric applied to a vector
	Given firstMatrix equals Scaling Matrix 2 3 4
	And t1 equals tuple -4 6 8 0
	Then firstMatrix multiplied by t1 equals tuple -8.0 18.0 32.0 0.0

#NOTE: Multiply by the inverse of a scaling matrix will have the opposite effect (shrink rather than grow!)
Scenario: Multiplying by the inverse of a scaling matrix
	Given firstMatrix equals Scaling Matrix 2 3 4
	And secondMatrix equals inverse of firstMatrix
	And t1 equals tuple -4 6 8 0
	Then secondMatrix multiplied by t1 equals tuple -2.0 2.0 2.0 0.0


#Reflection - move point to other side of axis.
#Reflection is essentially the same thing as scaling by a negative number.

Scenario: Reflection is scaling by a negative number
	Given firstMatrix equals Scaling Matrix -1 1 1
	And t1 equals tuple 2 3 4 1
	Then firstMatrix multiplied by t1 equals tuple -2.0 3.0 4.0 1.0


# Rotation: Rotate a point by a certain number of radians around an axis. 
Scenario: Rotating a point around the x axis
	Given t1 equals tuple 0 1 0 1
	#Half quarter rotation is Pi/4
	#Full quarter rotation is Pi/2
	And firstRotation equals Pi over 4
	And secondRotation equals Pi over 2
	And firstMatrix equals X Rotation Matrix for firstRotation
	And secondMatrix equals X Rotation Matrix for secondRotation
	Then firstMatrix multiplied by t1 equals tuple 0.0 0.70710677 0.70710677 1.0
	And secondMatrix multiplied by t1 equals tuple 0.0 0.0 1.0 1.0


Scenario: The inverse of an x rotation rotates in the opposite direction
	Given t1 equals tuple 0 1 0 1
	#Half quarter rotation is Pi/4
	And firstRotation equals Pi over 4
	And firstMatrix equals X Rotation Matrix for firstRotation
	And secondMatrix equals inverse of firstMatrix
	Then secondMatrix multiplied by t1 equals tuple 0.0 0.70710677 -0.70710677 1.0


Scenario: Rotating a point around the y axis
	Given t1 equals tuple 0 0 1 1
	#Half quarter rotation is Pi/4
	#Full quarter rotation is Pi/2
	And firstRotation equals Pi over 4
	And secondRotation equals Pi over 2
	And firstMatrix equals Y Rotation Matrix for firstRotation
	And secondMatrix equals Y Rotation Matrix for secondRotation
	Then firstMatrix multiplied by t1 equals tuple 0.70710677 0.0 0.70710677 1.0
	And secondMatrix multiplied by t1 equals tuple 1.0 0.0 0.0 1.0


Scenario: Rotating a point around the z axis
	Given t1 equals tuple 0 1 0 1
	#Half quarter rotation is Pi/4
	#Full quarter rotation is Pi/2
	And firstRotation equals Pi over 4
	And secondRotation equals Pi over 2
	And firstMatrix equals Z Rotation Matrix for firstRotation
	And secondMatrix equals Z Rotation Matrix for secondRotation
	Then firstMatrix multiplied by t1 equals tuple -0.70710677 0.70710677 0.0 1.0
	And secondMatrix multiplied by t1 equals tuple -1.0 0.0 0.0 1.0



# Shearing a.k.a. Skew
# One of the most complex transforms visually. But quite a simple calculation.
# Basically: a points movement on an axis are proportional to another axis movements.
# Calculation: a dynamic shearing matrix is built (from the Identity matrix, with specified shearing offsets)
#   and this is multipled by a point to give the new skewed point.

Scenario Outline: A shearing transform moves axis points in proportion to other axis movements
	Given firstMatrix equals Shearing Matrix <x2y> <x2z> <y2x> <y2z> <z2x> <z2y>
	#Tuple t1 is a Point (W=1)
	And t1 equals tuple 2 3 4 1
	Then firstMatrix multiplied by t1 equals tuple <x> <y> <z> 1.0

	Examples: Move one axis in proportion to another
		| x2y | x2z | y2x | y2z | z2x | z2y | x   | y   | z   |
		| 1   | 0   | 0   | 0   | 0   | 0   | 5.0 | 3.0 | 4.0 |
		| 0   | 1   | 0   | 0   | 0   | 0   | 6.0 | 3.0 | 4.0 |
		| 0   | 0   | 1   | 0   | 0   | 0   | 2.0 | 5.0 | 4.0 |
		| 0   | 0   | 0   | 1   | 0   | 0   | 2.0 | 7.0 | 4.0 |
		| 0   | 0   | 0   | 0   | 1   | 0   | 2.0 | 3.0 | 6.0 |
		| 0   | 0   | 0   | 0   | 0   | 1   | 2.0 | 3.0 | 7.0 |

#Examples:
#  Move X in proportion to Y
#  Move X in proportion to Z
#  Move Y in proportion to X
#  Move Y in proportion to Z
#  Move Z in proportion to X
#  Move Z in proportion to Y



# Chaining transformations.
#  -Prove same result as performing one by one.
#  -Show that chains need to be applied in reverse order.

# Apply transforms one by one. 
Scenario: Individual transformations are applied in sequence
	Given t1 equals tuple 1 0 1 1
	#Full quarter rotation is Pi/2
	And firstRotation equals Pi over 2
	And firstMatrix equals X Rotation Matrix for firstRotation
	And secondMatrix equals Scaling Matrix 5 5 5
	And thirdMatrix equals Translation Matrix 10 5 7
	#Individually assert rotation, then apply output to next transform.
	Then firstMatrix multiplied by t1 equals tuple 1.0 -1.0 0.0 1.0
	And t1 equals tuple 1 -1 0 1
	#Individually assert scaling, then apply output to next transform.
	Then secondMatrix multiplied by t1 equals tuple 5.0 -5.0 0.0 1.0
	And t1 equals tuple 5 -5 0 1
	#Individually assert translation
	Then thirdMatrix multiplied by t1 equals tuple 15.0 0.0 7.0 1.0

# Chain transformations.
Scenario: Chained transformations must be applied in reverse order
	Given t1 equals tuple 1 0 1 1
	#Full quarter rotation is Pi/2
	And firstRotation equals Pi over 2
	And firstMatrix equals X Rotation Matrix for firstRotation
	And secondMatrix equals Scaling Matrix 5 5 5
	And thirdMatrix equals Translation Matrix 10 5 7
	#This time apply transforms in one go and assert final result is same.
	Then thirdMatrix multiplied by secondMatrix multiplied by firstMatrix multiplied by t1 equals tuple 15.0 0.0 7.0 1.0


