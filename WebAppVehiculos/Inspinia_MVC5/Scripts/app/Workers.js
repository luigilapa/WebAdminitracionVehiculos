$(function () {
    $('.edit-mode').hide();
    $('.edit-user, .cancel-user').on('click', function () {
        var tr = $(this).parents('tr:first');
        tr.find('.edit-mode, .display-mode').toggle();
    });

    $('.save-user').on('click', function () {
        var tr = $(this).parents('tr:first');

        var Name = tr.find("#Name").val();
        var Position = tr.find("#Position").val();
        var Office = tr.find("#Office").val();
        var Extn = tr.find("#Extn").val();
        var Salary = tr.find("#Salary").val();
        var Email = tr.find("#Email").val();
        var DateRegister = tr.find("#DateRegister").val();
 

        var ID = tr.find("#ID").html();

        tr.find("#lblName").text(Name);
        tr.find("#lblPosition").text(Position);
        tr.find("#lblOffice").text(Office);
        tr.find("#lblExtn").text(Extn);
        tr.find("#lblSalary").text(Salary);
        tr.find("#lblEmail").text(Email);
        tr.find("#lblDateRegister").text(DateRegister);


        tr.find('.edit-mode, .display-mode').toggle();

        var WorkerModel =
        {
            "ID": ID,
            "Name": Name,
            "Position": Position,
            "Office": Office,
            "Extn": Extn,
            "Salary": Salary,
            "Email": Email,
            "DateRegister": DateRegister

        };
        $.ajax({
            url: '/Scaffolding/Edit/',
            data: JSON.stringify(WorkerModel),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                alert(data);
            }
        });

    });
})