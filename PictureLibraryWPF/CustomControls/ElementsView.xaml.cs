using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for FilesView.xaml
    /// </summary>
    public partial class ElementsView : UserControl
    {
        private readonly IExplorableElementsViewViewModel _viewModel;

        public ElementsView(IExplorableElementsViewViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            _viewModel = viewModel;
        }

        public async Task Initialize()
        {
            await _viewModel.CommonViewModel.LoadCurrentlyShownElementsAsync();
        }

        protected void ItemMouseDoubleClick(object o, EventArgs args)
        {
            (DataContext as IExplorableElementsViewViewModel).CommonViewModel.CurrentlyOpenedElement = ((o as ListViewItem).DataContext as IExplorableElement);
        }

        protected void ElementsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as IExplorableElementsViewViewModel).CommonViewModel.SelectedElements.Clear();

            foreach (var t in ElementsListView.SelectedItems)
            {
                (DataContext as IExplorableElementsViewViewModel).CommonViewModel.SelectedElements.Add(t as IExplorableElement);
            }
        }
    }
}
