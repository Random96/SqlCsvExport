using System;
using System.Linq;

namespace SqlCsvExport.ModelView
{
    /// <summary>
    /// Инерфейс поддержки выбора директории
    /// </summary>
    public interface IFolderChange
    {
        string FolderName { get; set; }
    }
}
