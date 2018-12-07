using SqlCsvExport.ModelView;
using System;
using System.Linq;
using System.Windows;

namespace SqlCsvExport.Forms
{
    /// <summary>
    /// Логика взаимодействия для SetupForm.xaml
    /// </summary>
    public partial class SetupForm : Window
    {
        public SetupForm()
        {
            InitializeComponent();
        }

        private void OkBotton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is SetupModelView mv)
            {
                if (!mv.CheckFolder)
                {
                    MessageBox.Show("Неправильно указана папка");
                    return;
                }

                DialogResult = true;
            }
        }

        private void ChangeDir_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is SetupModelView mv)
            {
                SetupModelView.ShowFolderDialog(mv);
            }
        }
    }
}
