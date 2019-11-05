Feature: MatrixTransformationsChainingFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to apply multiple transformations to shapes using a natural API
 

# See Chaining transformations in Transformations.feature.
#  Here we apply the same test, but try to provide a more natural API in the code behind.
Scenario: Chained transformations using a natural API
	Given t1 equals tuple 1 0 1 1
	#Full quarter rotation is Pi/2
	And firstRotation equals Pi over 2
	And Scale equals tuple 5 5 5
	And Translation equals tuple 10 5 7
	Then x-rotate scale and translate on t1 equals tuple 15.0 0.0 7.0 1.0

