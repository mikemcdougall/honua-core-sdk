// Copyright (c) Honua. All rights reserved.
// Licensed under the Apache License 2.0. See LICENSE in the project root.

using System.Collections.Immutable;
using Honua.Core.Models;

namespace Honua.Core.Abstractions;

/// <summary>
/// Platform-agnostic interface for reading features from a data source
/// </summary>
/// <typeparam name="TContext">Platform-specific context (e.g., CancellationToken on server, ProgressToken on mobile)</typeparam>
public interface IFeatureReader<in TContext>
{
    /// <summary>
    /// Gets features by query
    /// </summary>
    /// <param name="layerId">Layer identifier</param>
    /// <param name="query">Feature query specification</param>
    /// <param name="context">Platform-specific context</param>
    /// <returns>Collection of features</returns>
    ValueTask<ImmutableArray<Feature>> GetFeaturesAsync(int layerId, FeatureQuery query, TContext context);

    /// <summary>
    /// Gets a single feature by ID
    /// </summary>
    /// <param name="layerId">Layer identifier</param>
    /// <param name="featureId">Feature identifier</param>
    /// <param name="context">Platform-specific context</param>
    /// <returns>Feature instance or null if not found</returns>
    ValueTask<Feature?> GetFeatureAsync(int layerId, long featureId, TContext context);

    /// <summary>
    /// Counts features matching a query
    /// </summary>
    /// <param name="layerId">Layer identifier</param>
    /// <param name="query">Feature query specification</param>
    /// <param name="context">Platform-specific context</param>
    /// <returns>Feature count</returns>
    ValueTask<long> CountFeaturesAsync(int layerId, FeatureQuery query, TContext context);

    /// <summary>
    /// Streams features for large result sets
    /// </summary>
    /// <param name="layerId">Layer identifier</param>
    /// <param name="query">Feature query specification</param>
    /// <param name="context">Platform-specific context</param>
    /// <returns>Async enumerable of features</returns>
    IAsyncEnumerable<Feature> StreamFeaturesAsync(int layerId, FeatureQuery query, TContext context);
}

/// <summary>
/// Platform-agnostic interface for writing/editing features
/// </summary>
/// <typeparam name="TContext">Platform-specific context</typeparam>
public interface IFeatureWriter<in TContext>
{
    /// <summary>
    /// Creates a new feature
    /// </summary>
    /// <param name="layerId">Layer identifier</param>
    /// <param name="feature">Feature to create</param>
    /// <param name="context">Platform-specific context</param>
    /// <returns>Created feature with assigned ID</returns>
    ValueTask<Feature> CreateFeatureAsync(int layerId, Feature feature, TContext context);

    /// <summary>
    /// Updates an existing feature
    /// </summary>
    /// <param name="layerId">Layer identifier</param>
    /// <param name="feature">Feature to update</param>
    /// <param name="context">Platform-specific context</param>
    /// <returns>Updated feature</returns>
    ValueTask<Feature> UpdateFeatureAsync(int layerId, Feature feature, TContext context);

    /// <summary>
    /// Deletes a feature
    /// </summary>
    /// <param name="layerId">Layer identifier</param>
    /// <param name="featureId">Feature identifier</param>
    /// <param name="context">Platform-specific context</param>
    /// <returns>True if deleted, false if not found</returns>
    ValueTask<bool> DeleteFeatureAsync(int layerId, long featureId, TContext context);

    /// <summary>
    /// Applies a batch of edits with transaction support
    /// </summary>
    /// <param name="layerId">Layer identifier</param>
    /// <param name="editBatch">Batch of edits to apply</param>
    /// <param name="context">Platform-specific context</param>
    /// <returns>Edit operation results</returns>
    ValueTask<FeatureEditResult> ApplyEditsAsync(int layerId, FeatureEditBatch editBatch, TContext context);
}

/// <summary>
/// Combined interface for reading and writing features
/// </summary>
/// <typeparam name="TContext">Platform-specific context</typeparam>
public interface IFeatureService<in TContext> : IFeatureReader<TContext>, IFeatureWriter<TContext>
{
}

/// <summary>
/// Represents a batch of feature editing operations
/// </summary>
public readonly record struct FeatureEditBatch
{
    /// <summary>
    /// Features to add
    /// </summary>
    public ImmutableArray<Feature> Adds { get; init; }

    /// <summary>
    /// Features to update
    /// </summary>
    public ImmutableArray<Feature> Updates { get; init; }

    /// <summary>
    /// Feature IDs to delete
    /// </summary>
    public ImmutableArray<long> Deletes { get; init; }

    /// <summary>
    /// Whether to rollback all changes if any operation fails
    /// </summary>
    public bool UseGlobalIds { get; init; }

    /// <summary>
    /// Whether to rollback all changes if any operation fails
    /// </summary>
    public bool RollbackOnFailure { get; init; }
}

/// <summary>
/// Result of a feature edit operation
/// </summary>
public readonly record struct FeatureEditResult
{
    /// <summary>
    /// Results of add operations
    /// </summary>
    public ImmutableArray<EditOperationResult> AddResults { get; init; }

    /// <summary>
    /// Results of update operations
    /// </summary>
    public ImmutableArray<EditOperationResult> UpdateResults { get; init; }

    /// <summary>
    /// Results of delete operations
    /// </summary>
    public ImmutableArray<EditOperationResult> DeleteResults { get; init; }
}

/// <summary>
/// Result of a single edit operation
/// </summary>
public readonly record struct EditOperationResult
{
    /// <summary>
    /// Feature ID of the affected feature
    /// </summary>
    public long FeatureId { get; init; }

    /// <summary>
    /// Whether the operation succeeded
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// Error message if operation failed
    /// </summary>
    public string? ErrorMessage { get; init; }

    /// <summary>
    /// Error code if operation failed
    /// </summary>
    public int? ErrorCode { get; init; }
}