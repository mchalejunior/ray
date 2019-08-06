Feature: BasicColorOpsFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to draw pixels in different colors
 

Scenario Outline: Initializing colors
	Given c1 equals color <r1> <g1> <b1>
	Then c1.red equals <r1>
	And c1.green equals <g1>
	And c1.blue equals <b1>

	Examples: All in normal range
		| r1  | g1  | b1  |
		| 0.5 | 0.4 | 0.9 |

	Examples: Outside normal range
		| r1   | g1  | b1  |
		| -0.5 | 0.4 | 1.7 |


Scenario Outline: Adding colors
	Given c1 equals color <r1> <g1> <b1>
	And c2 equals color <r2> <g2> <b2>
	Then c1 plus c2 equals color <r> <g> <b>

	Examples: All in normal range
		| r1  | g1  | b1  | r2  | g2   | b2   | r   | g    | b    |
		| 0.0 | 0.5 | 0.8 | 0.9 | 0.45 | 0.15 | 0.9 | 0.95 | 0.95 |

	Examples: Outside normal range
		| r1  | g1  | b1   | r2  | g2  | b2   | r   | g   | b   |
		| 0.9 | 0.6 | 0.75 | 0.7 | 0.1 | 0.25 | 1.6 | 0.7 | 1.0 |


Scenario Outline: Subtracting colors
	Given c1 equals color <r1> <g1> <b1>
	And c2 equals color <r2> <g2> <b2>
	Then c1 minus c2 equals color <r> <g> <b>

	Examples: All in normal range
		| r1  | g1  | b1   | r2  | g2  | b2   | r   | g   | b   |
		| 0.9 | 0.6 | 0.75 | 0.7 | 0.1 | 0.25 | 0.2 | 0.5 | 0.5 |

	Examples: Outside normal range
		| r1  | g1  | b1   | r2  | g2  | b2   | r    | g    | b    |
		| 0.7 | 0.1 | 0.25 | 0.9 | 0.6 | 0.75 | -0.2 | -0.5 | -0.5 |


Scenario: Multiplying a color by a scalar
	Given c1 equals color 0.2 0.3 0.4
	Then c1 multiplied by scalar 2 equals color 0.4 0.6 0.8


Scenario Outline: Multiplying colors
	Given c1 equals color <r1> <g1> <b1>
	And c2 equals color <r2> <g2> <b2>
	Then c1 multiplied by c2 equals color <r> <g> <b>

	Examples: Multiply colors
		| r1  | g1  | b1  | r2  | g2  | b2  | r   | g   | b    |
		| 1.0 | 0.2 | 0.4 | 0.9 | 1.0 | 0.1 | 0.9 | 0.2 | 0.04 |


