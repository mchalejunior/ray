Feature: WorldFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to aggregate shapes in a scene
 
 # The text uses a default world with:
 #  light = point_light(point(-10,10,-10), color(1,1,1))
 #  shapes = two concentric spheres @ origin - outer a unit sphere and inner has r=0.5.


Scenario: Intersect a world with a ray
	Given world equals test default setup
	And origin equals tuple 0 0 -5 1
	And direction equals tuple 0 0 1 0
	When initialize ray with origin and direction
	And xs equals world intersections given ray
	And hit equals world intersection hit given ray
	Then xs intersection count equals 4
	And xs element 0 has t equals 4.0
	And xs element 1 has t equals 4.5
	And xs element 2 has t equals 5.5
	And xs element 3 has t equals 6.0
	And xs hit t equals 4.0


 Scenario: Precomputing the state of an intersection
	Given world equals test default setup
	And origin equals tuple 0 0 -5 1
	And direction equals tuple 0 0 1 0
	When initialize ray with origin and direction
	And hit equals world intersection hit given ray
	Then hit shape equals default world outer sphere
	And hit position equals tuple 0.0 0.0 -1.0 1.0
	#vector pointing back towards eye/camera
	And hit eyev equals tuple 0.0 0.0 -1.0 0.0
	And hit normalv equals tuple 0.0 0.0 -1.0 0.0


 Scenario: The hit when an intersection occurs on the outside
	Given world equals test default setup
	And origin equals tuple 0 0 -5 1
	And direction equals tuple 0 0 1 0
	When initialize ray with origin and direction
	And xs equals world intersections given ray
	And hit equals world intersection hit given ray
	Then xs intersection count equals 4
	And hit inside shape equals false


 Scenario: The hit when an intersection occurs on the inside
	Given world equals test default setup
	And origin equals tuple 0 0 0 1
	And direction equals tuple 0 0 1 0
	When initialize ray with origin and direction
	And xs equals world intersections given ray
	And hit equals world intersection hit given ray
	Then xs intersection count equals 4
	And hit inside shape equals true
	And hit eyev equals tuple 0.0 0.0 -1.0 0.0
	# normal would have been 0 0 1 but is inverted.
	And hit normalv equals tuple 0.0 0.0 -1.0 0.0


Scenario: Shading an intersection
	Given world equals test default setup
	And origin equals tuple 0 0 -5 1
	And direction equals tuple 0 0 1 0
	When initialize ray with origin and direction
	Then world color for ray equals 0.380661 0.475826 0.285495


Scenario: Shading an intersection from the inside
	Given world equals test default setup
	And light source is inside sphere
	And origin equals tuple 0 0 0 1
	And direction equals tuple 0 0 1 0
	When initialize ray with origin and direction
	Then world color for ray equals 0.9049845 0.9049845 0.9049845


Scenario: The color when a ray misses
	Given world equals test default setup
	And origin equals tuple 0 0 -5 1
	And direction equals tuple 0 1 0 0
	When initialize ray with origin and direction
	Then world color for ray equals 0.0 0.0 0.0


Scenario: The color when a ray hits
	Given world equals test default setup
	And origin equals tuple 0 0 -5 1
	And direction equals tuple 0 0 1 0
	When initialize ray with origin and direction
	Then world color for ray equals 0.380661 0.475826 0.285495



