// Copyright (c) Honua. All rights reserved.
// Licensed under the Apache License 2.0. See LICENSE in the project root.

using System.Collections.Immutable;
using Honua.Core.Models;

namespace Honua.Core.Extensions;

/// <summary>
/// Extension methods for FeatureQuery to enable fluent API
/// </summary>
public static class FeatureQueryExtensions
{
    /// <summary>
    /// Adds a WHERE clause to the query
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="where">WHERE clause expression</param>
    /// <returns>New query with WHERE clause</returns>
    public static FeatureQuery WithWhere(this FeatureQuery query, string where)
        => query with { Where = where };

    /// <summary>
    /// Adds object IDs filter to the query
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="objectIds">Object IDs to filter by</param>
    /// <returns>New query with object IDs filter</returns>
    public static FeatureQuery WithObjectIds(this FeatureQuery query, params long[] objectIds)
        => query with { ObjectIds = objectIds.ToImmutableArray() };

    /// <summary>
    /// Adds output fields to the query
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="fields">Fields to include in results</param>
    /// <returns>New query with output fields</returns>
    public static FeatureQuery WithFields(this FeatureQuery query, params string[] fields)
        => query with { OutFields = fields.ToImmutableArray() };

    /// <summary>
    /// Adds spatial filter to the query
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="spatialFilter">Spatial filter to apply</param>
    /// <returns>New query with spatial filter</returns>
    public static FeatureQuery WithSpatialFilter(this FeatureQuery query, SpatialFilter spatialFilter)
        => query with { SpatialFilter = spatialFilter };

    /// <summary>
    /// Adds temporal filter to the query
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="temporalFilter">Temporal filter to apply</param>
    /// <returns>New query with temporal filter</returns>
    public static FeatureQuery WithTemporalFilter(this FeatureQuery query, TemporalFilter temporalFilter)
        => query with { TemporalFilter = temporalFilter };

    /// <summary>
    /// Adds pagination to the query
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="offset">Number of records to skip</param>
    /// <param name="limit">Maximum number of records to return</param>
    /// <returns>New query with pagination</returns>
    public static FeatureQuery WithPagination(this FeatureQuery query, int offset, int limit)
        => query with { Offset = offset, Limit = limit };

    /// <summary>
    /// Adds ordering to the query
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="orderBy">Order by clauses</param>
    /// <returns>New query with ordering</returns>
    public static FeatureQuery WithOrderBy(this FeatureQuery query, params OrderByClause[] orderBy)
        => query with { OrderBy = orderBy.ToImmutableArray() };

    /// <summary>
    /// Adds ascending ordering by field to the query
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="field">Field to order by</param>
    /// <returns>New query with ascending ordering</returns>
    public static FeatureQuery OrderByAsc(this FeatureQuery query, string field)
        => query.WithOrderBy(OrderByClause.Asc(field));

    /// <summary>
    /// Adds descending ordering by field to the query
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="field">Field to order by</param>
    /// <returns>New query with descending ordering</returns>
    public static FeatureQuery OrderByDesc(this FeatureQuery query, string field)
        => query.WithOrderBy(OrderByClause.Desc(field));

    /// <summary>
    /// Sets distinct flag on the query
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="distinct">Whether to return distinct rows</param>
    /// <returns>New query with distinct setting</returns>
    public static FeatureQuery WithDistinct(this FeatureQuery query, bool distinct = true)
        => query with { Distinct = distinct };

    /// <summary>
    /// Adds statistics to the query
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="statistics">Statistics definitions</param>
    /// <returns>New query with statistics</returns>
    public static FeatureQuery WithStatistics(this FeatureQuery query, params StatisticDefinition[] statistics)
        => query with { OutStatistics = statistics.ToImmutableArray() };

    /// <summary>
    /// Adds group by fields to the query
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="groupByFields">Fields to group by</param>
    /// <returns>New query with group by fields</returns>
    public static FeatureQuery WithGroupBy(this FeatureQuery query, params string[] groupByFields)
        => query with { GroupByFields = groupByFields.ToImmutableArray() };

    /// <summary>
    /// Sets output spatial reference for the query
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="srid">Output spatial reference ID</param>
    /// <returns>New query with output spatial reference</returns>
    public static FeatureQuery WithOutputSrid(this FeatureQuery query, int srid)
        => query with { OutputSrid = srid };

    /// <summary>
    /// Sets whether to include null geometries when spatial filter is applied
    /// </summary>
    /// <param name="query">Source query</param>
    /// <param name="include">Whether to include null geometries</param>
    /// <returns>New query with null geometry setting</returns>
    public static FeatureQuery WithIncludeNullGeometry(this FeatureQuery query, bool include = true)
        => query with { IncludeNullGeometry = include };

    /// <summary>
    /// Creates a query for counting features
    /// </summary>
    /// <param name="query">Source query</param>
    /// <returns>New query optimized for counting</returns>
    public static FeatureQuery ForCount(this FeatureQuery query)
        => query with
        {
            OutFields = ImmutableArray<string>.Empty,
            OrderBy = null,
            Limit = null,
            Offset = null
        };

    /// <summary>
    /// Creates a query for getting feature extents
    /// </summary>
    /// <param name="query">Source query</param>
    /// <returns>New query optimized for extent calculation</returns>
    public static FeatureQuery ForExtent(this FeatureQuery query)
        => query with
        {
            OutFields = ImmutableArray<string>.Empty,
            OrderBy = null,
            Limit = null,
            Offset = null,
            Distinct = false
        };
}