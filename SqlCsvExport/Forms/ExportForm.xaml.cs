using System;
using System.Linq;
using System.Windows;

namespace SqlCsvExport.Forms
{
    /// <summary>
    /// Логика взаимодействия для ExportForm.xaml
    /// </summary>
    public partial class ExportForm : Window
    {
        public ExportForm()
        {
            InitializeComponent();
        }

        private void OkBotton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ModelView.ExportModelView mv)
            {
                mv.DoExport();
            }
        }

        private void ChangeDir_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ModelView.ExportModelView mv)
            {
                ModelView.SetupModelView.ShowFolderDialog(mv);
            }
        }
    }
}
