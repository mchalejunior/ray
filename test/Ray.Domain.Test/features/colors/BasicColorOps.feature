Feature: BasicColorOpsFeature
	In order to draw realistic 3D scenes
	As a Ray Tracer
	I want the ability to draw pixels in different colors
 

# "Normal Range" for RGB values is 0..1. Zero is "not present", 1 is fully present.
# However, values outside of normal range should not throw exceptions.
# It's possible for colors to be outside of range, go through multiple transformations,
# and end up back within normal range!
# The .NET Color struct has a Clamp() function to bring all back within range (can result
# in color loss). So I guess we keep all calculations pure until the final result is
# available and then you Clamp - that'll minimize color loss and give the most accurate results.

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


# Add one color to another color.
# Use-case: Doesn't actually say in the text. However Googling does come up with
#  mixing. E.g. red + green = blue.
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


# Subtract one color from another color.
# Use-case: Doesn't actually say in the text. However Googling does come up with
#  mixing. E.g. red + green = blue. Subtract is a little more abstract / mathematical!
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


# Multiply a color by a scalar.
# Use-case: Doesn't actually say in the text. However Googling does come up with
#  lighten / darken. The closer you bring RGB values to 0 (black) the darker, the
#  closer to 1 (white), the lighter.
Scenario: Multiplying a color by a scalar
	Given c1 equals color 0.2 0.3 0.4
	Then c1 multiplied by scalar 2 equals color 0.4 0.6 0.8


# Multiply a color by another color.
# Use-case: Text does describe this one: Blend colors - you want to know the visible
#   color of a yellow-green surface when illuminated by a reddish-purple light.
Scenario Outline: Multiplying colors
	Given c1 equals color <r1> <g1> <b1>
	And c2 equals color <r2> <g2> <b2>
	Then c1 multiplied by c2 equals color <r> <g> <b>

	Examples: Multiply colors
		| r1  | g1  | b1  | r2  | g2  | b2  | r   | g   | b    |
		| 1.0 | 0.2 | 0.4 | 0.9 | 1.0 | 0.1 | 0.9 | 0.2 | 0.04 |


