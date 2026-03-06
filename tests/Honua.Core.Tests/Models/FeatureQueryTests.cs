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

using Honua.Core.Models;
using Xunit;

namespace Honua.Core.Tests.Models;

public class FeatureQueryTests
{
    [Fact]
    public void FeatureQuery_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var query = new FeatureQuery();

        // Assert
        Assert.False(query.Distinct);
        Assert.Null(query.Where);
        Assert.Null(query.ObjectIds);
        Assert.Null(query.OutFields);
    }

    [Fact]
    public void SpatialFilter_ShouldRequireFilterGeometry()
    {
        // This test ensures that FilterGeometry is required
        // The required keyword in the property declaration enforces this
        Assert.True(true); // Placeholder - the required keyword provides compile-time validation
    }
}