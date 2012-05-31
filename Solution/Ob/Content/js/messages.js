// todo: update to jQuery
function checkedAll (id, checked, saveButtonName, deleteButtonName, removeButtonName) {
	var el = document.getElementById(id);
	for (var i = 0; i < el.elements.length; i++) {
		el.elements[i].checked = checked;
	}   
	
	messageClickHandler(id, saveButtonName, deleteButtonName, removeButtonName);
} 	

		
// todo: update to jQuery
function messageClickHandler (id, saveButtonName, deleteButtonName, removeButtonName) { 
	var enable = (numCheckedMessages(id) > 0);
	var saveButton = 	document.getElementById(saveButtonName)
	var deleteButton = document.getElementById(deleteButtonName)
	var removeButton = document.getElementById(removeButtonName)
	
	//enable/disable form properties
	if (saveButton != null) saveButton.disabled=(!enable);
	if (deleteButton != null) deleteButton.disabled=(!enable);
	if (removeButton != null) removeButton.disabled=(!enable);
	
	//swap images
	if (enable==true) {
		if (saveButton != null) saveButton.src='assets/i/messages/save_on.gif';
		if (deleteButton != null) deleteButton.src='images/messages/delete_on.gif';
		if (removeButton != null) removeButton.src='images/messages/remove_on.gif';
	}
	else  {
		if (saveButton != null) saveButton.src='assets/i/messages/save_off.gif';
		if (deleteButton != null) deleteButton.src='assets/i/messages/delete_off.gif';
		if (removeButton != null) removeButton.src='assets/i/messages/remove_off.gif';
	}			
}

// todo: update to jQuery
function numCheckedMessages  (id) {
	var i=0;
	var numChecked=0;
	var debug;
	var el = document.getElementById(id);
	for (var i = 0; i < el.elements.length; i++) {
		debug=(debug + el.elements[i].name + '=' + el.elements[i].checked + '\n');
		if (el.elements[i].checked) numChecked++;
	}   
	return numChecked;	
} 	



