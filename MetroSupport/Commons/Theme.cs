using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroSupport.Commons
{
    public static class Theme
    {
        public static string ThemePrimaryDefault(string theme)
        {
            theme = theme ?? "Создание заявки";
            return theme;
        }
    }
}