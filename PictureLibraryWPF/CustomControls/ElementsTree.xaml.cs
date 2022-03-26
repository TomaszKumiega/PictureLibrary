using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for FilesTree.xaml
    /// </summary>
    public partial class ElementsTree : UserControl
    {
        private readonly IExplorableElementsTreeViewModel _viewModel;

        public ElementsTree(IExplorableElementsTreeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
        }

        public async Task Initialize()
        {
            await _viewModel.Initialize();
        }

        //TODO: remove
        protected void TreeViewItem_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var viewModel = DataContext as IExplorableElementsTreeViewModel;
            var item = sender as TreeViewItem;

            item.Focusable = true;
            item.Focus();
            item.Focusable = false;

            viewModel.SelectedNode = item.DataContext as IExplorableElement;

            e.Handled = true;
        }
    }
}
