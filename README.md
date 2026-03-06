# Honua Core SDK

[![NuGet Version](https://img.shields.io/nuget/v/Honua.Core.Sdk.svg)](https://www.nuget.org/packages/Honua.Core.Sdk)
[![License: Apache 2.0](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

**Honua Core SDK** is the runtime .NET library for building production applications with Honua geospatial services. This library provides pure runtime capabilities including feature queries, spatial filtering, and transport abstractions without any administrative functionality. Optimized for mobile applications, field data collection, and cross-platform deployment.

## 🚀 Features

- **Runtime-Focused**: Lightweight library optimized for production applications
- **Cross-Platform Support**: Targets .NET 10.0, .NET 10.0-android, .NET 10.0-ios, and .NET 10.0-maccatalyst
- **gRPC-First Protocol**: Built around modern gRPC geospatial protocols
- **Feature Queries**: Rich domain models for spatial filtering and geospatial operations
- **Mobile-Ready**: Optimized for .NET MAUI mobile applications and field data collection
- **Protocol Buffers Integration**: Seamless conversion between domain types and Protocol Buffer messages
- **Open Source**: Apache 2.0 licensed for maximum flexibility

## 📦 Installation

Install via NuGet Package Manager:

```bash
dotnet add package Honua.Core.Sdk
```

Or via Package Manager Console:

```powershell
Install-Package Honua.Core.Sdk
```

## 🏗️ Functionality-First Architecture

**Functionality-First Design**: This SDK focuses exclusively on runtime capabilities. For administrative functionality like service management, bulk operations, and user administration, use [Honua.Admin.Tools](https://github.com/mikemcdougall/honua-admin-tools).

The Honua ecosystem follows a **functionality-first architecture** with clear separation of concerns:

Honua.Core.Sdk is designed to be the runtime foundation for:

### Runtime vs Admin Separation

| Use Case | Package | Purpose |
|----------|---------|---------|
| **Feature Queries** | Honua.Core.Sdk | Runtime spatial operations |
| **Mobile Apps** | Honua.Core.Sdk | Field data collection |
| **Service Management** | [honua-admin-tools](https://github.com/mikemcdougall/honua-admin-tools) | Deploy and configure services |
| **User Administration** | [honua-admin-tools](https://github.com/mikemcdougall/honua-admin-tools) | Manage users and permissions |
| **Bulk Operations** | [honua-admin-tools](https://github.com/mikemcdougall/honua-admin-tools) | Import/export large datasets |

### This SDK is for Runtime Applications

Use **Honua.Core.Sdk** when you need to:
- Query features from Honua services in production applications
- Build mobile apps with field data collection
- Integrate spatial operations into web applications
- Perform real-time feature filtering and spatial analysis

Use **[honua-admin-tools](https://github.com/mikemcdougall/honua-admin-tools)** when you need to:
- Deploy and manage Honua services
- Perform bulk data operations
- Administer users and permissions
- Automate CI/CD pipelines

## 📂 Project Structure

```
src/
  Honua.Core.Sdk/       # Main runtime library project
    Models/             # Domain models (FeatureQuery, SpatialFilter, etc.)
    Clients/            # gRPC client implementations
    Converters/         # Protocol conversion utilities
    Extensions/         # Extension methods
    Constants/          # Shared constants
tests/
  Honua.Core.Sdk.Tests/ # Unit tests
docs/                   # Documentation
```

## 🔧 Usage Examples

### Feature Queries

Build complex spatial feature queries:

```csharp
using Honua.Core.Sdk.Models;
using System.Collections.Immutable;

var query = new FeatureQuery
{
    Where = "population > 100000",
    OutFields = ImmutableArray.Create("name", "population", "area"),
    ReturnGeometry = true,
    Count = 50,
    SpatialFilter = new SpatialFilter
    {
        FilterGeometry = myPolygon,
        Relationship = SpatialRelationship.Intersects
    }
};
```

### gRPC Client Usage

Query features from a Honua service:

```csharp
using Honua.Core.Sdk.Clients;

var client = new HonuaFeatureClient("https://api.honua.com");

// Query features
var features = await client.QueryFeaturesAsync(
    serviceId: "my-service",
    layerId: 0,
    query: query
);

// Process results
foreach (var feature in features)
{
    Console.WriteLine($"Feature {feature.ObjectId}: {feature.Attributes["name"]}");
}
```

### Mobile Applications

Integrate with .NET MAUI for field data collection:

```csharp
// Configure service in MauiProgram.cs
builder.Services.AddHonuaFeatureClient(options =>
{
    options.BaseUrl = "https://api.honua.com";
    options.ApiKey = "your-api-key";
    options.EnableOfflineSync = true;
});

// Use in your views
public partial class FieldDataPage : ContentPage
{
    private readonly IHonuaFeatureClient _client;

    public FieldDataPage(IHonuaFeatureClient client)
    {
        _client = client;
        InitializeComponent();
    }

    private async Task LoadNearbyFeatures()
    {
        var currentLocation = await Geolocation.GetLocationAsync();
        var buffer = GeometryHelper.CreateBuffer(currentLocation, 1000); // 1km radius

        var query = new FeatureQuery
        {
            SpatialFilter = new SpatialFilter
            {
                FilterGeometry = buffer,
                Relationship = SpatialRelationship.Intersects
            }
        };

        var features = await _client.QueryFeaturesAsync("field-assets", 0, query);
        DisplayFeatures(features);
    }
}
```

## 🌐 gRPC Geospatial Standards

This SDK implements the emerging **gRPC geospatial protocol standard** defined in the [geospatial-grpc](https://github.com/mikemcdougall/geospatial-grpc) repository. This represents the next generation of open geospatial protocols, moving beyond traditional REST/OGC to modern, efficient gRPC-based communication.

### Protocol Features

- **Type-Safe**: Protocol Buffer definitions ensure type safety across languages
- **Performance**: Binary serialization with streaming support
- **Extensible**: Easy to add new operations and data types
- **Cross-Platform**: Works across .NET, JavaScript, Python, and more

## 🤝 Contributing

We welcome contributions! Please see our [Contributing Guide](CONTRIBUTING.md) for details.

## 📄 License

This project is licensed under the Apache License 2.0 - see the [LICENSE](LICENSE) file for details.

## 🔗 Related Projects

**Runtime SDKs (Apache 2.0)**:
- **[honua-mobile-sdk](https://github.com/mikemcdougall/honua-mobile-sdk)**: .NET MAUI mobile SDK (future)
- **[honua-js-sdk](https://github.com/mikemcdougall/honua-js-sdk)**: JavaScript runtime SDK (future)
- **[honua-python-sdk](https://github.com/mikemcdougall/honua-python-sdk)**: Python runtime SDK (future)

**Administrative Tools (Apache 2.0)**:
- **[honua-admin-tools](https://github.com/mikemcdougall/honua-admin-tools)**: Multi-language admin tooling for service management

**Infrastructure (Mixed Licensing)**:
- **[honua-server](https://github.com/mikemcdougall/honua-server)**: Server implementation (ELv2)
- **[geospatial-grpc](https://github.com/mikemcdougall/geospatial-grpc)**: Open gRPC protocol definitions (Apache 2.0)

## 📞 Support

- [Issues](https://github.com/mikemcdougall/honua-core-sdk/issues): Bug reports and feature requests
- [Discussions](https://github.com/mikemcdougall/honua-core-sdk/discussions): Community support and questions

---

Built with ❤️ by the Honua Project Contributors
