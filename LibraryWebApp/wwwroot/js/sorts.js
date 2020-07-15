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
  