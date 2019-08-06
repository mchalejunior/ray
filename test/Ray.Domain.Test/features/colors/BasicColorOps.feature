Feature: BasicColorOpsFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to draw pixels in different colors
 
Scenario: Colors are (Red, Green, Blue) tuples
	Given c1 equals color 0.5 0.4 0.9
	Then c1.red equals 0.5
	And c1.green equals 0.4
	And c1.blue equals 0.9


Scenario: Adding colors
	Given c1 equals color 0.0 0.5 0.8
	And c2 equals color 0.9 0.45 0.15
	Then c1 plus c2 equals color 0.9 0.95 0.95


