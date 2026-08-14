using Microsoft.AspNetCore.Builder;

namespace InventorySystem.Helpers
{
    /// <summary>No-op on Generic main. Full scale API is on the <c>scale</c> branch.</summary>
    public static class ScaleApiBootstrap
    {
        public static void WireBroadcasts() { }
        public static void MapEndpoints(WebApplication app) { }
    }
}
