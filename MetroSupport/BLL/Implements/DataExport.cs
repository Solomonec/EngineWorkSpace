using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MetroSupport.BLL.Interfaces;

namespace MetroSupport.BLL.Implement
{
    public class DataExport:IDataExport
    {
        
        public byte[] ExportTo(IExport export, DataTable data)
        {
            return export.Export(data);
        }
    }
}