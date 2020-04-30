Feature: ShadowsFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to draw shadows

 # See World.feature. These tests also rely on a "default world".
 # The text uses a default world with:
 #  light = point_light(point(-10,10,-10), color(1,1,1))
 #  shapes = two concentric spheres @ origin - outer a unit sphere and inner has r=0.5.

 	
Background:
	Given position at the origin
	And Material with default values
	* World equals test default setup


 Scenario: Lighting with the surface in shadow
	Given eye equals tuple 0.0 0.0 -1.0 0.0
	And normal equals tuple 0.0 0.0 -1.0 0.0
	And t1 equals tuple 1.0 1.0 1.0 1.0
	And t2 equals tuple 0.0 0.0 -10.0 1.0
	And light intensity and position initialized from t1 and t2
	And is in shadow equals true
	When calculate resultantColor lighting for material light position eye normal isInShadow
	# Color only considers the ambient portion
	Then resultantColor equals tuple 0.1 0.1 0.1


 Scenario: There is no shadow when nothing is collinear with point and light
	Given t1 equals tuple 0.0 10.0 0.0 1.0
	Then t1 placed in default world in_shadow equals false


 Scenario: The shadow when an object is between the point and the light
	Given t1 equals tuple 10.0 -10.0 10.0 1.0
	Then t1 placed in default world in_shadow equals true


 Scenario: There is no shadow when an object is behind the light
	Given t1 equals tuple -20.0 20.0 -20.0 1.0
	Then t1 placed in default world in_shadow equals false


 Scenario: There is no shadow when an object is behind the point
	Given t1 equals tuple -2.0 2.0 -2.0 1.0
	Then t1 placed in default world in_shadow equals false

