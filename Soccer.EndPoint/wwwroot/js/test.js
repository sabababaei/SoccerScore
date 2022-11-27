$("#btnAddPlayers").on("click" , function (){
    var txtPlayerNameValue = $("#Player").find('option:selected').val();
    var txtPlayerName=$("#Player").find('option:selected').text();

    $("#tbl_players").append('<tr><td>'+txtPlayerName+'</td>'+
        '<td><a class="btn btn-danger remove">Delete</a><input id="PlayerId" type="hidden" value="'+txtPlayerNameValue+'"/>'+'</td></tr>');

});

$("#tbl_players").on("click" ,".remove", function ()
{
    $(this).closest('tr').remove();
});

function SendDataToServer(e)
{
    debugger;
    e.preventDefault();
    var form= $("#createForm");
    form.validate();

    let formData= new FormData();
    formData.append("TeamName" ,$("#TeamName").val());
    var Players= new Array();
    $("#tbl_players tr:gt(0)").each(function ()
    {
        var row=$(this);
        var player={};
        player.PlayerName=row.find("TD").eq(0).html();
        Players.push(player);
    });
    for (var i=0;i<Players.length;i++)
    {
        formData.append("Players["+i+"].PlayerName" ,Players[i].PlayerName);
    }



    /* var PlayerList= $("#tbl_players tr:gt(0)").map( function ()
     {
         return {
             PlayerName:$(this.cells[0]).text()
         }
     }).get();
     $.each(PlayerList ,function (i ,val)
     {
         formData.append("Players["+i+"].PlayerName" ,val);
     });*/

    var ajaxRequest = $.ajax(
        {
            type:"POST",
            url:"/Team/New/",
            contentType:false,
            processData: false,
            data:formData,
            success:function (data){
                if (data.isSuccess)
                {
                    window.location.href="/Index";
                }
                else
                {

                }

            } ,
            error: function(xhr , exception){
                alert(xhr.status);
            }
        }
    );
}