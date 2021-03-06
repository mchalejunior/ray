﻿Feature: CompareMatricesFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to create matrices
 
 
Scenario: Initializing matrices
	Given firstMatrix equals the following 4x4 matrix:
		|    | c1   | c2   | c3   | c4   |
		| r1 | 1.0  | 2.0  | 3.0  | 4.0  |
		| r2 | 5.5  | 6.5  | 7.5  | 8.5  |
		| r3 | 9.0  | 10.0 | 11.0 | 12.0 |
		| r4 | 13.5 | 14.5 | 15.5 | 16.5 |
	Then firstMatrix.M11 equals 1.0
	And firstMatrix.M14 equals 4.0
	And firstMatrix.M21 equals 5.5
	And firstMatrix.M23 equals 7.5
	And firstMatrix.M33 equals 11.0
	And firstMatrix.M41 equals 13.5
	And firstMatrix.M43 equals 15.5

 
Scenario: Matrix equality with identical matrices
	Given firstMatrix equals the following 4x4 matrix:
		|    | c1  | c2  | c3  | c4  |
		| r1 | 1.0 | 2.0 | 3.0 | 4.0 |
		| r2 | 5.0 | 6.0 | 7.0 | 8.0 |
		| r3 | 9.0 | 8.0 | 7.0 | 6.0 |
		| r4 | 5.0 | 4.0 | 3.0 | 2.0 |
	And secondMatrix equals the following 4x4 matrix:
		|    | c1  | c2  | c3  | c4  |
		| r1 | 1.0 | 2.0 | 3.0 | 4.0 |
		| r2 | 5.0 | 6.0 | 7.0 | 8.0 |
		| r3 | 9.0 | 8.0 | 7.0 | 6.0 |
		| r4 | 5.0 | 4.0 | 3.0 | 2.0 |
	Then firstMatrix equals secondMatrix
 
 
Scenario: Matrix inequality with different matrices
	Given firstMatrix equals the following 4x4 matrix:
		|    | c1  | c2  | c3  | c4  |
		| r1 | 1.0 | 2.0 | 3.0 | 4.0 |
		| r2 | 5.0 | 6.0 | 7.0 | 8.0 |
		| r3 | 9.0 | 8.0 | 7.0 | 6.0 |
		| r4 | 5.0 | 4.0 | 3.0 | 2.0 |
	And secondMatrix equals the following 4x4 matrix:
		|    | c1  | c2  | c3  | c4  |
		| r1 | 2.0 | 3.0 | 4.0 | 5.0 |
		| r2 | 6.0 | 7.0 | 8.0 | 9.0 |
		| r3 | 8.0 | 7.0 | 6.0 | 5.0 |
		| r4 | 4.0 | 3.0 | 2.0 | 1.0 |
	Then firstMatrix does NOT equal secondMatrix

 
Scenario: Matrix equality with approximately identical matrices
	Given firstMatrix equals the following 4x4 matrix:
		|    | c1  | c2  | c3          | c4  |
		| r1 | 1.0 | 2.0 | 3.0         | 4.0 |
		| r2 | 5.0 | 6.0 | 7.0         | 8.0 |
		| r3 | 9.0 | 8.0 | 7.0         | 6.0 |
		| r4 | 5.0 | 4.0 | 3.741657387 | 2.0 |
	And secondMatrix equals the following 4x4 matrix:
		|    | c1  | c2  | c3          | c4  |
		| r1 | 1.0 | 2.0 | 3.0         | 4.0 |
		| r2 | 5.0 | 6.0 | 7.0         | 8.0 |
		| r3 | 9.0 | 8.0 | 7.0         | 6.0 |
		| r4 | 5.0 | 4.0 | 3.741657383 | 2.0 |
	Then firstMatrix equals secondMatrix
