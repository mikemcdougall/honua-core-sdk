// Copyright (c) Honua. All rights reserved.
// Licensed under the Apache License 2.0. See LICENSE in the project root.

using System.Collections.Immutable;
using Honua.Core.Models;

namespace Honua.Core.Converters;

/// <summary>
/// Utility helper for building and manipulating FeatureQuery instances
/// </summary>
public static class FeatureQueryHelper
{
    /// <summary>
    /// Creates a query for retrieving all features
    /// </summary>
    /// <param name="limit">Optional limit on number of features</param>
    /// <returns>Query for all features</returns>
    public static FeatureQuery All(int? limit = null)
        => new() { Limit = limit };

    /// <summary>
    /// Creates a query for a specific feature by ID
    /// </summary>
    /// <param name="featureId">Feature ID to query</param>
    /// <returns>Query for specific feature</returns>
    public static FeatureQuery ById(long featureId)
        => new() { ObjectIds = ImmutableArray.Create(featureId) };

    /// <summary>
    /// Creates a query for multiple features by IDs
    /// </summary>
    /// <param name="featureIds">Feature IDs to query</param>
    /// <returns>Query for multiple features</returns>
    public static FeatureQuery ByIds(params long[] featureIds)
        => new() { ObjectIds = featureIds.ToImmutableArray() };

    /// <summary>
    /// Creates a spatial intersection query
    /// </summary>
    /// <param name="geometry">Filter geometry in WKB format</param>
    /// <param name="srid">SRID of the filter geometry</param>
    /// <returns>Spatial intersection query</returns>
    public static FeatureQuery Intersects(byte[] geometry, int? srid = null)
        => new()
        {
            SpatialFilter = SpatialFilter.Create(geometry, SpatialRelationship.Intersects, srid)
        };

    /// <summary>
    /// Creates a bounding box intersection query
    /// </summary>
    /// <param name="bbox">Bounding box to query</param>
    /// <returns>Spatial query for bounding box</returns>
    public static FeatureQuery WithinBounds(BoundingBox bbox)
    {
        // Convert bounding box to WKB polygon
        var wkb = ConvertBoundingBoxToWkb(bbox);
        return new()
        {
            SpatialFilter = SpatialFilter.Create(wkb, SpatialRelationship.Intersects, bbox.SpatialReference)
        };
    }

    /// <summary>
    /// Creates a distance-based query
    /// </summary>
    /// <param name="geometry">Center geometry in WKB format</param>
    /// <param name="distance">Distance value</param>
    /// <param name="unit">Distance unit</param>
    /// <param name="srid">SRID of the center geometry</param>
    /// <returns>Distance query</returns>
    public static FeatureQuery WithinDistance(byte[] geometry, double distance, DistanceUnit unit = DistanceUnit.Meters, int? srid = null)
        => new()
        {
            SpatialFilter = SpatialFilter.CreateDistanceFilter(geometry, distance, unit, true, srid)
        };

    /// <summary>
    /// Creates a K-Nearest Neighbor query
    /// </summary>
    /// <param name="geometry">Reference geometry in WKB format</param>
    /// <param name="count">Number of nearest features to return</param>
    /// <param name="returnDistance">Whether to include distance in results</param>
    /// <param name="srid">SRID of the reference geometry</param>
    /// <returns>KNN query</returns>
    public static FeatureQuery NearestNeighbors(byte[] geometry, int count, bool returnDistance = false, int? srid = null)
        => new()
        {
            SpatialFilter = SpatialFilter.CreateKnnFilter(geometry, count, returnDistance, srid)
        };

    /// <summary>
    /// Creates a temporal range query
    /// </summary>
    /// <param name="fieldName">Temporal field name</param>
    /// <param name="start">Start of time range</param>
    /// <param name="end">End of time range</param>
    /// <param name="propertyType">Type of temporal property</param>
    /// <returns>Temporal query</returns>
    public static FeatureQuery TemporalRange(string fieldName, DateTimeOffset? start, DateTimeOffset? end, TemporalPropertyType propertyType = TemporalPropertyType.DateTime)
        => new()
        {
            TemporalFilter = new TemporalFilter
            {
                PropertyName = fieldName,
                PropertyType = propertyType,
                Start = start,
                End = end
            }
        };

    /// <summary>
    /// Creates a query with field selection
    /// </summary>
    /// <param name="fields">Fields to include in results</param>
    /// <returns>Query with field selection</returns>
    public static FeatureQuery WithFields(params string[] fields)
        => new() { OutFields = fields.ToImmutableArray() };

    /// <summary>
    /// Creates a paginated query
    /// </summary>
    /// <param name="pageSize">Number of features per page</param>
    /// <param name="pageNumber">Page number (1-based)</param>
    /// <returns>Paginated query</returns>
    public static FeatureQuery Page(int pageSize, int pageNumber = 1)
    {
        var offset = (pageNumber - 1) * pageSize;
        return new() { Offset = offset, Limit = pageSize };
    }

    /// <summary>
    /// Creates a query for counting features
    /// </summary>
    /// <param name="where">Optional WHERE clause</param>
    /// <returns>Count query optimized for performance</returns>
    public static FeatureQuery Count(string? where = null)
        => new()
        {
            Where = where,
            OutFields = ImmutableArray<string>.Empty,
            Limit = null,
            Offset = null
        };

    /// <summary>
    /// Converts a bounding box to WKB polygon
    /// Note: This is a simplified implementation. In a real scenario, you would use
    /// a proper geometry library like NetTopologySuite
    /// </summary>
    private static byte[] ConvertBoundingBoxToWkb(BoundingBox bbox)
    {
        // This is a placeholder - in reality you'd use NetTopologySuite or similar
        // to create proper WKB from the bounding box coordinates
        throw new NotImplementedException("Geometry conversion requires a spatial library like NetTopologySuite");
    }
}