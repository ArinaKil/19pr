using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kino_Kilunina
{
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public MainWindow()
        {
            InitializeComponent();
            init = this;
            OpenPages(pages.kino);
        }

        public enum pages
        {
            kino,
            afisha
        }

        public void OpenPages(pages page)
        {
            switch (page)
            {
                case pages.kino:
                    frame.Navigate(new Pages.Kino());
                    break;
                case pages.afisha:
                    frame.Navigate(new Pages.Afisha());
                    break;
            }
        }
    }
}
