using System;
using System.Windows.Forms;
using Share_a_Ton.Forms;
using Share_a_Ton.Properties;

namespace Share_a_Ton
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Check whether this is the first time this user has ran the application.
            var firstTimeSetupPerformed = Settings.Default.FirstTimeSetupPerformed;

            // If this is the first time, show the Options screen to the user.
            if (!firstTimeSetupPerformed)
            {
                var options = new OptionsForm();
                options.ShowDialog();
                Settings.Default.FirstTimeSetupPerformed = true;
            }

            Application.Run(new MainForm());
        }
    }
}