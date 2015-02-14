
$(function () {

    getProducts();

    $("#amount").autoNumeric('init', { mDec: 0 });
    $("#price").autoNumeric('init');

    $("#add").click(addProduct);

    $("#submit").click(submit);

    $("#tableCart").on('click', ".btn-delete", deleteProduct);

});

function toggleTable() {
    if ($(".btn-delete").length === 0) {
        $('#divTable').hide();
    } else {
        $("#divTable").show();
    }
}

function addProduct() {
    var isImported = 'No';
    if ($("#isImported").prop('checked') == true) {
        isImported = 'Yes';
    }
    var td = '<td>' + $("#product option:selected").text() + '</td>'
        + '<td class="p-amount">' + $("#amount").val() + '</td>'
        + '<td class="p-price">' + $("#price").val() + '</td>'
        + '<td class="p-imported">' + isImported + '</td>'
        + '<td style="text-align: center; width: 110px;">' +
        '<a class="btn btn-primary btn-xs btn-delete" >Delete</a></td>';
    $("#tableCart tbody").append("<tr data-id=" + $("#product").val() + ">" + td + "</tr>");

    toggleTable();
    clearReceipt();
    return false;
}
function deleteProduct() {
    $(this).closest("tr").remove();
    clearReceipt();
    toggleTable();
}

function getProducts() {
    $.get("http://localhost:38001/home/GetAllProducts").success(function (data) {
        var da = data;
        $.each(data, function (index, p) {
            $("#product").append("<option value=" + p.Id + ">" + p.Name + "</option>");
        });
    });
}

function submit() {
    clearReceipt();
    var data = getItems();
    if (data.length === 0) {
        return false;
    }
    $.ajax({
        type: "POST",
        url: "http://localhost:38001/home/printreceipt",
        data: JSON.stringify(data),
        contentType: "application/json"
    }).success(printReceipt);

    return false;
}

function getItems() {
    var data = [];
    $('#tableCart tr').each(function () {
        var tr = $(this);
        if (tr.data('id')) {
            var item = {
                Id: tr.data('id'),
                Amount: tr.find("td.p-amount").text(),
                Price: tr.find("td.p-price").text(),
                IsImported: tr.find("td.p-imported").text() === "Yes"
            };
            data.push(item);
        }
    });
    return data;
}

function printReceipt(result) {
    var divReceipt = $("#divReceipt");
    divReceipt.append("<h3>Receipt</h3>");
    $.each(result.Products, function (key, value) {
        var isImported = "";
        if (value.IsImported === true) {
            isImported = "Imported ";
        }
        var item = "<h4>" + value.Amount + '  ' + isImported + value.Name + ':  ' + value.PrintPrice + "</h4>";
        divReceipt.append(item);

    });
    divReceipt.append("<h4>Sales Taxes: " + result.SalesTaxes + "</h4>");
    divReceipt.append("<h4>Total: " + result.Total + "</h4>");
    divReceipt.show();

}


function clearReceipt() {
    $("#divReceipt").empty();
    $("#divReceipt").hide();
}