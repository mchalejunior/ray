Feature: CameraFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability for a camera to project rays onto a canvas
 

Scenario: The pixel size for a horizontal canvas
	Given camera with Hsize 200 Vsize 125 and Field of pi over 2
	Then camera PixelSize equals 0.01
 

Scenario: The pixel size for a vertical canvas
	Given camera with Hsize 125 Vsize 200 and Field of pi over 2
	Then camera PixelSize equals 0.01
 

Scenario: Constructing a ray through the center of the canvas
	Given camera with Hsize 201 Vsize 101 and Field of pi over 2
	When calculate ray at 100 50
	Then ray origin equals tuple 0.0 0.0 0.0 1.0
	And ray direction equals tuple 0.0 0.0 -1.0 0.0
 

Scenario: Constructing a ray through a corner of the canvas
	Given camera with Hsize 201 Vsize 101 and Field of pi over 2
	When calculate ray at 0 0
	Then ray origin equals tuple 0.0 0.0 0.0 1.0
	And ray direction equals tuple 0.6651864 0.33259323 -0.66851234 0.0
 

Scenario: Constructing a ray when the camera is transformed
	Given camera with Hsize 201 Vsize 101 and Field of pi over 2
	And firstRotation equals Pi over 4
	And Translation equals tuple 0 -2 5
	And camera transform equals first rotation on y and translation
	When calculate ray at 100 50
	Then ray origin equals tuple 0.0 2.0 -5.0 1.0
	And ray direction equals tuple 0.707107 0.0 -0.707107 0.0
 
 