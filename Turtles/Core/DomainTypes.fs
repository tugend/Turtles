namespace Core.DomainTypes

/// An alias for a float
type Distance = float

/// Use a unit of measure to make it clear that the angle is in degrees, not radians
[<Measure>]
type Degrees

/// An alias for a float of Degrees
type Angle = float<Degrees>

/// Enumeration of available pen states
type PenState =
    | Up
    | Down

/// Enumeration of available pen colors
type PenColor =
    | Black
    | Red
    | Blue

/// A structure to store the (x,y) coordinates
type Position = { X: float; Y: float }
