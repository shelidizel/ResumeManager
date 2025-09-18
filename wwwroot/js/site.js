// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});

function hasClass(elem, className) {
    return elem.classList.contains(className);
}

function CalcTotalExperiences() {
    var x = document.getElementsByClassName('YearsWorked');
    var totalExp = 0;
    var i;
    for (i = 0; i < x.length; i++) {
        if (x[i].closest("tr").style.display == "none") {
            totalExp = totalExp - eval(x[i].value);
        } 
            totalExp = totalExp + eval(x[i].value);
        
        
    }
    document.getElementById('TotalExperience').value = totalExp;
    return;
}



document.addEventListener('change', function (e) {
    
    if (hasClass(e.target, 'YearsWorked')) {
        
        CalcTotalExperiences();
    }
}, false);

function DeleteItem(btn) {
    if (btn.id.indexOf('Soft') > 0)
        var table = document.getElementById('SoftwareExpTable');
    else
        var table = document.getElementById('ExpTable');
    var rows = table.getElementsByTagName('tr');
    if (rows.length == 2) {
        alert("This Row Cannot Be Deleted");
        return;
    }

    var btnIdx = btn.id.replaceAll('btnremove-', '');
    //var idOfIsDeleted = btnIdx + "__IsDeleted";

    if (btn.id.indexOf("Soft") > 0)
        btnIdx = btn.id.replaceAll('btnremoveSoft-', '');

    var idOfIsDeleted = btnIdx + "__IsDeleted";

    if (btn.id.indexOf("Soft") > 0)
        idOfIsDeleted = btnIdx + "__IsHidden";
    var hidIsDelId = document.querySelector("[id$='" + idOfIsDeleted + "']").id;
    document.getElementById(hidIsDelId).value = "true";
    $(btn).closest('tr').hide();
    CalcTotalExperiences();
}

function AddItem(btn) {

    if (btn.id == 'btnaddSoftware')
        var table = document.getElementById('SoftwareExpTable');
    else
        var table = document.getElementById('ExpTable');
    var rows = table.getElementsByTagName('tr');
    var lastrowIdx = rows.length - 1;

    var rowOuterHtml = rows[lastrowIdx].outerHTML;

    lastrowIdx = lastrowIdx - 1;
    var nextrowIdx = eval(lastrowIdx) + 1;

    console.log('Last Row Idx = ' + lastrowIdx);
    console.log('Next Row Idx = ' + nextrowIdx);

    rowOuterHtml = rowOuterHtml.replaceAll(' ' + lastrowIdx + ' ', ' ' + nextrowIdx + ' ');
    rowOuterHtml = rowOuterHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowOuterHtml = rowOuterHtml.replaceAll(' ' + lastrowIdx, ' ' + nextrowIdx);

    var newRow = table.insertRow();
    newRow.innerHTML = rowOuterHtml;

    rebindvalidators();
}

function rebindvalidators() {
    var $form = $("#ApplicantForm");
    $form.unbind();
    $form.data("validator", null);
    $.validator.unobtrusive.parse($form);
    $form.validate($form.data("unobtrusiveValidation").options);
}
