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
