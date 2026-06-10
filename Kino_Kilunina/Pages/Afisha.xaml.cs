using Kino_Kilunina.Classes.Context;
using Kino_Kilunina.Elements;
using AfishaModel = Kino_Kilunina.Classes.Model.Afisha;
using System.Windows;
using System.Windows.Controls;

namespace Kino_Kilunina.Pages
{
    public partial class Afisha : Page
    {
        private readonly AfishaContext _context = new AfishaContext();

        public Afisha()
        {
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            wpAfisha.Children.Clear();
            foreach (var afisha in _context.GetAll())
            {
                var card = new AfishaItem(afisha);
                card.OnEdit += OnEdit;
                card.OnDelete += OnDelete;
                wpAfisha.Children.Add(card);
            }
        }

        private void btnKino_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Kino());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var nav = NavigationService;
            nav.Navigate(new Add.Add_Afisha(null, () => { nav.GoBack(); LoadItems(); }));
        }

        private void OnEdit(AfishaModel afisha)
        {
            var nav = NavigationService;
            nav.Navigate(new Add.Add_Afisha(afisha, () => { nav.GoBack(); LoadItems(); }));
        }

        private void OnDelete(AfishaModel afisha)
        {
            _context.Delete(afisha.Id);
            LoadItems();
        }
    }
}
