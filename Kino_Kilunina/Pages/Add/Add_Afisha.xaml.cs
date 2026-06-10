using Kino_Kilunina.Classes.Context;
using AfishaModel = Kino_Kilunina.Classes.Model.Afisha;
using KinoModel = Kino_Kilunina.Classes.Model.Kino;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Kino_Kilunina.Pages.Add
{
    public partial class Add_Afisha : Page
    {
        private readonly AfishaContext _context = new AfishaContext();
        private readonly KinoContext _kinoContext = new KinoContext();
        private readonly AfishaModel _editAfisha;
        private readonly Action _onSaved;

        public Add_Afisha(AfishaModel afisha, Action onSaved)
        {
            InitializeComponent();
            _editAfisha = afisha;
            _onSaved = onSaved;

            for (int h = 0; h < 24; h++)
                cbHour.Items.Add(h.ToString("D2"));

            for (int m = 0; m < 60; m += 5)
                cbMinute.Items.Add(m.ToString("D2"));

            cbKinoteatr.ItemsSource = _kinoContext.GetAll();

            if (afisha != null)
            {
                tbTitle.Text = "Изменить сеанс";
                tbName.Text = afisha.Name;
                tbPrice.Text = afisha.Price.ToString();
                dpDate.SelectedDate = afisha.Time.Date;
                cbHour.SelectedItem = afisha.Time.Hour.ToString("D2");

                int nearestMinute = (afisha.Time.Minute / 5) * 5;
                cbMinute.SelectedItem = nearestMinute.ToString("D2");

                foreach (KinoModel kino in cbKinoteatr.Items)
                {
                    if (kino.Id == afisha.IdKinoteatr)
                    {
                        cbKinoteatr.SelectedItem = kino;
                        break;
                    }
                }
            }
            else
            {
                dpDate.SelectedDate = DateTime.Today;
                cbHour.SelectedItem = "10";
                cbMinute.SelectedItem = "00";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text) ||
                cbKinoteatr.SelectedItem == null ||
                dpDate.SelectedDate == null ||
                cbHour.SelectedItem == null ||
                cbMinute.SelectedItem == null ||
                !int.TryParse(tbPrice.Text, out int price))
            {
                MessageBox.Show("Заполните все поля корректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int hour = int.Parse((string)cbHour.SelectedItem);
            int minute = int.Parse((string)cbMinute.SelectedItem);
            DateTime time = dpDate.SelectedDate.Value.AddHours(hour).AddMinutes(minute);
            int idKino = ((KinoModel)cbKinoteatr.SelectedItem).Id;

            if (_editAfisha == null)
                _context.Add(new AfishaModel(0, idKino, tbName.Text, time, price));
            else
                _context.Update(new AfishaModel(_editAfisha.Id, idKino, tbName.Text, time, price));

            _onSaved?.Invoke();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _onSaved?.Invoke();
        }
    }
}
