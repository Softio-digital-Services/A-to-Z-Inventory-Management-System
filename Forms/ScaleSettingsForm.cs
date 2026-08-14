using System.Windows.Forms;
using InventorySystem.Helpers;

namespace InventorySystem.Forms
{
    /// <summary>Placeholder — scale settings are available on the <c>scale</c> branch.</summary>
    public class ScaleSettingsForm : BaseModalForm
    {
        public ScaleSettingsForm()
        {
            TitleText = LocalizationManager.GetString("Scale_SettingsBtn", "Scale Settings");
            var lbl = new Label
            {
                AutoSize = true,
                Text = "Hardware scale is not included in this build.\nUse the 'scale' branch for TM-A17 / COM support.",
                Padding = new Padding(20)
            };
            ContentPanel.Controls.Add(lbl);
        }
    }
}
