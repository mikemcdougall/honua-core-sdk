# Honua.Core Usage Guide

## Overview

Honua.Core provides cross-platform domain models and abstractions for building geospatial applications. It supports server-side applications using `CancellationToken` and mobile applications using progress reporting.

## Core Models

### FeatureQuery

Build feature queries using fluent API:

```csharp
using Honua.Core.Models;
using Honua.Core.Extensions;

// Basic query with WHERE clause
var query = FeatureQuery.WithWhere("population > 100000")
    .WithFields("name", "population", "geometry")
    .WithPagination(0, 50)
    .OrderByDesc("population");

// Spatial query
var spatialQuery = FeatureQuery.WithSpatialFilter(
    SpatialFilter.Create(geometryWkb, SpatialRelationship.Intersects, 4326));

// Distance query
var distanceQuery = FeatureQuery.WithSpatialFilter(
    SpatialFilter.CreateDistanceFilter(pointWkb, 1000, DistanceUnit.Meters));

// KNN query
var knnQuery = FeatureQuery.WithSpatialFilter(
    SpatialFilter.CreateKnnFilter(pointWkb, 10, returnDistance: true));
```

### Feature

Represent geographic features with attributes and geometry:

```csharp
using Honua.Core.Models;
using System.Collections.Immutable;

var feature = Feature.Create(
    id: 123,
    geometry: geometryWkb, // WKB-encoded geometry
    attributes: ImmutableDictionary<string, object?>.Empty
        .Add("name", "Sample Feature")
        .Add("population", 50000)
);
```

## Platform Abstractions

### Server Context

For server-side applications:

```csharp
using Honua.Core.Abstractions;
using Honua.Core.Adapters;

// Create server context
var context = ServerContext.Create(cancellationToken, userId: "user123");

// Use with server adapter
var serverService = new ServerFeatureServiceAdapter(myServerImplementation);
var features = await serverService.GetFeaturesAsync(layerId, query, context);
```

### Mobile Context

For mobile applications with progress reporting:

```csharp
using Honua.Core.Abstractions;
using Honua.Core.Adapters;

// Create mobile context with progress reporting
var progress = new Progress<OperationProgress>(p =>
    Console.WriteLine($"Progress: {p.PercentComplete}% - {p.Description}"));
var context = MobileContext.Create(progress, cancellationToken);

// Use with mobile adapter
var mobileService = new MobileFeatureServiceAdapter(myMobileImplementation);
var features = await mobileService.GetFeaturesAsync(layerId, query, context);
```

## Implementation

### Server Implementation

Implement `IServerFeatureService` for server-side data access:

```csharp
public class MyServerFeatureService : IServerFeatureService
{
    public async ValueTask<ImmutableArray<Feature>> GetFeaturesAsync(
        int layerId,
        FeatureQuery query,
        CancellationToken cancellationToken)
    {
        // Your server-specific implementation using Entity Framework,
        // ADO.NET, or other data access technology
        return await GetFeaturesFromDatabase(layerId, query, cancellationToken);
    }

    // Implement other methods...
}
```

### Mobile Implementation

Implement `IMobileFeatureService` for mobile data access:

```csharp
public class MyMobileFeatureService : IMobileFeatureService
{
    public async ValueTask<ImmutableArray<Feature>> GetFeaturesAsync(
        int layerId,
        FeatureQuery query,
        IProgress<OperationProgress>? progress = null,
        CancellationToken cancellationToken = default)
    {
        // Your mobile-specific implementation using SQLite,
        // local files, or cached data
        progress?.Report(OperationProgress.Create(0, "Starting query..."));

        var result = await GetFeaturesFromLocalStorage(layerId, query, cancellationToken);

        progress?.Report(OperationProgress.Create(100, "Query completed"));
        return result;
    }

    // Implement other methods...
}
```

## Query Helpers

Use `FeatureQueryHelper` for common query patterns:

```csharp
using Honua.Core.Converters;

// Get all features
var allQuery = FeatureQueryHelper.All(limit: 1000);

// Get by ID
var byIdQuery = FeatureQueryHelper.ById(123);

// Spatial queries
var intersectsQuery = FeatureQueryHelper.Intersects(geometryWkb, srid: 4326);
var distanceQuery = FeatureQueryHelper.WithinDistance(pointWkb, 500, DistanceUnit.Meters);

// Pagination
var pageQuery = FeatureQueryHelper.Page(pageSize: 25, pageNumber: 3);

// Count query
var countQuery = FeatureQueryHelper.Count("status = 'active'");
```

## Spatial References

Work with spatial reference systems:

```csharp
using Honua.Core.Models;

// Use predefined spatial references
var wgs84 = SpatialReference.WGS84; // EPSG:4326
var webMercator = SpatialReference.WebMercator; // EPSG:3857

// Create custom spatial reference
var customSr = SpatialReference.Create(2154); // RGF93 / Lambert-93

// Check properties
if (wgs84.IsGeographic)
{
    Console.WriteLine($"WGS84 is geographic: {wgs84.DisplayName}");
}
```

## Exception Handling

Handle Honua-specific exceptions:

```csharp
using Honua.Core.Exceptions;

try
{
    var feature = await service.GetFeatureAsync(layerId, featureId, context);
}
catch (ResourceNotFoundException ex)
{
    Console.WriteLine($"Feature not found: {ex.Message}");
}
catch (ValidationException ex)
{
    Console.WriteLine($"Validation error: {ex.Message}");
    if (ex.Details != null)
    {
        foreach (var detail in ex.Details)
            Console.WriteLine($"  - {detail}");
    }
}
catch (HonuaException ex)
{
    Console.WriteLine($"Honua error: {ex.Message} (Code: {ex.ErrorCode})");
}
```

## Cross-Platform Considerations

- **Domain Models**: All models in `Honua.Core.Models` are platform-agnostic
- **Abstractions**: Use `IFeatureService<TContext>` for generic implementations
- **Adapters**: Use platform-specific adapters to bridge contexts
- **Dependencies**: Minimal dependencies for maximum compatibility
- **Serialization**: All models support JSON serialization

This design allows you to:
1. Share domain logic across server and mobile applications
2. Implement platform-specific optimizations in adapters
3. Maintain type safety with generic context parameters
4. Support both synchronous and asynchronous progress reporting