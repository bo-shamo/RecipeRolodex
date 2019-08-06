//Functions to enable ingredient table manipulation for add and edit views

//Functions
//Add row
function addRow(event) {
    //new row object
    //let newRow = '<tr> < td > <button type="button" class="btn btn-default plus">+</button></td><td><button type="button" class="btn btn-default minus">-</button></td><td><input type="text" class="form-control" /></td></tr>';
    console.log('plus');
}

//Remove Row
function removeRow(evt) {

}

//Check if this is the last row and returns true if safe to remove
function canRemove(evt) {

}


//$('.table .plus').on('click', addRow(event));
//Event Handlers
//$('.table').on('click', $('.plus'), addRow());
var plusButton = document.getElementsByClassName('plus');
plusButton.addEventListener("click",addRow());
console.log(plusButton);

//$('.table').on('click', $('.minus'), removeRow(event));