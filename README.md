# Honua.Core

[![NuGet Version](https://img.shields.io/nuget/v/Honua.Core.svg)](https://www.nuget.org/packages/Honua.Core)
[![License: Apache 2.0](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

**Honua.Core** is the foundational .NET library for building applications with Honua geospatial services. This library provides core domain models, gRPC conversion utilities, and shared abstractions for both server-side and mobile client implementations.

## 🚀 Features

- **Cross-Platform Support**: Targets .NET 10.0, .NET 10.0-android, .NET 10.0-ios, and .NET 10.0-maccatalyst
- **gRPC-First Protocol**: Built around modern gRPC geospatial protocols
- **Domain Models**: Rich domain models for feature queries, spatial filtering, and geospatial operations
- **Protocol Buffers Integration**: Seamless conversion between domain types and Protocol Buffer messages
- **Mobile-Ready**: Optimized for .NET MAUI mobile applications
- **Open Source**: Apache 2.0 licensed for maximum flexibility

## 📦 Installation

Install via NuGet Package Manager:

```bash
dotnet add package Honua.Core
```

Or via Package Manager Console:

```powershell
Install-Package Honua.Core
```

## 🏗️ Architecture

Honua.Core is designed to be the shared foundation for:

- **Server Applications**: Core domain logic and gRPC message conversion
- **Mobile Applications**: Feature query building and spatial operations
- **Desktop Applications**: Cross-platform geospatial functionality
- **Web Applications**: Client-side spatial data processing

## 📂 Project Structure

```
src/
  Honua.Core/           # Main library project
    Models/             # Domain models (FeatureQuery, SpatialFilter, etc.)
    Converters/         # gRPC conversion helpers
    Services/           # Core service abstractions
    Extensions/         # Extension methods
    Constants/          # Shared constants
tests/
  Honua.Core.Tests/     # Unit tests
docs/                   # Documentation
```

## 🔧 Usage

### Feature Queries

Build complex spatial feature queries:

```csharp
using Honua.Core.Models;
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

### gRPC Protocol Conversion

Convert between domain models and Protocol Buffer messages:

```csharp
using Honua.Core.Converters;
using Geospatial.V1; // From geospatial-grpc standard

// Convert domain query to gRPC request
var grpcRequest = GrpcConversionHelpers.ToProtoRequest(query, "my-service", 0);

// Convert gRPC request back to domain model
var domainQuery = GrpcConversionHelpers.ToFeatureQuery(grpcRequest);
```

## 🌐 gRPC Geospatial Standards

This library implements conversion utilities for the emerging **gRPC geospatial protocol standard** defined in the [geospatial-grpc](https://github.com/mikemcdougall/geospatial-grpc) repository. This represents the next generation of open geospatial protocols, moving beyond traditional REST/OGC to modern, efficient gRPC-based communication.

## 🤝 Contributing

We welcome contributions! Please see our [Contributing Guide](CONTRIBUTING.md) for details.

## 📄 License

This project is licensed under the Apache License 2.0 - see the [LICENSE](LICENSE) file for details.

## 🔗 Related Projects

- **[honua-server](https://github.com/mikemcdougall/honua-server)**: Server implementation using Honua.Core
- **[geospatial-grpc](https://github.com/mikemcdougall/geospatial-grpc)**: Protocol Buffer definitions for geospatial services
- **[honua-mobile](https://github.com/mikemcdougall/honua-mobile)**: .NET MAUI mobile SDK using Honua.Core

## 📞 Support

- [Issues](https://github.com/mikemcdougall/honua-core/issues): Bug reports and feature requests
- [Discussions](https://github.com/mikemcdougall/honua-core/discussions): Community support and questions

---

Built with ❤️ by the Honua Project Contributors
