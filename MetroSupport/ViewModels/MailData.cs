using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.Models;

namespace MetroSupport.ViewModels
{
    public class MailData
    {
      public Guid CallRequestId { get; set; }
      public string CallRequestNumber { get; set; }
      public UserProfile Assigner { get; set; }
      public UserProfile Boss { get; set; }
      public string CallRequestTheme { get; set; }
      public string Department { get; set; }

    }
}