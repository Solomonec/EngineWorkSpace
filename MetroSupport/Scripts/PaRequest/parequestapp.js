/**
 * Created by S.Kuzmik on 07.07.2016.
 */
$(document).ready(function() {
    initializeTabs();
    initializeControls();
    initializeDatepickers();
   
});

function initializeControls() {

    $(".button-cancel").click(function (e) {
        $.modal.close();
    });

}

function initializeTabs()
{
     $("#content").find("[id^='tab']").hide(); 
    $("#tabs li:first").attr("id","current"); 
    $("#content #tab1").fadeIn(); 
    
    $('#tabs a').click(function(e) {
        e.preventDefault();
        if ($(this).closest("li").attr("id") == "current"){ 
         return;       
        }
        else{             
          $("#content").find("[id^='tab']").hide();
          $("#tabs li").attr("id",""); 
          $(this).parent().attr("id","current"); 
          $('#' + $(this).attr('name')).fadeIn(); 
        }
    });
}

function initializeDatepickers() {


    $.datetimepicker.setLocale('ru');


    $('#startdatepicker').datetimepicker({
        timepicker:false,
        format:'d.m.Y'
    });

    $('#starttimepicker').datetimepicker({

        datepicker:false,
        format:'H:i'
    });

    $('#enddatepicker').datetimepicker({

        timepicker:false,
        format:'d.m.Y'
    });

    $('#endtimepicker').datetimepicker({

        datepicker:false,
        format:'H:i'
    });
	
	$(".request-work-context-time").change(
        function () {
          if($('#startdatepicker').val() !== "" && $('#starttimepicker').val() !== "" &&  $('#enddatepicker').val() !== "" && $('#endtimepicker').val() !== "" ){
            var wc = new WorkCalculation($('#startdatepicker').datetimepicker('getValue'),$('#enddatepicker').datetimepicker('getValue'),
                                         $('#starttimepicker').datetimepicker('getValue'),  $('#endtimepicker').datetimepicker('getValue'));
              var totaldays = wc.totalDays(wc.total());
              $("#totaldays").val(totaldays);
              var totalhours = wc.totalHours(totaldays,wc.total());
              $("#totalhours").val(totalhours);
              var totalminutes = wc.totalMinutes(totalhours);
			  $("#totalminutes").val(totalminutes);
		  }
        }
    );

    $('#startdatetimenow').click(function () {

            var datetime = new Date();
            $('#startdatepicker').val((formatDateTimeValue(datetime.getDay()) + "." + formatMonthValue(datetime.getMonth()) + "." + formatDateTimeValue(datetime.getFullYear())).toString());
            $('#starttimepicker').val((formatDateTimeValue(datetime.getHours()) + ":" + formatDateTimeValue(datetime.getMinutes())).toString())
        }
    );


    $('#enddatetimenow').click(function () {

            var datetime = new Date();
            $('#enddatepicker').val((formatDateTimeValue(datetime.getDay()) + "." + formatMonthValue(datetime.getMonth()) + "." + formatDateTimeValue(datetime.getFullYear())).toString());
            $('#endtimepicker').val((formatDateTimeValue(datetime.getHours()) + ":" + formatDateTimeValue(datetime.getMinutes())).toString());
        }
    );

    function formatDateTimeValue(value) {
        var intval = parseInt(value, 10);
        if (intval <= 9)
            return '0' + intval.toString();
        else
            return intval.toString();
    }

    function formatMonthValue(value) {
        var intval = parseInt(value, 10);
        if (intval <= 9)
            return '0' + (intval + 1).toString();
        else
            return (intval + 1).toString();

    }
}

function WorkCalculation(startDate,endDate,startTime,endTime) {
    this.startDate = startDate;
    this.endDate  = endDate;
    this.startTime = startTime;
    this.endTime  = endTime;

    this.total = function () {

        this.startDate = startDate.setHours(this.startTime.getHours(), this.startTime.getMinutes());
        this.endDate = endDate.setHours(this.endTime.getHours(), this.endTime.getMinutes());
        return  ((this.endDate - this.startDate) / 60000);
        
    };

    this.totalHours = function (days, total) {

        var hours = 0;
        if(days > 0) hours = Math.floor((total - (1440*days))/60);  else hours = Math.floor(total/60);
       
        return hours;

    };

    this.totalMinutes = function (hours) {
        var min = 0;
        if (hours >= 0)
            if (this.startTime.getMinutes() > this.endTime.getMinutes())
                min = ((this.endTime.getMinutes() + 60) - this.startTime.getMinutes());
            else
                min = (this.endTime.getMinutes() - this.startTime.getMinutes());
        return min;
    };

    this.totalDays = function (total) {
        var days = 0;
        if (total < 0) days = days * (-1);
        if (total > 1440) days = Math.floor(total / 1440); else days = 0;
        return days;
    }
    this.totalResult = function () {

        return this.totalDays().toString() + " дн. " + this.totalHours().toString() + " ч. " + this.totalMinutes().toString() +" мин. " ;
    }

}