# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Initial release of Honua.Core library
- Core domain models for feature queries and spatial operations
- gRPC conversion helpers for Protocol Buffer message conversion
- Cross-platform support for .NET 10.0, Android, iOS, and macOS Catalyst
- Integration with geospatial-grpc protocol definitions
- Support for complex spatial filtering and statistical operations
- Apache 2.0 license for maximum open source compatibility

### Features
- `FeatureQuery` domain model with comprehensive querying capabilities
- `SpatialFilter` for geometric spatial operations
- `GrpcConversionHelpers` for seamless Protocol Buffer conversion
- Support for all major spatial relationships (Intersects, Contains, Within, etc.)
- Statistical operations (Count, Sum, Min, Max, Average, etc.)
- Distance-based spatial operations with multiple unit support
- Multi-platform targeting for server and mobile applications

## [1.0.0] - 2026-03-05

### Added
- Initial public release
- Foundation for Honua geospatial ecosystem
- Full gRPC protocol integration
- Mobile-first design for .NET MAUI applications