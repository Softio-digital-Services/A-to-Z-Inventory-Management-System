using System;

namespace InventorySystem.Helpers
{
    /// <summary>
    /// No-op scale stub for Generic <c>main</c> (hardware scale lives on the <c>scale</c> branch).
    /// </summary>
    public sealed class ScaleService
    {
        public static ScaleService Instance { get; } = new ScaleService();

        public event Action<decimal, string, bool> WeightReceived;
        public event Action<bool, string> StatusChanged;

        public bool IsConnected => false;
        public ScaleConfig Config { get; private set; } = new ScaleConfig();

        public void Connect(string portName = null, int baudRate = 0) =>
            StatusChanged?.Invoke(false, "Scale disabled on this build");

        public void Disconnect() { }
        public void RequestWeight() { }
        public void SendTare() { }
        public void SendZero() { }
        public void LoadConfig() { }
        public void SaveConfig() { }
        public void UpdateConfig(ScaleConfig next)
        {
            if (next == null) return;
            Config = next;
        }

        public void SimulateWeight(decimal weight, string unit = "kg", bool isStable = true)
        {
            WeightReceived?.Invoke(weight, unit ?? "kg", isStable);
        }
    }

    public class ScaleConfig
    {
        public string PortName { get; set; } = "";
        public int BaudRate { get; set; } = 9600;
        public int DataBits { get; set; } = 8;
        public string Parity { get; set; } = "None";
        public string StopBits { get; set; } = "One";
        public string DefaultUnit { get; set; } = "kg";
        public bool AutoConnect { get; set; }
    }
}
