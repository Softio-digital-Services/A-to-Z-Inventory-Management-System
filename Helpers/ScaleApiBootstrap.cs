using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace InventorySystem.Helpers
{
    /// <summary>
    /// Scale hardware is not available on this build.
    /// Endpoints remain so the web UI does not error when calling /api/scale/*.
    /// </summary>
    public static class ScaleApiBootstrap
    {
        public static void WireBroadcasts() { }

        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/api/scale/ports", () => Results.Ok(System.Array.Empty<object>()));
            app.MapGet("/api/scale/status", () => Results.Ok(new
            {
                connected = false,
                weight = 0d,
                unit = "kg",
                stable = true,
                port = "",
                message = "Scale disabled on this build"
            }));
            app.MapPost("/api/scale/config", () => Results.Ok(new { success = true, message = "Scale disabled on this build" }));
            app.MapPost("/api/scale/connect", () => Results.Ok(new { success = false, message = "Scale disabled on this build" }));
            app.MapPost("/api/scale/disconnect", () => Results.Ok(new { success = true }));
            app.MapPost("/api/scale/tare", () => Results.Ok(new { success = false, message = "Scale disabled on this build" }));
            app.MapPost("/api/scale/zero", () => Results.Ok(new { success = false, message = "Scale disabled on this build" }));
            app.MapPost("/api/scale/request", () => Results.Ok(new { success = false, weight = 0d, message = "Scale disabled on this build" }));
            app.MapPost("/api/scale/simulate", () => Results.Ok(new { success = false, message = "Scale disabled on this build" }));
            app.MapPost("/api/scale/resolve-barcode", () => Results.Ok(new { handled = false, message = "Scale barcodes disabled" }));
        }
    }
}
