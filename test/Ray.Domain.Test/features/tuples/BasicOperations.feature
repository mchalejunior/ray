Feature: BasicTuplesMathFeature
	In order to move around 3D space
	As a Ray Tracer
	I want the ability to perform basic math on points and vectors

Background:
	Given a1 = tuple 3 -2 5 1

Scenario​: Adding two tuples
	​​And​ a2 = tuple -2 3 1 0
	​​Then​ a1 plus a2 = tuple 1 1 6 1
