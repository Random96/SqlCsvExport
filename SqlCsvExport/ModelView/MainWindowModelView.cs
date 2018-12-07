using SqlCsvExport.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlCsvExport.ModelView
{
    public class MainWindowModelView : ModelView
    {
        private NorthWind context = new NorthWind();
        private int? rowCount;
        private string folderName;

        public bool Connected { get; set; }

        public bool CanExeCute => Connected;

        /// <summary>
        /// подключение к БД
        /// </summary>
        internal void Connect()
        {
            Connected = true;

            OnPropertyChanged();
            OnPropertyChanged(nameof(CanExeCute));
        }

        /// <summary>
        /// Максимальное количество строк в выборке
        /// </summary>
        internal int? RowCount
        {
            get => rowCount;
            set
            {
                rowCount = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Папка экспорта
        /// </summary>
        internal string FolderName
        {
            get => folderName;
            set
            {
                folderName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// процедура экспорта запроса
        /// </summary>
        internal void Export()
        {
            var Wnd = new Forms.ExportForm();

            if (Wnd.DataContext is ExportModelView mv)
            {
                mv.RowCount = RowCount;
                mv.FolderName = FolderName;

                Wnd.ShowDialog();

                if (Wnd.DialogResult ?? false)
                {
                    FolderName = mv.FolderName;
                }
            }

        }

        /// <summary>
        /// Вызов процедуры установки
        /// 1) количесво строк
        /// 2) папка экспорта
        /// </summary>
        internal void Setup()
        {
            var Wnd = new Forms.SetupForm();

            if (Wnd.DataContext is SetupModelView mv)
            {
                mv.RowCount = RowCount;
                mv.FolderName = FolderName;

                Wnd.ShowDialog();
                    
                if (Wnd.DialogResult ?? false)
                {
                    RowCount = mv.RowCount;
                    FolderName = mv.FolderName;
                }
            }
        }
    }
}
