// Copyright (c) 2026 Honua Project Contributors
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Immutable;
using Google.Protobuf;
using Honua.Core.Models;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
// TODO: Uncomment when geospatial-grpc package is available
// using Geospatial.V1;

namespace Honua.Core.Converters;

/// <summary>
/// Conversion helpers between domain types and gRPC proto messages.
/// Uses protocol definitions from geospatial-grpc standard.
/// Works on both server and mobile clients to ensure consistent behavior.
///
/// NOTE: This class is currently a placeholder. Full implementation will be
/// available once the geospatial-grpc protocol definitions are published.
/// </summary>
public static class GrpcConversionHelpers
{
    private static readonly GeometryFactory _geoFactory = new();

    [ThreadStatic]
    private static WKBReader? _wkbReader;
    [ThreadStatic]
    private static WKBWriter? _wkbWriter;

    private static WKBReader WkbReader => _wkbReader ??= new WKBReader();
    private static WKBWriter WkbWriter => _wkbWriter ??= new WKBWriter();

    // TODO: Add conversion methods when geospatial-grpc types are available
    // The following methods will be implemented:
    // - ToFeatureQuery(QueryFeaturesRequest request)
    // - ToProtoRequest(FeatureQuery query, string serviceId, int layerId)
    // - ToNtsGeometry(proto geometry)
    // - ToProtoGeometry(NTS geometry)
    // - Spatial relationship conversions
    // - Distance unit conversions
    // - Statistic type conversions
}