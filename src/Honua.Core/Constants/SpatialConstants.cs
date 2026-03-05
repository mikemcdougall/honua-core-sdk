// Copyright (c) Honua. All rights reserved.
// Licensed under the Apache License 2.0. See LICENSE in the project root.

namespace Honua.Core.Constants;

/// <summary>
/// Spatial reference system constants
/// </summary>
public static class SpatialConstants
{
    /// <summary>
    /// WGS84 Geographic coordinate system (EPSG:4326) - longitude/latitude
    /// </summary>
    public const int WGS84_SRID = 4326;

    /// <summary>
    /// Web Mercator projection (EPSG:3857) - used by web mapping services
    /// </summary>
    public const int WEB_MERCATOR_SRID = 3857;

    /// <summary>
    /// Default SRID when none is specified
    /// </summary>
    public const int DEFAULT_SRID = WGS84_SRID;

    /// <summary>
    /// Earth's radius in meters (WGS84 spheroid)
    /// </summary>
    public const double EARTH_RADIUS_METERS = 6378137.0;

    /// <summary>
    /// Degrees to radians conversion factor
    /// </summary>
    public const double DEGREES_TO_RADIANS = Math.PI / 180.0;

    /// <summary>
    /// Radians to degrees conversion factor
    /// </summary>
    public const double RADIANS_TO_DEGREES = 180.0 / Math.PI;

    /// <summary>
    /// Meters per degree at the equator (approximate)
    /// </summary>
    public const double METERS_PER_DEGREE_AT_EQUATOR = 111320.0;

    /// <summary>
    /// Maximum latitude value for WGS84 (north pole)
    /// </summary>
    public const double MAX_LATITUDE = 90.0;

    /// <summary>
    /// Minimum latitude value for WGS84 (south pole)
    /// </summary>
    public const double MIN_LATITUDE = -90.0;

    /// <summary>
    /// Maximum longitude value for WGS84 (international date line east)
    /// </summary>
    public const double MAX_LONGITUDE = 180.0;

    /// <summary>
    /// Minimum longitude value for WGS84 (international date line west)
    /// </summary>
    public const double MIN_LONGITUDE = -180.0;
}

/// <summary>
/// Field name constants for common geospatial operations
/// </summary>
public static class FieldNames
{
    /// <summary>
    /// Standard object ID field name
    /// </summary>
    public const string OBJECTID = "OBJECTID";

    /// <summary>
    /// Standard geometry field name
    /// </summary>
    public const string SHAPE = "SHAPE";

    /// <summary>
    /// Alternative geometry field name
    /// </summary>
    public const string GEOMETRY = "GEOMETRY";

    /// <summary>
    /// Standard shape length field name
    /// </summary>
    public const string SHAPE_LENGTH = "SHAPE_Length";

    /// <summary>
    /// Standard shape area field name
    /// </summary>
    public const string SHAPE_AREA = "SHAPE_Area";

    /// <summary>
    /// Global ID field name (used for replication)
    /// </summary>
    public const string GLOBALID = "GlobalID";

    /// <summary>
    /// Created date field name
    /// </summary>
    public const string CREATED_DATE = "created_date";

    /// <summary>
    /// Created user field name
    /// </summary>
    public const string CREATED_USER = "created_user";

    /// <summary>
    /// Last edited date field name
    /// </summary>
    public const string LAST_EDITED_DATE = "last_edited_date";

    /// <summary>
    /// Last edited user field name
    /// </summary>
    public const string LAST_EDITED_USER = "last_edited_user";
}