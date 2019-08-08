//Functions to enable ingredient table manipulation for add and edit views

//Event Handlers
$('.table').on('click', '.plus', function addRow(evt) {
    //HTML of the new table row
    var newRow = `
        <tr>
            <td><button type="button" class="btn btn-default plus">+</button></td>
            <td><button type="button" class="btn btn-default minus">-</button></td>
            <td><input type="text" class="form-control" /></td>
        </tr>`;
    //get the clicked element and get the entire row DOM
    var row = $(evt.target).parents('tr');
    //Add the newRow below the selected row
    row.after(newRow);
});

$('.table').on('click', '.minus', function removeRow(evt) {
    //Get the minus button clicked and grab the entire row DOM
    var row = $(evt.target).parents('tr');
    //Removes the row only if there is more than one row so the entire table is not deleted
    if (row.siblings().length > 1) {
        row.remove();
    }
});
