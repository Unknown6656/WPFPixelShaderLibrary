using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using wpfpslib;

namespace WPFTestApplication
{
    public partial class App
        : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MessageBox.Show($"Available pixel shaders:\n{string.Join("\n", from fx in PixelShaderEffectBase.AvailableEffects select $" > {fx}")}");
            
            base.OnStartup(e);
        }
    }
}
