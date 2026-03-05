// Copyright (c) Honua. All rights reserved.
// Licensed under the Apache License 2.0. See LICENSE in the project root.

using System.Collections.Immutable;
using Honua.Core.Abstractions;
using Honua.Core.Models;

namespace Honua.Core.Adapters;

/// <summary>
/// Adapter that bridges generic IFeatureService to mobile-specific implementations using progress reporting
/// </summary>
/// <param name="innerService">The underlying mobile-specific feature service</param>
public class MobileFeatureServiceAdapter(IMobileFeatureService innerService)
    : IFeatureService<MobileContext>
{
    private readonly IMobileFeatureService _innerService = innerService ?? throw new ArgumentNullException(nameof(innerService));

    /// <inheritdoc />
    public async ValueTask<ImmutableArray<Feature>> GetFeaturesAsync(int layerId, FeatureQuery query, MobileContext context)
    {
        return await _innerService.GetFeaturesAsync(layerId, query, context.Progress, context.CancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<Feature?> GetFeatureAsync(int layerId, long featureId, MobileContext context)
    {
        return await _innerService.GetFeatureAsync(layerId, featureId, context.CancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<long> CountFeaturesAsync(int layerId, FeatureQuery query, MobileContext context)
    {
        return await _innerService.CountFeaturesAsync(layerId, query, context.CancellationToken);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<Feature> StreamFeaturesAsync(int layerId, FeatureQuery query, MobileContext context)
    {
        return _innerService.StreamFeaturesAsync(layerId, query, context.Progress, context.CancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<Feature> CreateFeatureAsync(int layerId, Feature feature, MobileContext context)
    {
        return await _innerService.CreateFeatureAsync(layerId, feature, context.Progress, context.CancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<Feature> UpdateFeatureAsync(int layerId, Feature feature, MobileContext context)
    {
        return await _innerService.UpdateFeatureAsync(layerId, feature, context.Progress, context.CancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<bool> DeleteFeatureAsync(int layerId, long featureId, MobileContext context)
    {
        return await _innerService.DeleteFeatureAsync(layerId, featureId, context.CancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<FeatureEditResult> ApplyEditsAsync(int layerId, FeatureEditBatch editBatch, MobileContext context)
    {
        return await _innerService.ApplyEditsAsync(layerId, editBatch, context.Progress, context.CancellationToken);
    }
}

/// <summary>
/// Mobile-specific feature service interface using progress reporting
/// This interface should be implemented by mobile client data access layers
/// </summary>
public interface IMobileFeatureService
{
    /// <summary>
    /// Gets features by query with progress reporting
    /// </summary>
    ValueTask<ImmutableArray<Feature>> GetFeaturesAsync(
        int layerId,
        FeatureQuery query,
        IProgress<OperationProgress>? progress = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single feature by ID
    /// </summary>
    ValueTask<Feature?> GetFeatureAsync(int layerId, long featureId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts features matching a query
    /// </summary>
    ValueTask<long> CountFeaturesAsync(int layerId, FeatureQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Streams features for large result sets with progress reporting
    /// </summary>
    IAsyncEnumerable<Feature> StreamFeaturesAsync(
        int layerId,
        FeatureQuery query,
        IProgress<OperationProgress>? progress = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new feature with progress reporting
    /// </summary>
    ValueTask<Feature> CreateFeatureAsync(
        int layerId,
        Feature feature,
        IProgress<OperationProgress>? progress = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing feature with progress reporting
    /// </summary>
    ValueTask<Feature> UpdateFeatureAsync(
        int layerId,
        Feature feature,
        IProgress<OperationProgress>? progress = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a feature
    /// </summary>
    ValueTask<bool> DeleteFeatureAsync(int layerId, long featureId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Applies a batch of edits with transaction support and progress reporting
    /// </summary>
    ValueTask<FeatureEditResult> ApplyEditsAsync(
        int layerId,
        FeatureEditBatch editBatch,
        IProgress<OperationProgress>? progress = null,
        CancellationToken cancellationToken = default);
}