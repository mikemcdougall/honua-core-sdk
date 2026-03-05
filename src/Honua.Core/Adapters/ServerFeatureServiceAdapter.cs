// Copyright (c) Honua. All rights reserved.
// Licensed under the Apache License 2.0. See LICENSE in the project root.

using System.Collections.Immutable;
using Honua.Core.Abstractions;
using Honua.Core.Models;

namespace Honua.Core.Adapters;

/// <summary>
/// Adapter that bridges generic IFeatureService to server-specific implementations using CancellationToken
/// </summary>
/// <param name="innerService">The underlying server-specific feature service</param>
public class ServerFeatureServiceAdapter(IServerFeatureService innerService)
    : IFeatureService<ServerContext>
{
    private readonly IServerFeatureService _innerService = innerService ?? throw new ArgumentNullException(nameof(innerService));

    /// <inheritdoc />
    public async ValueTask<ImmutableArray<Feature>> GetFeaturesAsync(int layerId, FeatureQuery query, ServerContext context)
    {
        return await _innerService.GetFeaturesAsync(layerId, query, context.CancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<Feature?> GetFeatureAsync(int layerId, long featureId, ServerContext context)
    {
        return await _innerService.GetFeatureAsync(layerId, featureId, context.CancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<long> CountFeaturesAsync(int layerId, FeatureQuery query, ServerContext context)
    {
        return await _innerService.CountFeaturesAsync(layerId, query, context.CancellationToken);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<Feature> StreamFeaturesAsync(int layerId, FeatureQuery query, ServerContext context)
    {
        return _innerService.StreamFeaturesAsync(layerId, query, context.CancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<Feature> CreateFeatureAsync(int layerId, Feature feature, ServerContext context)
    {
        return await _innerService.CreateFeatureAsync(layerId, feature, context.CancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<Feature> UpdateFeatureAsync(int layerId, Feature feature, ServerContext context)
    {
        return await _innerService.UpdateFeatureAsync(layerId, feature, context.CancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<bool> DeleteFeatureAsync(int layerId, long featureId, ServerContext context)
    {
        return await _innerService.DeleteFeatureAsync(layerId, featureId, context.CancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<FeatureEditResult> ApplyEditsAsync(int layerId, FeatureEditBatch editBatch, ServerContext context)
    {
        return await _innerService.ApplyEditsAsync(layerId, editBatch, context.CancellationToken);
    }
}

/// <summary>
/// Server-specific feature service interface using CancellationToken
/// This interface should be implemented by server-side data access layers
/// </summary>
public interface IServerFeatureService
{
    /// <summary>
    /// Gets features by query
    /// </summary>
    ValueTask<ImmutableArray<Feature>> GetFeaturesAsync(int layerId, FeatureQuery query, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a single feature by ID
    /// </summary>
    ValueTask<Feature?> GetFeatureAsync(int layerId, long featureId, CancellationToken cancellationToken);

    /// <summary>
    /// Counts features matching a query
    /// </summary>
    ValueTask<long> CountFeaturesAsync(int layerId, FeatureQuery query, CancellationToken cancellationToken);

    /// <summary>
    /// Streams features for large result sets
    /// </summary>
    IAsyncEnumerable<Feature> StreamFeaturesAsync(int layerId, FeatureQuery query, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new feature
    /// </summary>
    ValueTask<Feature> CreateFeatureAsync(int layerId, Feature feature, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing feature
    /// </summary>
    ValueTask<Feature> UpdateFeatureAsync(int layerId, Feature feature, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a feature
    /// </summary>
    ValueTask<bool> DeleteFeatureAsync(int layerId, long featureId, CancellationToken cancellationToken);

    /// <summary>
    /// Applies a batch of edits with transaction support
    /// </summary>
    ValueTask<FeatureEditResult> ApplyEditsAsync(int layerId, FeatureEditBatch editBatch, CancellationToken cancellationToken);
}