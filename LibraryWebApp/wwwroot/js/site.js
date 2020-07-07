const toolUri = 'api/tools';
const checkoutUri = "api/checkoutrecords"
let tools = [];

function getItems() {
  fetch(toolUri)
    .then(response => response.json())
    .then(data => _displayTools(data))
    .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
  const addNameTextbox = document.getElementById('add-name');

  const item = {
    isComplete: false,
    name: addNameTextbox.value.trim()
  };

  fetch(toolUri, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
    .then(response => response.json())
    .then(() => {
      getItems();
      addNameTextbox.value = '';
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

function displayEditForm(id) {
  const item = todos.find(item => item.id === id);
  
  document.getElementById('edit-name').value = item.name;
  document.getElementById('edit-id').value = item.id;
  document.getElementById('edit-isComplete').checked = item.isComplete;
  document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
  const itemId = document.getElementById('edit-id').value;
  const item = {
    id: parseInt(itemId, 10),
    isComplete: document.getElementById('edit-isComplete').checked,
    name: document.getElementById('edit-name').value.trim()
  };

  fetch(`${toolUri}/${itemId}`, {
    method: 'PUT',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
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

function _displayTools(data) {
  const tBody = document.getElementById('tools');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {

    let id=item.toolId;
    let checkoutButton = document.createElement('button');
    checkoutButton.innerText="Checkout";
    checkoutButton.setAttribute('onclick','displayCheckoutForm(${item.id})');
    

    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    td1.appendChild(checkoutButton);

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