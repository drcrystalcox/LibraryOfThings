const toolUri = 'api/tools';
const recordsUri = "api/checkoutrecords"
let tools = [];
let records=[];

function getItems() {
  fetch(toolUri)
    .then(response => response.json())
    .then(data => _displayTools(data))
    .catch(error => console.error('Unable to get items.', error));
}

function getRecords() {
  
  fetch(`${recordsUri}/open`)
    .then(response => response.json())
    .then(data => _displayRecords(data))
    .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
  const addNameTextbox = document.getElementById('add-name');
  const addDescriptionTextbox = document.getElementById('add-description');
  const addQuantityHolder = document.getElementById('add-quantity');
  const addDailyCostHolder = document.getElementById('add-daily-cost');
  const addReplacementCostHolder = document.getElementById('add-replacement-cost');


  const tool = {
    
    name: addNameTextbox.value.trim(),
    description: addDescriptionTextbox.value.trim(),
    quantityAvailable: parseInt(addQuantityHolder.value),
    dailyCost:parseInt(addDailyCostHolder.value),
    replacementCost:parseInt(addReplacementCostHolder.value)

  };

  fetch(toolUri, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(tool)
  })
    .then(response => response.json())
    .then(() => {
      getItems();
      addNameTextbox.value = '';
      addDescriptionTextbox.value = '';
      addQuantityHolder.value = '';
      addDailyCostHolder.value = '';
      addReplacementCostHolder.value=''; 
    })
    .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
  fetch(`${toolUri}/${id}`, {
    method: 'DELETE'
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to delete item.', error));
}
function displayCheckoutForm(id) {
  console.log("in checkout, id is "+id);
  var modal= document.getElementById("checkoutModal");
  var span = document.getElementsByClassName("close")[0];
  var span = document.getElementsByClassName("close")[1];

  modal.style.display="block";

  span.onclick=function(){modal.style.display="none";}

  const item = tools.find(item => item.toolId === id);
  document.getElementById('checkout-itemId').value=id;
  document.getElementById('checkout-itemName').value=item.name;
  document.getElementById('checkout-itemDailyCost').value=item.dailyCost;
  document.getElementById('checkout-agreedDailyCost').value=item.dailyCost;
  document.getElementById('checkout-dateCheckedOut').value=new Date().toDateInputValue();
  //document.getElementById('checkout-dateDue')

  

}

function displayCheckInForm(id) {
  console.log("in checkin, id is "+id);
  var modal= document.getElementById("checkinModal");
  var span = document.getElementsByClassName("close")[0];
  var span = document.getElementsByClassName("close")[1];
  modal.style.display="block";

  span.onclick=function(){modal.style.display="none";}

  const item = records.find(item => item.checkoutRecordId === id);
  document.getElementById('checkin-checkoutRecordId').value=id;
 // document.getElementById('checkin-itemName').value=item.name;
 document.getElementById('checkin-customerId').value=item.customerId;
  document.getElementById('checkin-itemDailyCost').value=item.dailyCost;
  document.getElementById('checkin-agreedDailyCost').value=item.agreedDailyCost;
  document.getElementById('checkin-dateCheckedOut').value=new Date(item.dateCheckedOut).toDateInputValue();
  document.getElementById('checkin-dateDue').value=new Date(item.dateDue).toDateInputValue();
  document.getElementById('checkin-dateReturned').value=new Date().toDateInputValue();
  document.getElementById('checkin-amountPaid').value=item.amountPaid;

  //document.getElementById('checkout-dateDue')

  //change fields to match checkin, not checkout form
  //finish  updateCheckout record function

}
Date.prototype.toDateInputValue = (function() {
  var local = new Date(this);
  local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
  return local.toJSON().slice(0,10);
});
function displayEditForm(id) {
  //console.log("in display edit form with id: "+id);
  const item = tools.find(item => item.toolId === id);
  //console.log(item);
  document.getElementById('edit-id').value=id;
  
  document.getElementById('edit-name').value = item.name;
  document.getElementById('edit-description').value = item.description;
  document.getElementById('edit-quantity').value = item.quantityAvailable;
  document.getElementById('edit-daily-cost').value = item.dailyCost;
  document.getElementById('edit-replacement-cost').value = item.replacementCost;
  document.getElementById('editForm').style.display = 'block';
}

function checkinItem() {
  const checkoutRecordId = document.getElementById('checkin-checkoutRecordId').value;
  const checkoutRecord = {

    
     dateReturned: new Date(document.getElementById("checkin-dateReturned").value),
    notes:["nothing"],
    //public IList<string> Notes {get;set;}
    amountPaid :parseInt(document.getElementById("checkin-amountPaid").value)

  };
  console.log(checkoutRecord);
  
  fetch(`${recordsUri}/${checkoutRecordId}`, {
    method: 'PUT',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(checkoutRecord)
  })
    .then(document.getElementById('checkin-result').innerText="Checkin Completed.")
    .then(() => {
      getItems();
      /*addNameTextbox.value = '';
      addDescriptionTextbox.value = '';
      addQuantityHolder.value = '';
      addDailyCostHolder.value = '';
      addReplacementCostHolder.value=''; */
      getRecords();
     // clearCheckinModal();
    })
    .catch(error => console.error('Unable to checkin item.', error));

}

function checkoutItem() {
  const checkoutRecord = {

 

    itemCheckedOutId: document.getElementById("checkout-itemId").value,
    customerId:document.getElementById("checkout-customerId").value,
    dateCheckedOut: new Date(Date.now()),
    dateDue: new Date(document.getElementById("checkout-dateDue").value),
    notes:["nothing"],
    agreedDailyCost:parseInt(document.getElementById("checkout-agreedDailyCost").value)
/*
    name: addNameTextbox.value.trim(),
    description: addDescriptionTextbox.value.trim(),
    quantityAvailable: parseInt(addQuantityHolder.value),
    dailyCost:parseInt(addDailyCostHolder.value),
    replacementCost:parseInt(addReplacementCostHolder.value)
    */

  };

  fetch(recordsUri, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(checkoutRecord)
  })
    .then(response => response.json())
    .then(() => {
      getItems();
      /*addNameTextbox.value = '';
      addDescriptionTextbox.value = '';
      addQuantityHolder.value = '';
      addDailyCostHolder.value = '';
      addReplacementCostHolder.value=''; */
      getRecords();
      clearCheckoutModal();
    })
    .catch(error => console.error('Unable to checkout item.', error));
}

function clearCheckoutModal() {
  var modal= document.getElementById("checkoutModal");
  var span = document.getElementsByClassName("close")[0];
  document.getElementById('checkout-itemId').value='';
  document.getElementById('checkout-itemName').value='';
  document.getElementById('checkout-itemDailyCost').value='';
  document.getElementById('checkout-agreedDailyCost').value='';
  document.getElementById('checkout-dateCheckedOut').value='';
  modal.style.display="none";
}
function clearCheckinModal() {
  //add code later to clear out input fields
  var modal= document.getElementById("checkinModal");
  modal.style.display="none";

}

function updateItem() {
  const itemId = document.getElementById('edit-id').value;
  const tool = {
    
    name: document.getElementById('edit-name').value.trim(),
    description: document.getElementById('edit-description').value.trim(),
    quantityAvailable:parseInt(document.getElementById('edit-quantity').value),
    dailyCost:parseInt(document.getElementById('edit-daily-cost').value),
    replacementCost:parseInt(document.getElementById('edit-replacement-cost').value)
  };

  fetch(`${toolUri}/${itemId}`, {
    method: 'PUT',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(tool)
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to update item.', error));

  closeInput();

  return false;
}

function closeInput() {
  document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
  const name = (itemCount === 1) ? 'to-do' : 'to-dos';

  document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayRecordCount(itemCount) {
  const name = (itemCount === 1) ? 'record' : 'records';

  document.getElementById('recordsCounter').innerText = `${itemCount} ${name}`;
}


function _displayTools(data) {
  const tBody = document.getElementById('tools');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {

    let id=item.toolId;
    let checkoutButton = document.createElement('button');
    checkoutButton.innerText="Check out";
    checkoutButton.setAttribute('onclick', `displayCheckoutForm("${item.toolId}")`);

    

    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm("${item.toolId}")`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    td1.appendChild(checkoutButton);

    var propertiesList = Reflect.ownKeys(item);
    //console.log(propertiesList);

    let td2 = tr.insertCell(1);
    let textNode2 = document.createTextNode(item.toolId);
    td2.appendChild(textNode2);

    let td3 = tr.insertCell(2);
    let textNode3 = document.createTextNode(item.name);
    td3.appendChild(textNode3);


    let td4 = tr.insertCell(3);
    let textNode4 = document.createTextNode(item.description);
    td4.appendChild(textNode4);

    let td5 = tr.insertCell(4);
    let textNode5 = document.createTextNode(item.quantityAvailable);
    td5.appendChild(textNode5);

    let td6 = tr.insertCell(5);
    let textNode6 = document.createTextNode(item.dailyCost);
    td6.appendChild(textNode6);

    let td7 = tr.insertCell(6);
    let textNode7 = document.createTextNode(item.replacementCost);
    td7.appendChild(textNode7);

    let td8 = tr.insertCell(7);
    td8.appendChild(editButton);

    let td9 = tr.insertCell(8);
    td9.appendChild(deleteButton);
  });

  tools = data;
}

function _displayRecords(data) {
  const tBody = document.getElementById('records');
  tBody.innerHTML = '';
//console.log(data.length);
  _displayRecordCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {

    let id=item.checkoutRecordId;
    let checkinButton = document.createElement('button');
    checkinButton.innerText="Check in";
    checkinButton.setAttribute('onclick',`displayCheckInForm("${id}")`);
                              
    

    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    td1.appendChild(checkinButton);

    let td2 = tr.insertCell(1);
    let textNode2 = document.createTextNode(item.checkoutRecordId);
    td2.appendChild(textNode2);

    let td3 = tr.insertCell(2);
    let textNode3 = document.createTextNode(item.itemCheckedOutId);
    td3.appendChild(textNode3);


    let td4 = tr.insertCell(3);
    let textNode4 = document.createTextNode(item.customerId);
    td4.appendChild(textNode4);
    //try chaining: tr.insertCell(3).appendChild(document.createTextNode(item.customerId));

    let td5 = tr.insertCell(4);
    
    let textNode5 = document.createTextNode(formatJsonDate(item.dateCheckedOut) );
    td5.appendChild(textNode5);

    let td6 = tr.insertCell(5);
    let textNode6 = document.createTextNode(item.dateDue);
    td6.appendChild(textNode6);

    let td7 = tr.insertCell(6);
    let textNode7 = document.createTextNode(item.agreedDailyCost);
    td7.appendChild(textNode7);

    //don't need to allow for editing or deleting checkout records , except adding notes
    let td8 = tr.insertCell(7);
    //td8.appendChild(editButton);
    let textNode8 = document.createTextNode(item.dateReturned);
    td8.appendChild(textNode8);

    let td9 = tr.insertCell(8);
    let textNode9 = document.createTextNode(item.amountPaid);
    td9.appendChild(textNode9);

    let td10=tr.insertCell(9);
    let textNode10=document.createTextNode(item.hasBeenReturned);
    td10.appendChild(textNode10);
  });
//on initial page load, load records array
  records = data;
}

function formatJsonDate(strdate) {
  var jsDate = new Date(strdate);
  var formattedDate=jsDate.getFullYear()+"-"+jsDate.getMonth()+"-"+jsDate.getDay();
  return formattedDate;
}
function checkedOutDateSort(a,b){
  var bDate = new Date(b.dateCheckedOut);
  var aDate = new Date(a.dateCheckedOut);
  if(aDate<bDate) return -1;
  if(aDate>bDate) return 1;
  else return 0;
}
function sortByDateCheckedOut() {
  records.sort(checkedOutDateSort);
  _displayRecords(records);
}
function nameSort(a,b){
  //used to pass to the arrays sort method
  //a and b are tool objects
  var x = a.name.toLowerCase();
  var y = b.name.toLowerCase();
  if(x<y){return -1;}
  else if(x>y){return 1;}
  else return 0;
}
function sortByName() {
  tools.sort(nameSort);
  _displayTools(tools);
}
function descriptionSort(a,b){
  //used to pass to the arrays sort method
  //a and b are tool objects
  var x = a.description.toLowerCase();
  var y = b.description.toLowerCase();
  if(x<y){return -1;}
  else if(x>y){return 1;}
  else return 0;
}
function sortByDescription() {
  tools.sort(descriptionSort);
  _displayTools(tools);
}
function idSort(a,b){
  //used to pass to the arrays sort method
  //a and b are tool objects
  var x = a.toolId.toLowerCase();
  var y = b.toolId.toLowerCase();
  if(x<y){return -1;}
  else if(x>y){return 1;}
  else return 0;
}
function sortById() {
  tools.sort(idSort);
  _displayTools(tools);
}
function dailyCostSort(a,b){
  //used to pass to the arrays sort method
  //a and b are tool objects
  var x = a.dailyCost;
  var y = b.dailyCost;
  if(x<y){return -1;}
  else if(x>y){return 1;}
  else return 0;
}
function sortByDailyCost() {
  tools.sort(dailyCostSort);
  _displayTools(tools);
}
function replacementCostSort(a,b){
  //used to pass to the arrays sort method
  //a and b are tool objects
  var x = a.replacementCost;
  var y = b.replacementCost;
  if(x<y){return -1;}
  else if(x>y){return 1;}
  else return 0;
}
function sortByReplacementCost() {
  tools.sort(replacementCostSort);
  _displayTools(tools);
}
function quantityAvailableSort(a,b){
  //used to pass to the arrays sort method
  //a and b are tool objects
  var x = a.quantityAvailable;
  var y = b.quantityAvailable;
  if(x<y){return -1;}
  else if(x>y){return 1;}
  else return 0;
}
function sortByQuantityAvailable() {
  tools.sort(quantityAvailableSort);
  _displayTools(tools);
}
function recordsIdSort(a,b){
  //used to pass to the arrays sort method
  //a and b are tool objects
  var x = a.checkoutRecordId;
  var y = b.checkoutRecordId;
  if(x<y){return -1;}
  else if(x>y){return 1;}
  else return 0;
}
function recordsSortById() {
  records.sort(recordsIdSort);
  _displayRecords(records);
}
function recordsItemCheckedOutIdSort(a,b){
  //used to pass to the arrays sort method
  //a and b are tool objects
  var x = a.itemCheckedOutId;
  var y = b.itemCheckedOutId;
  if(x<y){return -1;}
  else if(x>y){return 1;}
  else return 0;
}
function recordsSortByItemCheckedOutId() {
  records.sort(recordsItemCheckedOutIdSort);
  _displayRecords(records);
}
function recordsCustomerIdSort(a,b){
  //used to pass to the arrays sort method
  //a and b are tool objects
  var x = a.customerId;
  var y = b.customerId;
  if(x<y){return -1;}
  else if(x>y){return 1;}
  else return 0;
}
function recordsSortByCustomerId() {
  records.sort(recordsCustomerIdSort);
  _displayRecords(records);
}
