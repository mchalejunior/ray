Feature: BasicColorOpsFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to draw pixels in different colors
 
Scenario: Colors are (Red, Green, Blue) tuples
	Given c1 equals color 0 74 255
	Then c1.red equals 0
	And c1.green equals 74
	And c1.blue equals 255


Scenario: Adding colors
	Given c1 equals color 0 74 255
	And c2 equals color 125 55 20
	Then c1 plus c2 equals color 125 129 255


