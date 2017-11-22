using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroSupport.Commons
{
    public static class Status
    {
        public static string StatusConvertion(int statuscode, int workstatus)
        {

            statuscode = statuscode == 5 && workstatus == 1 ? statuscode + workstatus : statuscode;

            switch (statuscode)
            {
                case 0: return "В процессе создания...";
                case 5: return "Назначена (Не в работе)";
                case 6: return "Назначена (В работе)";
                case 4: return "Выполнение отложено";
                case -1: return "Отклонена";
                case 9: return "Ожидается подтверждение ответственного";
                case 12: return "Закрыта";
                default : return "";
            }   
           
        }

        public static string TroubleReasonConvertion(int reasoncode)
        {

            switch (reasoncode)
            {
                case 1: return "Программная ошибка";
                case 2: return "Отказ оборудования";
                case 3: return "Ошибка пользователя";
                case 4: return "Другое";
                default: return "";
            }

        }
        
    }

}