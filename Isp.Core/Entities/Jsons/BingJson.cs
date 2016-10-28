using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Isp.Core.Entities.Jsons
{
    public class BingJson
    {
        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("instrumentation")]
        public Instrumentation Instrumentation { get; set; }

        [JsonProperty("readLink")]
        public string ReadLink { get; set; }

        [JsonProperty("webSearchUrl")]
        public string WebSearchUrl { get; set; }

        [JsonProperty("webSearchUrlPingSuffix")]
        public string WebSearchUrlPingSuffix { get; set; }

        [JsonProperty("totalEstimatedMatches")]
        public int TotalEstimatedMatches { get; set; }

        [JsonProperty("value")]
        public IList<Value> Value { get; set; }

        [JsonProperty("queryExpansions")]
        public IList<QueryExpansion> QueryExpansions { get; set; }

        [JsonProperty("nextOffsetAddCount")]
        public int NextOffsetAddCount { get; set; }

        [JsonProperty("pivotSuggestions")]
        public IList<PivotSuggestion> PivotSuggestions { get; set; }

        [JsonProperty("displayShoppingSourcesBadges")]
        public bool DisplayShoppingSourcesBadges { get; set; }

        [JsonProperty("displayRecipeSourcesBadges")]
        public bool DisplayRecipeSourcesBadges { get; set; }
    }

    public class Instrumentation
    {
        [JsonProperty("pingUrlBase")]
        public string PingUrlBase { get; set; }

        [JsonProperty("pageLoadPingUrl")]
        public string PageLoadPingUrl { get; set; }
    }

    public class Thumbnail
    {
        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class InsightsSourcesSummary
    {
        [JsonProperty("shoppingSourcesCount")]
        public int ShoppingSourcesCount { get; set; }

        [JsonProperty("recipeSourcesCount")]
        public int RecipeSourcesCount { get; set; }
    }

    public class Value
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("webSearchUrl")]
        public string WebSearchUrl { get; set; }

        [JsonProperty("webSearchUrlPingSuffix")]
        public string WebSearchUrlPingSuffix { get; set; }

        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [JsonProperty("datePublished")]
        public DateTime DatePublished { get; set; }

        [JsonProperty("contentUrl")]
        public string ContentUrl { get; set; }

        [JsonProperty("hostPageUrl")]
        public string HostPageUrl { get; set; }

        [JsonProperty("hostPageUrlPingSuffix")]
        public string HostPageUrlPingSuffix { get; set; }

        [JsonProperty("contentSize")]
        public string ContentSize { get; set; }

        [JsonProperty("encodingFormat")]
        public string EncodingFormat { get; set; }

        [JsonProperty("hostPageDisplayUrl")]
        public string HostPageDisplayUrl { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnail { get; set; }

        [JsonProperty("imageInsightsToken")]
        public string ImageInsightsToken { get; set; }

        [JsonProperty("insightsSourcesSummary")]
        public InsightsSourcesSummary InsightsSourcesSummary { get; set; }

        [JsonProperty("imageId")]
        public string ImageId { get; set; }

        [JsonProperty("accentColor")]
        public string AccentColor { get; set; }
    }

    public class QueryExpansion
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("displayText")]
        public string DisplayText { get; set; }

        [JsonProperty("webSearchUrl")]
        public string WebSearchUrl { get; set; }

        [JsonProperty("webSearchUrlPingSuffix")]
        public string WebSearchUrlPingSuffix { get; set; }

        [JsonProperty("searchLink")]
        public string SearchLink { get; set; }

        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnail { get; set; }
    }

    public class Suggestion
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("displayText")]
        public string DisplayText { get; set; }

        [JsonProperty("webSearchUrl")]
        public string WebSearchUrl { get; set; }

        [JsonProperty("webSearchUrlPingSuffix")]
        public string WebSearchUrlPingSuffix { get; set; }

        [JsonProperty("searchLink")]
        public string SearchLink { get; set; }

        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnail { get; set; }
    }

    public class PivotSuggestion
    {
        [JsonProperty("pivot")]
        public string Pivot { get; set; }

        [JsonProperty("suggestions")]
        public IList<Suggestion> Suggestions { get; set; }
    }
}