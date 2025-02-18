// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using Azure.Core;
using Azure.IoT.TimeSeriesInsights.Models;

namespace Azure.IoT.TimeSeriesInsights
{
    /// <summary> Time series instance that is returned by instances search call. Returned instance matched the search request and contains highlighted text to be displayed to the user if it is set to &apos;true&apos;. </summary>
    public partial class InstanceHit
    {
        /// <summary> Initializes a new instance of InstanceHit. </summary>
        internal InstanceHit()
        {
            TimeSeriesId = new ChangeTrackingList<object>();
            HierarchyIds = new ChangeTrackingList<string>();
        }

        /// <summary> Initializes a new instance of InstanceHit. </summary>
        /// <param name="timeSeriesId"> Time series ID of the time series instance that matched the search request. </param>
        /// <param name="name"> Name of the time series instance that matched the search request. May be null. </param>
        /// <param name="typeId"> Represents the type that time series instance which matched the search request belongs to. Never null. </param>
        /// <param name="hierarchyIds"> List of time series hierarchy IDs that time series instance which matched the search request belongs to. Cannot be used to lookup hierarchies. May be null. </param>
        /// <param name="highlights"> Highlighted text of time series instance to be displayed to the user. Highlighting inserts &lt;hit&gt; and &lt;/hit&gt; tags in the portions of text that matched the search string. Do not use any of the highlighted properties to do further API calls. </param>
        internal InstanceHit(IReadOnlyList<object> timeSeriesId, string name, string typeId, IReadOnlyList<string> hierarchyIds, InstanceHitHighlights highlights)
        {
            TimeSeriesId = timeSeriesId;
            Name = name;
            TypeId = typeId;
            HierarchyIds = hierarchyIds;
            Highlights = highlights;
        }

        /// <summary> Time series ID of the time series instance that matched the search request. </summary>
        public IReadOnlyList<object> TimeSeriesId { get; }
        /// <summary> Name of the time series instance that matched the search request. May be null. </summary>
        public string Name { get; }
        /// <summary> Represents the type that time series instance which matched the search request belongs to. Never null. </summary>
        public string TypeId { get; }
        /// <summary> List of time series hierarchy IDs that time series instance which matched the search request belongs to. Cannot be used to lookup hierarchies. May be null. </summary>
        public IReadOnlyList<string> HierarchyIds { get; }
        /// <summary> Highlighted text of time series instance to be displayed to the user. Highlighting inserts &lt;hit&gt; and &lt;/hit&gt; tags in the portions of text that matched the search string. Do not use any of the highlighted properties to do further API calls. </summary>
        public InstanceHitHighlights Highlights { get; }
    }
}
