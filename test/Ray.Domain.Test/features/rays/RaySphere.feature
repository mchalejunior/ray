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

