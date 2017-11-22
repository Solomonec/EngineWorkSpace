using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroSupport.BLL.Interfaces
{
    public interface IExport
    {
       byte[] Export(DataTable datatable);
       
    }
}
