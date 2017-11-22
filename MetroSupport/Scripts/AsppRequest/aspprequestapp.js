
$(document).ready(function () {
    initializeTabs();
    initializeControls()
	initializeDatepickers();
	
    
})

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
        format:'d.m.Y',
    });

    $('#starttimepicker').datetimepicker({

        datepicker:false,
        format:'H:i',
    });

    $('#enddatepicker').datetimepicker({

        timepicker:false,
        format:'d.m.Y',
    });

    $('#endtimepicker').datetimepicker({

        datepicker:false,
        format:'H:i',
    });
	
	$(".request-work-context-time").change(
        function () {
          if($('#startdatepicker').val() != "" && $('#starttimepicker').val() != "" &&  $('#enddatepicker').val() != "" && $('#endtimepicker').val() != "" ){
            var wc = new WorkCalculation($('#startdatepicker').datetimepicker('getValue'),$('#enddatepicker').datetimepicker('getValue'),
                                         $('#starttimepicker').datetimepicker('getValue'),  $('#endtimepicker').datetimepicker('getValue'));
            $("#totaldays").val(wc.totalDays());
			$("#totalhours").val(wc.totalHours());
			$("#totalminutes").val(wc.totalMinutes());
		  }
        }
    );
}

function WorkCalculation(startDate,endDate,startTime,endTime) {
    this.startDate = startDate;
    this.endDate  = endDate;
    this.startTime = startTime;
    this.endTime  = endTime;
    this.totalHours = function() {

        var hours = (this.endTime.getHours() - this.startTime.getHours());
        if(hours < 0) hours = hours*(-1);
        return hours

    };

    this.totalMinutes = function (){
        var minutes = (this.endTime.getMinutes() - this.startTime.getMinutes());
        if(minutes < 0) minutes = minutes*(-1);
        return minutes
    };

    this.totalDays = function() {
        var days = ((((this.endDate - this.startDate) / 60000) / 60) / 24);
        if(days < 0) days = days*(-1);
        return Math.ceil(days);
    }
    this.totalResult = function () {

        return this.totalDays().toString() + " дн. " + this.totalHours().toString() + " ч. " + this.totalMinutes().toString() +" мин. " ;
    }

}