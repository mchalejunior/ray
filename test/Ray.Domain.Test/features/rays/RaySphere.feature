Feature: RaySphereIntersectionFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to trace rays through a scene and calculate their intersections
 

Scenario: Creating and querying a ray
	Given origin equals tuple 1 2 3 1
	And direction equals tuple 4 5 6 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	When initialize ray with origin and direction
	Then ray origin equals tuple 1 2 3 1
	And ray direction equals tuple 4 5 6 0
 

Scenario Outline: Computing a point from a distance
	Given origin equals tuple 2 3 4 1
	And direction equals tuple 1 0 0 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	When initialize ray with origin and direction
	And distance equals <t>
	Then ray position equals tuple <x> <y> <z> 1.0

	Examples: Move specified distance along ray
		| t    | x   | y   | z   |
		| 0.0  | 2.0 | 3.0 | 4.0 |
		| 1.0  | 3.0 | 3.0 | 4.0 |
		| -1.0 | 1.0 | 3.0 | 4.0 |
		| 2.5  | 4.5 | 3.0 | 4.0 |
 

Scenario: A ray intersects a sphere at two points
	Given origin equals tuple 0 0 -5 1
	And direction equals tuple 0 0 1 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	When initialize ray with origin and direction
	And initialize sphere as a unit sphere at the origin
	And initialize xs as intersection calulator for ray, sphere
	Then xs intersection count equals 2
	And xs element 0 has t equals 4.0
	And xs element 1 has t equals 6.0
	And xs calculates shape being in front of ray
	And xs calculates intersection as non tangential

 
Scenario: A ray intersects a sphere at a tangent
	Given origin equals tuple 0 1 -5 1
	And direction equals tuple 0 0 1 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	When initialize ray with origin and direction
	And initialize sphere as a unit sphere at the origin
	And initialize xs as intersection calulator for ray, sphere
	Then xs intersection count equals 2
	And xs element 0 has t equals 5.0
	And xs element 1 has t equals 5.0
	And xs calculates intersection as tangential


Scenario: A ray misses a sphere
	Given origin equals tuple 0 2 -5 1
	And direction equals tuple 0 0 1 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	When initialize ray with origin and direction
	And initialize sphere as a unit sphere at the origin
	And initialize xs as intersection calulator for ray, sphere
	Then xs intersection count equals 0


Scenario: A ray originates inside a sphere
	Given origin equals tuple 0 0 0 1
	And direction equals tuple 0 0 1 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	When initialize ray with origin and direction
	And initialize sphere as a unit sphere at the origin
	And initialize xs as intersection calulator for ray, sphere
	Then xs intersection count equals 2
	And xs element 0 has t equals -1.0
	And xs element 1 has t equals 1.0
	And xs calculates ray as originating inside shape


Scenario: A sphere is behind a ray
	Given origin equals tuple 0 0 5 1
	And direction equals tuple 0 0 1 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	When initialize ray with origin and direction
	And initialize sphere as a unit sphere at the origin
	And initialize xs as intersection calulator for ray, sphere
	Then xs intersection count equals 2
	And xs element 0 has t equals -6.0
	And xs element 1 has t equals -4.0
	And xs calculates shape being behind ray


Scenario: An intersection encapsulates t and object
	Given origin equals tuple 0 0 -5 1
	And direction equals tuple 0 0 1 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	When initialize ray with origin and direction
	And initialize sphere as a unit sphere at the origin
	And initialize xs as intersection calulator for ray, sphere
	Then xs intersection count equals 2
	And xs element 0 has t equals 4.0
	And xs element 1 has t equals 6.0
	And xs element 0 has shape equals sphere
	And xs element 1 has shape equals sphere


Scenario: The hit when all intersections have a positive t
	Given origin equals tuple 0 0 -5 1
	And direction equals tuple 0 0 1 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	When initialize ray with origin and direction
	And initialize sphere as a unit sphere at the origin
	And initialize xs as intersection calulator for ray, sphere
	Then xs intersection count equals 2
	And xs hit t equals 4.0


Scenario: The hit when some intersections have a negative t
	Given origin equals tuple 0 0 0 1
	And direction equals tuple 0 0 1 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	When initialize ray with origin and direction
	And initialize sphere as a unit sphere at the origin
	And initialize xs as intersection calulator for ray, sphere
	Then xs intersection count equals 2
	And xs hit t equals 1.0


Scenario: The hit when all intersections have a negative t
	Given origin equals tuple 0 0 5 1
	And direction equals tuple 0 0 1 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	When initialize ray with origin and direction
	And initialize sphere as a unit sphere at the origin
	And initialize xs as intersection calulator for ray, sphere
	Then xs intersection count equals 2
	And xs hit t equals null

