using Kino_Kilunina.Classes.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Kino_Kilunina.Elements
{
    public partial class AfishaItem : UserControl
    {
        public event Action<Afisha> OnEdit;
        public event Action<Afisha> OnDelete;

        private Afisha _afisha;

        public AfishaItem(Afisha afisha)
        {
            InitializeComponent();
            SetData(afisha);
        }

        public void SetData(Afisha afisha)
        {
            _afisha = afisha;
            tbName.Text = afisha.Name;
            tbTime.Text = "Время: " + afisha.Time.ToString("dd.MM.yyyy HH:mm");
            tbPrice.Text = "Цена: " + afisha.Price + " руб.";
            tbKinoteatr.Text = "Кинотеатр ID: " + afisha.IdKinoteatr;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            OnEdit?.Invoke(_afisha);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Удалить «{_afisha.Name}»?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OnDelete?.Invoke(_afisha);
            }
        }
    }
}
