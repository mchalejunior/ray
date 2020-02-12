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

