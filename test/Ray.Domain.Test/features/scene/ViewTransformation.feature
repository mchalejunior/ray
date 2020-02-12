Feature: ViewTransformationFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to move the camera around the world
 

Scenario: The transformation matrix for the default orientation
	Given From equals tuple 0 0 0 1
	And To equals tuple 0 0 -1 1
	And Up equals tuple 0 1 0 0
	When set view transformation on camera
	Then camera transform equals identity matrix
 

Scenario: A view transformation matrix looking in the positive z direction
	Given From equals tuple 0 0 0 1
	And To equals tuple 0 0 1 1
	And Up equals tuple 0 1 0 0
	When set view transformation on camera
	Then camera transform equals scaling matrix -1 1 -1
 

Scenario: A view transformation moves the world
	Given From equals tuple 0 0 8 1
	And To equals tuple 0 0 0 1
	And Up equals tuple 0 1 0 0
	When set view transformation on camera
	Then camera transform equals translation matrix 0 0 -8
 

Scenario: A arbitrary view transformation
	Given From equals tuple 1 3 2 1
	And To equals tuple 4 -2 8 1
	And Up equals tuple 1 1 0 0
	When set view transformation on camera
	Then camera transform equals the following 4x4 matrix:
		|    | c1           | c2          | c3           | c4        |
		| r1 | -0.507092535 | 0.507092535 | 0.6761234    | -2.366432 |
		| r2 | 0.767715931  | 0.6060915   | 0.121218294  | -2.828427 |
		| r3 | -0.358568579 | 0.5976143   | -0.717137158 | 0.0       |
		| r4 | 0.0          | 0.0         | 0.0          | 1.0       |

