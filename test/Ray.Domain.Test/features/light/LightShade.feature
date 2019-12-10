Feature: LightShadeFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to calculate light reflection on surfaces 
  

Scenario: The normal on a sphere at a point on the x axis
	Given initialize sphere as a unit sphere at the origin
	#t1 is a point (W=1) - calc normal at this point
	And t1 equals tuple 1.0 0.0 0.0 1.0
	When calculate normal for sphere at t1
	Then normal equals tuple 1.0 0.0 0.0 0.0


Scenario: The normal on a sphere at a point on the y axis
	Given initialize sphere as a unit sphere at the origin
	#t1 is a point (W=1) - calc normal at this point
	And t1 equals tuple 0.0 1.0 0.0 1.0
	When calculate normal for sphere at t1
	Then normal equals tuple 0.0 1.0 0.0 0.0

	
Scenario: The normal on a sphere at a point on the z axis
	Given initialize sphere as a unit sphere at the origin
	#t1 is a point (W=1) - calc normal at this point
	And t1 equals tuple 0.0 0.0 1.0 1.0
	When calculate normal for sphere at t1
	Then normal equals tuple 0.0 0.0 1.0 0.0

	
Scenario: The normal on a sphere at a non axial point
	Given initialize sphere as a unit sphere at the origin
	#t1 is a point (W=1) - calc normal at this point
	And t1 equals tuple 0.57735 0.57735 0.57735 1.0
	When calculate normal for sphere at t1
	Then normal equals tuple 0.57735 0.57735 0.57735 0.0

	
Scenario: The normal is a normalized vector
	Given initialize sphere as a unit sphere at the origin
	#t1 is a point (W=1) - calc normal at this point
	And t1 equals tuple 0.57735 0.57735 0.57735 1.0
	When calculate normal for sphere at t1
	Then normal equals normalized normal

	
Scenario: Computing the normal on a translated sphere
	Given initialize sphere as a unit sphere at the origin
	#t1 is a point (W=1) - calc normal at this point
	And transformMatrix includes Translation Matrix 0 1 0
	And t1 equals tuple 0.0 1.70711 -0.70711 1.0
	When set sphere transformation equals transformMatrix
	And calculate normal for sphere at t1
	Then normal equals tuple 0.0 0.70711 -0.70711 0.0
	
	
Scenario: Computing the normal on a transformed sphere
	Given initialize sphere as a unit sphere at the origin
	# Note: transforms reversed compared to text, as IMatrixTransformationBuilder takes care of that.
	And transformMatrix includes Z Rotation Matrix pi over 5
	And transformMatrix includes Scaling Matrix 1.0 0.5 1.0
	And t1 equals tuple 0.0 0.70711 -0.70711 1.0
	When set sphere transformation equals transformMatrix
	And calculate normal for sphere at t1
	Then normal equals tuple 0.0 0.97014 -0.24254 0.0
	

