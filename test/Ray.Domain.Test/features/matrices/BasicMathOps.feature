Feature: BasicMatrixMathFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to perform basic math on matrices
 
 
#Take a matrix and multiply it by another matrix.
#Use cases: scaling, rotation, translation.
Scenario: Multiply two matrices
	Given firstMatrix equals the following 4x4 matrix:
		|    | c1  | c2  | c3  | c4  |
		| r1 | 1.0 | 2.0 | 3.0 | 4.0 |
		| r2 | 5.0 | 6.0 | 7.0 | 8.0 |
		| r3 | 9.0 | 8.0 | 7.0 | 6.0 |
		| r4 | 5.0 | 4.0 | 3.0 | 2.0 |
	And secondMatrix equals the following 4x4 matrix:
		|    | c1   | c2  | c3  | c4   |
		| r1 | -2.0 | 1.0 | 2.0 | 3.0  |
		| r2 | 3.0  | 2.0 | 1.0 | -1.0 |
		| r3 | 4.0  | 3.0 | 6.0 | 5.0  |
		| r4 | 1.0  | 2.0 | 7.0 | 8.0  |
	Then firstMatrix multiplied by secondMatrix equals the following 4x4 matrix:
		|    | c1   | c2   | c3    | c4    |
		| r1 | 20.0 | 22.0 | 50.0  | 48.0  |
		| r2 | 44.0 | 54.0 | 114.0 | 108.0 |
		| r3 | 40.0 | 58.0 | 110.0 | 102.0 |
		| r4 | 16.0 | 26.0 | 46.0  | 42.0  |


 
Scenario: A Matrix multiplied by a Tuple
	Given firstMatrix equals the following 4x4 matrix:
		|    | c1  | c2  | c3  | c4  |
		| r1 | 1.0 | 2.0 | 3.0 | 4.0 |
		| r2 | 2.0 | 4.0 | 4.0 | 2.0 |
		| r3 | 8.0 | 6.0 | 4.0 | 1.0 |
		| r4 | 0.0 | 0.0 | 0.0 | 1.0 |
	And t equals tuple 1 2 3 1
	Then firstMatrix multiplied by t equals tuple 18 24 33 1


 
Scenario: Multiplying a matrix by the Identity matrix
	Given firstMatrix equals the following 4x4 matrix:
		|    | c1  | c2  | c3   | c4   |
		| r1 | 0.0 | 1.0 | 2.0  | 4.0  |
		| r2 | 2.0 | 4.0 | 6.0  | 8.0  |
		| r3 | 2.0 | 4.0 | 8.0  | 16.0 |
		| r4 | 4.0 | 8.0 | 16.0 | 32.0 |
	Then firstMatrix multiplied by Identity Matrix equals firstMatrix


#Transpose a matrix.
#Use cases: light, shading.
Scenario: Transposing a matrix
	Given firstMatrix equals the following 4x4 matrix:
		|    | c1  | c2  | c3  | c4  |
		| r1 | 0.0 | 9.0 | 3.0 | 0.0 |
		| r2 | 9.0 | 8.0 | 0.0 | 8.0 |
		| r3 | 1.0 | 8.0 | 5.0 | 3.0 |
		| r4 | 0.0 | 0.0 | 5.0 | 8.0 |
	Then the Transpose of firstMatrix is the following 4x4 matrix:
		|    | c1  | c2  | c3  | c4  |
		| r1 | 0.0 | 9.0 | 1.0 | 0.0 |
		| r2 | 9.0 | 8.0 | 8.0 | 0.0 |
		| r3 | 3.0 | 0.0 | 5.0 | 5.0 |
		| r4 | 0.0 | 8.0 | 3.0 | 8.0 |


#See below matrix values - this is the Identity matrix.
#Note: normally get this using Matrix4x4.Identity e.g. "Multiplying a matrix by the Identity matrix"
Scenario: Transposing the Identity matrix
	Given firstMatrix equals the following 4x4 matrix:
		|    | c1  | c2  | c3  | c4  |
		| r1 | 1.0 | 0.0 | 0.0 | 0.0 |
		| r2 | 0.0 | 1.0 | 0.0 | 0.0 |
		| r3 | 0.0 | 0.0 | 1.0 | 0.0 |
		| r4 | 0.0 | 0.0 | 0.0 | 1.0 |
	Then the Transpose of firstMatrix is still the Identity Matrix


#Invert a matrix.
#Use cases: transforming and deforming shapes.
Scenario: Matrix can be Inverted
	Given firstMatrix equals the following 4x4 matrix:
		|    | c1  | c2   | c3  | c4   |
		| r1 | 6.0 | 4.0  | 4.0 | 4.0  |
		| r2 | 5.0 | 5.0  | 7.0 | 6.0  |
		| r3 | 4.0 | -9.0 | 3.0 | -7.0 |
		| r4 | 9.0 | 1.0  | 7.0 | -6.0 |
	Then the Determinant of firstMatrix is -2120
	And firstMatrix IsInvertible equals true


Scenario: Matrix cannot be Inverted
	Given firstMatrix equals the following 4x4 matrix:
		|    | c1   | c2   | c3   | c4   |
		| r1 | -4.0 | 2.0  | -2.0 | -3.0 |
		| r2 | 9.0  | 6.0  | 2.0  | 6.0  |
		| r3 | 0.0  | -5.0 | 1.0  | -5.0 |
		| r4 | 0.0  | 0.0  | 0.0  | 0.0  |
	Then the Determinant of firstMatrix is 0
	And firstMatrix IsInvertible equals false


Scenario: Invert a matrix
	Given firstMatrix equals the following 4x4 matrix:
		|    | c1   | c2   | c3   | c4   |
		| r1 | -5.0 | 2.0  | 6.0  | -8.0 |
		| r2 | 1.0  | -5.0 | 1.0  | 8.0  |
		| r3 | 7.0  | 7.0  | -6.0 | -7.0 |
		| r4 | 1.0  | -3.0 | 7.0  | 4.0  |
	Then the Inversion of firstMatrix is the following 4x4 matrix:
		|    | c1          | c2          | c3          | c4          |
		| r1 | 0.21804512  | 0.45112783  | 0.24060151  | -0.04511278 |
		| r2 | -0.8082707  | -1.456767   | -0.44360903 | 0.52067673  |
		| r3 | -0.07894737 | -0.22368422 | -0.05263158 | 0.19736843  |
		| r4 | -0.5225564  | -0.81390977 | -0.3007519  | 0.30639097  |


Scenario: Multiplying a product by its inverse
	Given firstMatrix equals the following 4x4 matrix:
		|    | c1  | c2  | c3  | c4  |
		| r1 | 3.0 | -9.0 | 7.0 | 3.0 |
		| r2 | 3.0 | -8.0 | 2.0 | -9.0 |
		| r3 | -4.0 | 4.0 | 4.0 | 1.0 |
		| r4 | -6.0 | 5.0 | -1.0 | 1.0 |
	And secondMatrix equals the following 4x4 matrix:
		|    | c1   | c2  | c3  | c4   |
		| r1 | 8.0 | 2.0 | 2.0 | 2.0  |
		| r2 | 3.0  | -1.0 | 7.0 | 0.0 |
		| r3 | 7.0  | 0.0 | 5.0 | 4.0  |
		| r4 | 6.0  | -2.0 | 0.0 | 5.0  |
	And thirdMatrix equals firstMatrix multiplied by secondMatrix
	Then thirdMatrix multiplied by Inverse of secondMatrix equals firstMatrix

 
Scenario: Invert the Identity matrix
	Given firstMatrix equals the Identity Matrix
	Then the Inversion of firstMatrix is still the Identity Matrix

 
Scenario: Multiplying a matrix by its inverse
	Given firstMatrix equals the following 4x4 matrix:
		|    | c1  | c2   | c3  | c4   |
		| r1 | 6.0 | 4.0  | 4.0 | 4.0  |
		| r2 | 5.0 | 5.0  | 7.0 | 6.0  |
		| r3 | 4.0 | -9.0 | 3.0 | -7.0 |
		| r4 | 9.0 | 1.0  | 7.0 | -6.0 |
	Then firstMatrix multiplied by Inverse of firstMatrix equals the Identity Matrix
