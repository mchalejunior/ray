Feature: ConvertTuplesFeature
	In order to represent 3D space
	As a Ray Tracer
	I want the ability to convert tuples to points and vectors

Background:
	Given a <- tuple 4 -4 3

Scenario: Convert tuple to Point
	Then a.AsPoint converts to Point
	And a.x = 4
	And a.y = -4
	And a.z = 3
	And a.w = 1
	And a.IsPoint = true
	And a.IsVector = false

Scenario: Convert tuple to Vector
	Then a.AsVector converts to Vector
	And a.x = 4
	And a.y = -4
	And a.z = 3
	And a.w = 0
	And a.IsPoint = false
	And a.IsVector = true
