function formatComment(callrequests, prop) {
    var comment = callrequests[prop];
    if (comment !== null)
        return comment.substring(0, 40) + "...";
    else {
        return comment;
    }
};

function formatStatus(callrequests, prop) {
   var status = callrequests["RequestStatus"];
   
    switch (status) {
        case "Закрыта": return "<div style='color: green; text-width:bold;'>" + status + "</div>";
        case "Назначена (В работе)": return "<div style='color: blue; text-width:bold;'>" + status + "</div>";
        case "Назначена (Не в работе)": return "<div style='color: blue; text-width:bold;'>" + status + "</div>";
        case "Выполнение отложено": return "<div style='color: cornflowerblue; text-width:bold;'>" + status + "</div>";
        case "Отклонена": return "<div style='color: red; text-width:bold;'>" + status + "</div>";
        case "Ожидается подтверждение ответственного": return "<div style='color: orange; text-width:bold;'>" + status + "</div>";
        default:
            return status;
    }
}