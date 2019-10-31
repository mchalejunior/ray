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



