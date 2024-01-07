// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
async function fetchData(url)
{
    const response = await fetch(url).then(res => res.json(), err => alert("error"));
    return (response);
}

async function postData(url, body, onSuccess = res => res.json(), onError = (e) => { alert(e) })
{
    $.ajax({
        type: 'POST',
        url: url,
        data: typeof body == 'string' ? body : JSON.stringify(body), // or JSON.stringify ({name: 'jonas'}),
        success: onSuccess,
        error: onError,
        contentType: "application/json",
        dataType: 'json'
    });
}
async function DeleteData(url, onSuccess = res => res.json(), onError = (e) => { alert(e) })
{
    if (confirm("CONFIRM DELETE ?")) {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: onSuccess,
            error: onError,
            contentType: "application/json",
            dataType: 'json'
        });
    }


}
async function PutData(url, body, onSuccess = res => res.json(), onError = (e) => { alert(e) })
{
    $.ajax({
        type: 'PUT',
        url: url,
        success: onSuccess,
        data: typeof body == 'string' ? body : JSON.stringify(body), 
        error: onError,
        contentType: "application/json",
        dataType: 'json'
    });
}
var Site = Site || {};

Site.populateDropdown = function (elementId, data) {
    const element = $("#" + elementId);
    var options = "";

    data.forEach(item => {
        options += `<option value="${item.id}">${item.name}</option>`;
    });

    element.html(options);
};
function populateList(data, moduleName) {
    const tbody = $('#tableBody');
    let htmlstr = "";

    data = typeof data == 'string' ? JSON.parse(data) : data;

    data.forEach(item => {
       // let dataName, dataDetails;

      var  row 
        switch (moduleName) {
            case 'Customers':
                row = `<tr>
                                <td>${item.name}</td>
                                <td>${item.email}</td>
                                <td>
                                        <a onclick="openModal(${item.id})">Edit</a>
                                </td>
                                <td>

                                        <a onclick="deleteCustomer(${item.id})">Delete</a>
                                       
                                </td>
                            </tr>`;
                break;
            case 'Products':
               row = `<tr>
                        <td>${item.name}</td>
                        <td>${item.price}</td>
                        <td>
                                <a onclick="openModal(${item.id})">Edit</a>
                        </td>
                                 <td>
                                                <a onclick="deleteProduct(${item.id})">Delete</a>
                                </td>
                    </tr>`;
                break;
            case 'Sales':

               row = `<tr>
                                    <td>${item.customerName}</td>
                                     <td>${item.productName}</td>
                                     <td>${item.quantity}</td>
                                     <td>${item.totalPrice}</td>
                                     <td>
                                             <a onclick="openModal(${item.id})">Edit</a>
                                     </td>
                                     <td>

                                             <a onclick="deleteSale(${item.id})">Delete</a>

                                     </td>
                                 </tr>`;
                break;
            
        }

       

        htmlstr += row;
    });

    tbody.html(htmlstr);
}



