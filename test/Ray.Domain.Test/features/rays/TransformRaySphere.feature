Feature: RaySphereTransformationFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to transform rays, moving between world and object space, in order to calculate intersections on scaled and translated spheres
  

Scenario: Translating a ray
	Given origin equals tuple 1 2 3 1
	And direction equals tuple 0 1 0 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	And transformMatrix equals Translation Matrix 3 4 5
	When initialize ray with origin and direction
	And transform ray with transformMatrix
	Then transformRay origin equals tuple 4.0 6.0 8.0 1.0
	And transformRay direction equals tuple 0.0 1.0 0.0 0.0


Scenario: Scaling a ray
	Given origin equals tuple 1 2 3 1
	And direction equals tuple 0 1 0 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	And transformMatrix equals Scaling Matrix 2 3 4
	When initialize ray with origin and direction
	And transform ray with transformMatrix
	Then transformRay origin equals tuple 2.0 6.0 12.0 1.0
	And transformRay direction equals tuple 0.0 3.0 0.0 0.0


Scenario: A spheres default transform
	Given initialize sphere as a unit sphere at the origin
	And transformMatrix equals Identity Matrix
	And initialize sphere as a unit sphere at the origin
	Then sphere transform equals transformMatrix


Scenario: Changing a spheres transformation
	Given initialize sphere as a unit sphere at the origin
	And transformMatrix equals Translation Matrix 2 3 4
	And initialize sphere as a unit sphere at the origin
	When set sphere transformation equals transformMatrix
	Then sphere transform equals transformMatrix
	  

Scenario: Intersecting a scaled sphere with a ray
	Given origin equals tuple 0 0 -5 1
	And direction equals tuple 0 0 1 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	And transformMatrix equals Scaling Matrix 2 2 2
	And initialize sphere as a unit sphere at the origin
	When set sphere transformation equals transformMatrix
	And initialize ray with origin and direction
	And initialize xs as intersection calulator for ray, sphere
	Then xs intersection count equals 2
	And xs element 0 has t equals 3.0
	And xs element 1 has t equals 7.0
	  

Scenario: Intersecting a translated sphere with a ray
	Given origin equals tuple 0 0 -5 1
	And direction equals tuple 0 0 1 0
	#Origin is a Point (W=1), direction a Vector (W=0)
	And transformMatrix equals Translation Matrix 5 0 0
	And initialize sphere as a unit sphere at the origin
	When set sphere transformation equals transformMatrix
	And initialize ray with origin and direction
	And initialize xs as intersection calulator for ray, sphere
	Then xs intersection count equals 0

