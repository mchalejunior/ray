Feature: LightPhongFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to calculate light reflection on surfaces with material and color
 
#Easier to create a new Feature and class for the last part of the "Light and Shading" chapter.
#Could shoe-horn it into the existing feature, but it would take away from the clarity of both!

		
	
Background:
	Given position at the origin
	And Material with default values


Scenario: Lighting with the eye between the light and the surface
	Given eye equals tuple 0.0 0.0 -1.0 0.0
	And normal equals tuple 0.0 0.0 -1.0 0.0
	And t1 equals tuple 1.0 1.0 1.0 1.0
	And t2 equals tuple 0.0 0.0 -10.0 1.0
	And light intensity and position initialized from t1 and t2
	When calculate resultantColor lighting for material light position eye normal
	Then resultantColor equals tuple 1.9 1.9 1.9


Scenario: Lighting with the eye between light and surface, eye offset 45 degrees
	Given eye equals tuple 0.0 0.70711 -0.70711 0.0
	And normal equals tuple 0.0 0.0 -1.0 0.0
	And t1 equals tuple 1.0 1.0 1.0 1.0
	And t2 equals tuple 0.0 0.0 -10.0 1.0
	And light intensity and position initialized from t1 and t2
	When calculate resultantColor lighting for material light position eye normal
	Then resultantColor equals tuple 1.0 1.0 1.0


Scenario: Lighting with eye opposite surface, light offset 45 degrees
	Given eye equals tuple 0.0 0.0 -1.0 0.0
	And normal equals tuple 0.0 0.0 -1.0 0.0
	And t1 equals tuple 1.0 1.0 1.0 1.0
	And t2 equals tuple 0.0 10.0 -10.0 1.0
	And light intensity and position initialized from t1 and t2
	When calculate resultantColor lighting for material light position eye normal
	Then resultantColor equals tuple 0.736396 0.736396 0.736396


Scenario: Lighting with eye in the path of the reflection vector
	Given eye equals tuple 0.0 -0.70711 -0.70711 0.0
	And normal equals tuple 0.0 0.0 -1.0 0.0
	And t1 equals tuple 1.0 1.0 1.0 1.0
	And t2 equals tuple 0.0 10.0 -10.0 1.0
	And light intensity and position initialized from t1 and t2
	When calculate resultantColor lighting for material light position eye normal
	#Rounding diff to text here, but believe calcs here more accurate.
	Then resultantColor equals tuple 1.637233 1.637233 1.637233





