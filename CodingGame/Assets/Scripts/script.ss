import math

/*
The equation for this is u = sqrt(g*R/Sin(2*a)) where

u = initial velocity
g = gravity 9.81 m/s^2
R = Horizontal distance travelled
a = Angle of projection
*/

var R = targetPosition.x - canonPosition.x
var g = 9.81
var firingAngle = 45

var u = math.Sqrt(g*R/math.Sin(2*firingAngle))

canonController.FireCanon(firingAngle, u)