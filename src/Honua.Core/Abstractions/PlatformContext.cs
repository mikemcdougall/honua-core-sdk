// Copyright (c) Honua. All rights reserved.
// Licensed under the Apache License 2.0. See LICENSE in the project root.

namespace Honua.Core.Abstractions;

/// <summary>
/// Context for server-side operations using CancellationToken
/// </summary>
public readonly record struct ServerContext
{
    /// <summary>
    /// Cancellation token for async operations
    /// </summary>
    public required CancellationToken CancellationToken { get; init; }

    /// <summary>
    /// Optional user context for authorization
    /// </summary>
    public string? UserId { get; init; }

    /// <summary>
    /// Creates a server context with cancellation token
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Server context instance</returns>
    public static ServerContext Create(CancellationToken cancellationToken)
        => new() { CancellationToken = cancellationToken };

    /// <summary>
    /// Creates a server context with user information
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="userId">User identifier</param>
    /// <returns>Server context instance</returns>
    public static ServerContext Create(CancellationToken cancellationToken, string? userId)
        => new() { CancellationToken = cancellationToken, UserId = userId };
}

/// <summary>
/// Context for mobile/client operations using progress reporting
/// </summary>
public readonly record struct MobileContext
{
    /// <summary>
    /// Progress reporter for long-running operations
    /// </summary>
    public IProgress<OperationProgress>? Progress { get; init; }

    /// <summary>
    /// Cancellation token for operation cancellation
    /// </summary>
    public CancellationToken CancellationToken { get; init; }

    /// <summary>
    /// Local database connection (if applicable)
    /// </summary>
    public string? ConnectionString { get; init; }

    /// <summary>
    /// Creates a mobile context with progress reporting
    /// </summary>
    /// <param name="progress">Progress reporter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Mobile context instance</returns>
    public static MobileContext Create(IProgress<OperationProgress>? progress = null, CancellationToken cancellationToken = default)
        => new() { Progress = progress, CancellationToken = cancellationToken };

    /// <summary>
    /// Creates a mobile context with local database
    /// </summary>
    /// <param name="connectionString">Local database connection string</param>
    /// <param name="progress">Progress reporter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Mobile context instance</returns>
    public static MobileContext Create(string connectionString, IProgress<OperationProgress>? progress = null, CancellationToken cancellationToken = default)
        => new() { ConnectionString = connectionString, Progress = progress, CancellationToken = cancellationToken };
}

/// <summary>
/// Progress information for long-running operations
/// </summary>
public readonly record struct OperationProgress
{
    /// <summary>
    /// Current progress value (0-100)
    /// </summary>
    public int PercentComplete { get; init; }

    /// <summary>
    /// Description of current operation
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Number of items processed
    /// </summary>
    public long ItemsProcessed { get; init; }

    /// <summary>
    /// Total number of items to process
    /// </summary>
    public long TotalItems { get; init; }

    /// <summary>
    /// Creates a progress report
    /// </summary>
    /// <param name="percentComplete">Percentage complete (0-100)</param>
    /// <param name="description">Operation description</param>
    /// <returns>Progress instance</returns>
    public static OperationProgress Create(int percentComplete, string? description = null)
        => new() { PercentComplete = percentComplete, Description = description };

    /// <summary>
    /// Creates a progress report with item counts
    /// </summary>
    /// <param name="itemsProcessed">Items processed</param>
    /// <param name="totalItems">Total items</param>
    /// <param name="description">Operation description</param>
    /// <returns>Progress instance</returns>
    public static OperationProgress Create(long itemsProcessed, long totalItems, string? description = null)
    {
        var percentComplete = totalItems > 0 ? (int)((itemsProcessed * 100) / totalItems) : 0;
        return new()
        {
            PercentComplete = percentComplete,
            Description = description,
            ItemsProcessed = itemsProcessed,
            TotalItems = totalItems
        };
    }
}