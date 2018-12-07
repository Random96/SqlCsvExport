using System;
using System.Linq;
using System.Windows.Forms;

namespace SqlCsvExport.ModelView
{
    public class SetupModelView : ModelView, IFolderChange
    {
        int? numRows { get; set; }

        string folderName { get; set; }


        public int? RowCount
        {
            get => numRows;
            set
            {
                numRows = value;
                OnPropertyChanged();
            }
        }

        public string FolderName
        {
            get => folderName;
            set
            {
                folderName = value;
                OnPropertyChanged();
            }

        }

        public bool CheckFolder
        {
            get
            {
                return System.IO.Directory.Exists(FolderName);
            }
        }

        public static void ShowFolderDialog(IFolderChange obj)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog()
            {
                SelectedPath = obj.FolderName,
                ShowNewFolderButton = true
            };

            if (dlg.ShowDialog() == DialogResult.OK)
                obj.FolderName = dlg.SelectedPath;
        }
    }
}
