using Kino_Kilunina.Classes.Context;
using Kino_Kilunina.Elements;
using KinoModel = Kino_Kilunina.Classes.Model.Kino;
using System.Windows;
using System.Windows.Controls;

namespace Kino_Kilunina.Pages
{
    public partial class Kino : Page
    {
        private readonly KinoContext _context = new KinoContext();

        public Kino()
        {
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            wpKino.Children.Clear();
            foreach (var kino in _context.GetAll())
            {
                var card = new KinoItem(kino);
                card.OnEdit += OnEdit;
                card.OnDelete += OnDelete;
                wpKino.Children.Add(card);
            }
        }

        private void btnAfisha_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Afisha());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var nav = NavigationService;
            nav.Navigate(new Add.Add_Kino(null, () => { nav.GoBack(); LoadItems(); }));
        }

        private void OnEdit(KinoModel kino)
        {
            var nav = NavigationService;
            nav.Navigate(new Add.Add_Kino(kino, () => { nav.GoBack(); LoadItems(); }));
        }

        private void OnDelete(KinoModel kino)
        {
            if (new AfishaContext().HasByKinoteatr(kino.Id))
            {
                MessageBox.Show($"Нельзя удалить кинотеатр «{kino.Name}»: в нём назначены сеансы.", "Удаление невозможно", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _context.Delete(kino.Id);
            LoadItems();
        }
    }
}
