using MiTiendaPOS.Forms;

namespace MiTiendaPOS
{
    internal static class Program
    {
        
        
        
        [STAThread]
        static void Main()
        {

            ApplicationConfiguration.Initialize();
            Application.Run(new FrmVentas());
        }
    }
}