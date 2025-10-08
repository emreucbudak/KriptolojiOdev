using KriptolojiOdev.Baglanti.Interface;

namespace KriptolojiOdev
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize(); // Önce yapýlandýrmayý baþlat

            SunucuForm serverForm = new SunucuForm();
            Form1 clientForm = new Form1(serverForm);

            serverForm.Show();
            clientForm.Show();

            Application.Run();
        }
    }
}