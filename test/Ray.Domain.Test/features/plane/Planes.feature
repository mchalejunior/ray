Feature: PlanesFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to draw planes


Scenario Outline: The normal of a plane is constant everywhere
	Given t1 equals tuple <x> <y> <z> 1.0
	And t2 equals tuple 0.0 1.0 0.0 0.0
	Then plane normal at t1 equals t2
	
	Examples: Point varied Normal constant
		| x    | y   | z     |
		| 0.0  | 0.0 | 0.0   |
		| 10.0 | 0.0 | -10.0 |
		| -5.0 | 0.0 | 150.0 |


# Running parallel - so never intersects
Scenario: Intersect with a ray parallel to the plane
	Given t1 equals tuple 0.0 10.0 0.0 1.0
	And t2 equals tuple 0.0 0.0 1.0 0.0
	When initialize ray with origin t1 and direction t2
	And xs equals plane intersections given ray
	Then xs intersection count equals 0


# Actual running "inside" plane, so intersects infinitely.
# But we just report this as no intersection as visually the same (but no stack overflow!)
Scenario: Intersect with a coplanar ray
	Given t1 equals tuple 0.0 0.0 0.0 1.0
	And t2 equals tuple 0.0 0.0 1.0 0.0
	When initialize ray with origin t1 and direction t2
	And xs equals plane intersections given ray
	Then xs intersection count equals 0


Scenario: A ray intersecting a plane from above
	Given t1 equals tuple 0.0 1.0 0.0 1.0
	And t2 equals tuple 0.0 -1.0 0.0 0.0
	When initialize ray with origin t1 and direction t2
	And xs equals plane intersections given ray
	Then xs intersection count equals 1
	And xs intersection hit equals plane instance


Scenario: A ray intersecting a plane from below
	Given t1 equals tuple 0.0 -1.0 0.0 1.0
	And t2 equals tuple 0.0 1.0 0.0 0.0
	When initialize ray with origin t1 and direction t2
	And xs equals plane intersections given ray
	Then xs intersection count equals 1
	And xs intersection hit equals plane instance

