using Kino_Kilunina.Classes.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Kino_Kilunina.Elements
{
    public partial class KinoItem : UserControl
    {
        public event Action<Kino> OnEdit;
        public event Action<Kino> OnDelete;

        private Kino _kino;

        public KinoItem(Kino kino)
        {
            InitializeComponent();
            SetData(kino);
        }

        public void SetData(Kino kino)
        {
            _kino = kino;
            tbName.Text = kino.Name;
            tbCountZal.Text = "Залов: " + kino.CountZal;
            tbCount.Text = "Мест: " + kino.Count;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            OnEdit?.Invoke(_kino);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Удалить «{_kino.Name}»?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                OnDelete?.Invoke(_kino);
            }
        }
    }
}
