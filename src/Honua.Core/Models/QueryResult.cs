// Copyright (c) Honua. All rights reserved.
// Licensed under the Apache License 2.0. See LICENSE in the project root.

using System.Collections.Immutable;

namespace Honua.Core.Models;

/// <summary>
/// Represents the result of a feature query operation
/// </summary>
/// <typeparam name="T">Type of items in the result</typeparam>
public readonly record struct QueryResult<T>
{
    /// <summary>
    /// Features returned by the query
    /// </summary>
    public required ImmutableArray<T> Features { get; init; }

    /// <summary>
    /// Total number of features that match the query (may be more than returned due to limits)
    /// </summary>
    public long? TotalCount { get; init; }

    /// <summary>
    /// Whether there are more results available
    /// </summary>
    public bool HasMore { get; init; }

    /// <summary>
    /// Spatial extent of the returned features
    /// </summary>
    public BoundingBox? Extent { get; init; }

    /// <summary>
    /// Spatial reference of the results
    /// </summary>
    public SpatialReference? SpatialReference { get; init; }

    /// <summary>
    /// Field definitions for the returned features
    /// </summary>
    public ImmutableArray<FieldDefinition>? Fields { get; init; }

    /// <summary>
    /// Creates a query result with features
    /// </summary>
    /// <param name="features">Features to include</param>
    /// <param name="totalCount">Total count if known</param>
    /// <param name="hasMore">Whether more results are available</param>
    /// <returns>Query result instance</returns>
    public static QueryResult<T> Create(ImmutableArray<T> features, long? totalCount = null, bool hasMore = false)
        => new() { Features = features, TotalCount = totalCount, HasMore = hasMore };

    /// <summary>
    /// Creates a query result with spatial metadata
    /// </summary>
    /// <param name="features">Features to include</param>
    /// <param name="extent">Spatial extent</param>
    /// <param name="spatialReference">Spatial reference</param>
    /// <param name="fields">Field definitions</param>
    /// <param name="totalCount">Total count if known</param>
    /// <param name="hasMore">Whether more results are available</param>
    /// <returns>Query result instance</returns>
    public static QueryResult<T> Create(
        ImmutableArray<T> features,
        BoundingBox? extent,
        SpatialReference? spatialReference,
        ImmutableArray<FieldDefinition>? fields = null,
        long? totalCount = null,
        bool hasMore = false)
        => new()
        {
            Features = features,
            Extent = extent,
            SpatialReference = spatialReference,
            Fields = fields,
            TotalCount = totalCount,
            HasMore = hasMore
        };

    /// <summary>
    /// Creates an empty query result
    /// </summary>
    /// <returns>Empty query result</returns>
    public static QueryResult<T> Empty()
        => new() { Features = ImmutableArray<T>.Empty };

    /// <summary>
    /// Number of features in this result
    /// </summary>
    public int Count => Features.Length;

    /// <summary>
    /// Whether this result is empty
    /// </summary>
    public bool IsEmpty => Features.IsEmpty;

    /// <summary>
    /// Initializes a QueryResult with explicit constructor
    /// </summary>
    public QueryResult()
    {
        Features = ImmutableArray<T>.Empty;
    }
}

/// <summary>
/// Represents metadata about a field in a feature layer
/// </summary>
public readonly record struct FieldDefinition
{
    /// <summary>
    /// Field name
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Field type
    /// </summary>
    public required FieldType Type { get; init; }

    /// <summary>
    /// Field alias (display name)
    /// </summary>
    public string? Alias { get; init; }

    /// <summary>
    /// Field length for string fields
    /// </summary>
    public int? Length { get; init; }

    /// <summary>
    /// Whether the field allows null values
    /// </summary>
    public bool Nullable { get; init; } = true;

    /// <summary>
    /// Whether the field is editable
    /// </summary>
    public bool Editable { get; init; } = true;

    /// <summary>
    /// Default value for the field
    /// </summary>
    public object? DefaultValue { get; init; }

    /// <summary>
    /// Domain information for the field (for coded value domains)
    /// </summary>
    public FieldDomain? Domain { get; init; }

    /// <summary>
    /// Initializes a FieldDefinition with explicit constructor
    /// </summary>
    public FieldDefinition()
    {
        Nullable = true;
        Editable = true;
    }

    /// <summary>
    /// Creates a field definition
    /// </summary>
    /// <param name="name">Field name</param>
    /// <param name="type">Field type</param>
    /// <param name="alias">Field alias</param>
    /// <param name="nullable">Whether field allows nulls</param>
    /// <param name="editable">Whether field is editable</param>
    /// <returns>Field definition instance</returns>
    public static FieldDefinition Create(string name, FieldType type, string? alias = null, bool nullable = true, bool editable = true)
        => new() { Name = name, Type = type, Alias = alias, Nullable = nullable, Editable = editable };
}

/// <summary>
/// Represents a field domain for validation
/// </summary>
public readonly record struct FieldDomain
{
    /// <summary>
    /// Domain type
    /// </summary>
    public required FieldDomainType Type { get; init; }

    /// <summary>
    /// Domain name
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Coded values for coded value domains
    /// </summary>
    public ImmutableArray<CodedValue>? CodedValues { get; init; }

    /// <summary>
    /// Minimum value for range domains
    /// </summary>
    public object? MinValue { get; init; }

    /// <summary>
    /// Maximum value for range domains
    /// </summary>
    public object? MaxValue { get; init; }
}

/// <summary>
/// Represents a coded value in a domain
/// </summary>
public readonly record struct CodedValue
{
    /// <summary>
    /// Coded value
    /// </summary>
    public required object Code { get; init; }

    /// <summary>
    /// Display name for the coded value
    /// </summary>
    public required string Name { get; init; }
}

/// <summary>
/// Types of field domains
/// </summary>
public enum FieldDomainType
{
    /// <summary>
    /// Coded value domain with specific allowed values
    /// </summary>
    CodedValue,

    /// <summary>
    /// Range domain with min/max values
    /// </summary>
    Range
}