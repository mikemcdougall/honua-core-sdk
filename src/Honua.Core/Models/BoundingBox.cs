// Copyright (c) Honua. All rights reserved.
// Licensed under the Apache License 2.0. See LICENSE in the project root.

namespace Honua.Core.Models;

/// <summary>
/// Represents a geographic bounding box (extent)
/// </summary>
public readonly record struct BoundingBox
{
    /// <summary>
    /// Minimum X coordinate (west longitude)
    /// </summary>
    public required double XMin { get; init; }

    /// <summary>
    /// Minimum Y coordinate (south latitude)
    /// </summary>
    public required double YMin { get; init; }

    /// <summary>
    /// Maximum X coordinate (east longitude)
    /// </summary>
    public required double XMax { get; init; }

    /// <summary>
    /// Maximum Y coordinate (north latitude)
    /// </summary>
    public required double YMax { get; init; }

    /// <summary>
    /// Spatial reference system ID
    /// </summary>
    public int? SpatialReference { get; init; }

    /// <summary>
    /// Width of the bounding box
    /// </summary>
    public readonly double Width => XMax - XMin;

    /// <summary>
    /// Height of the bounding box
    /// </summary>
    public readonly double Height => YMax - YMin;

    /// <summary>
    /// Center point X coordinate
    /// </summary>
    public readonly double CenterX => (XMin + XMax) / 2.0;

    /// <summary>
    /// Center point Y coordinate
    /// </summary>
    public readonly double CenterY => (YMin + YMax) / 2.0;

    /// <summary>
    /// Whether the bounding box is valid (XMax >= XMin and YMax >= YMin)
    /// </summary>
    public readonly bool IsValid => XMax >= XMin && YMax >= YMin;

    /// <summary>
    /// Area of the bounding box
    /// </summary>
    public readonly double Area => IsValid ? Width * Height : 0.0;

    /// <summary>
    /// Creates a bounding box with specified coordinates
    /// </summary>
    /// <param name="xMin">Minimum X coordinate</param>
    /// <param name="yMin">Minimum Y coordinate</param>
    /// <param name="xMax">Maximum X coordinate</param>
    /// <param name="yMax">Maximum Y coordinate</param>
    /// <param name="spatialReference">Spatial reference system ID</param>
    /// <returns>Bounding box instance</returns>
    public static BoundingBox Create(double xMin, double yMin, double xMax, double yMax, int? spatialReference = null)
        => new() { XMin = xMin, YMin = yMin, XMax = xMax, YMax = yMax, SpatialReference = spatialReference };

    /// <summary>
    /// Creates a bounding box around a center point with specified width and height
    /// </summary>
    /// <param name="centerX">Center X coordinate</param>
    /// <param name="centerY">Center Y coordinate</param>
    /// <param name="width">Width of the bounding box</param>
    /// <param name="height">Height of the bounding box</param>
    /// <param name="spatialReference">Spatial reference system ID</param>
    /// <returns>Bounding box instance</returns>
    public static BoundingBox CreateFromCenter(double centerX, double centerY, double width, double height, int? spatialReference = null)
    {
        var halfWidth = width / 2.0;
        var halfHeight = height / 2.0;
        return Create(
            centerX - halfWidth,
            centerY - halfHeight,
            centerX + halfWidth,
            centerY + halfHeight,
            spatialReference
        );
    }

    /// <summary>
    /// Checks if this bounding box intersects with another
    /// </summary>
    /// <param name="other">Other bounding box</param>
    /// <returns>True if they intersect</returns>
    public readonly bool Intersects(BoundingBox other)
        => XMin <= other.XMax && XMax >= other.XMin && YMin <= other.YMax && YMax >= other.YMin;

    /// <summary>
    /// Checks if this bounding box contains another
    /// </summary>
    /// <param name="other">Other bounding box</param>
    /// <returns>True if this contains the other</returns>
    public readonly bool Contains(BoundingBox other)
        => XMin <= other.XMin && YMin <= other.YMin && XMax >= other.XMax && YMax >= other.YMax;

    /// <summary>
    /// Checks if this bounding box contains a point
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <returns>True if the point is contained</returns>
    public readonly bool Contains(double x, double y)
        => x >= XMin && x <= XMax && y >= YMin && y <= YMax;

    /// <summary>
    /// Expands this bounding box by a specified distance
    /// </summary>
    /// <param name="distance">Distance to expand</param>
    /// <returns>Expanded bounding box</returns>
    public readonly BoundingBox Expand(double distance)
        => Create(XMin - distance, YMin - distance, XMax + distance, YMax + distance, SpatialReference);

    /// <summary>
    /// Combines this bounding box with another to create a union
    /// </summary>
    /// <param name="other">Other bounding box</param>
    /// <returns>Union of both bounding boxes</returns>
    public readonly BoundingBox Union(BoundingBox other)
        => Create(
            Math.Min(XMin, other.XMin),
            Math.Min(YMin, other.YMin),
            Math.Max(XMax, other.XMax),
            Math.Max(YMax, other.YMax),
            SpatialReference ?? other.SpatialReference
        );
}