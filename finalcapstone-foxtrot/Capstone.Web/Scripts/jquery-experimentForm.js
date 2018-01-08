/// <reference path="jquery-3.2.1.js" />

$(document).ready(function () {

    //Form treatment List
    $('<input type="text" name="FormTreatments" class="formTreatment textbox"/> <br/>').appendTo('#formTreatmentDiv');
    $('#btnAddFormTreatment').click(function (e) {
        e.preventDefault();
        $('<input type="text" name="FormTreatments" class="formTreatment textbox" /><br/>').appendTo('#formTreatmentDiv');
    });

    //Pre-Activation Age in weeks List
    $('<input type="text" name="PreActivationAges" class="textbox"/> <br/>').appendTo('#preActDiv');
    $('#btnAddPreActAge').click(function (e) {
        e.preventDefault();
        $('<input type="text" name="PreActivationAges" class="textbox"/> <br/>').appendTo('#preActDiv');
    });

    //Days Post Activation List
    $('<input type="text" name="DPAs" class="textbox" /> <br/>').appendTo('#dpaDiv');
    $('#btnDPA').click(function (e) {
        e.preventDefault();
        $('<input type="text" name="DPAs" class="textbox"/> <br/>').appendTo('#dpaDiv');
    });

    //Rate Dilution List
    $('<input type="text" name="RateDilutions" class="textbox"/> <br/>').appendTo('#rateDilutionDiv');
    $('#btnRateDilution').click(function (e) {
        e.preventDefault();
        $('<input type="text" name="RateDilutions" class="textbox"/> <br/>').appendTo('#rateDilutionDiv');
    });

    //Age of Beads List
    $('<input type="text" name="BeadAge" class="textbox"/> <br/>').appendTo('#ageOfBeadsDiv');
    $('#btnAgeOfBeads').click(function (e) {
        e.preventDefault();
        $('<input type="text" name="BeadAge"  class="textbox"/> <br/>').appendTo('#ageOfBeadsDiv');
    });

    //Chemicals List
    $('<input type="text" name="Chemicals" class="textbox"/> <br/>').appendTo('#chemicalsDiv');
    $('#btnChemicals').click(function (e) {
        e.preventDefault();
        $('<input type="text" name="Chemicals" class="textbox" /> <br/>').appendTo('#chemicalsDiv');
    });

    //highlight textbox that is clicked on
    $('input[type="text"]').focus(function () {
        $(this).addClass("focus");
    });

    $('input[type="text"]').blur(function () {
        $(this).removeClass("focus");
    });

    //change bool value when a data cell is updated
    $('input[type="text"]').change(function () {
        $(this).parent().parent('tr').children('td.cellUpdate').children().val(true);
    });

    //disable return key as submission of form in data entry view
    $('#updateform').on('keyup keypress', function (e) {
        var keyCode = e.keyCode || e.which;
        if (keyCode === 13) {
            e.preventDefault();
            return false;
        }
    });
  
});



