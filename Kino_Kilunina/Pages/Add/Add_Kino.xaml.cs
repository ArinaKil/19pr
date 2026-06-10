using Kino_Kilunina.Classes.Context;
using KinoModel = Kino_Kilunina.Classes.Model.Kino;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Kino_Kilunina.Pages.Add
{
    public partial class Add_Kino : Page
    {
        private readonly KinoContext _context = new KinoContext();
        private readonly KinoModel _editKino;
        private readonly Action _onSaved;

        public Add_Kino(KinoModel kino, Action onSaved)
        {
            InitializeComponent();
            _editKino = kino;
            _onSaved = onSaved;

            if (kino != null)
            {
                tbTitle.Text = "Изменить кинотеатр";
                tbName.Text = kino.Name;
                tbCountZal.Text = kino.CountZal.ToString();
                tbCount.Text = kino.Count.ToString();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text) ||
                !int.TryParse(tbCountZal.Text, out int countZal) ||
                !int.TryParse(tbCount.Text, out int count))
            {
                MessageBox.Show("Заполните все поля корректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_editKino == null)
                _context.Add(new KinoModel(0, tbName.Text, countZal, count));
            else
                _context.Update(new KinoModel(_editKino.Id, tbName.Text, countZal, count));

            _onSaved?.Invoke();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _onSaved?.Invoke();
        }
    }
}
