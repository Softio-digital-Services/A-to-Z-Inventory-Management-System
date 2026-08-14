namespace InventorySystem.Helpers
{
    public enum ScaleBarcodeType
    {
        Unknown = 0,
        WeightBased = 1,
        PriceBased = 2
    }

    public class ScaleBarcodeResult
    {
        public string ProductCode { get; set; }
        public decimal WeightKg { get; set; }
        public decimal TotalPrice { get; set; }
        public ScaleBarcodeType BarcodeType { get; set; } = ScaleBarcodeType.Unknown;
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }

    /// <summary>Disabled on Generic main — scale barcodes are not parsed.</summary>
    public static class ScaleBarcodeParser
    {
        public static bool IsScaleBarcode(string barcode) => false;

        public static ScaleBarcodeResult Parse(string barcode) =>
            new ScaleBarcodeResult
            {
                BarcodeType = ScaleBarcodeType.Unknown,
                Message = "Scale barcodes disabled",
                IsSuccess = false
            };
    }
}
