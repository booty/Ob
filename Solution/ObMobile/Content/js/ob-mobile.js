/*
To just merge the arrays (without removing duplicates) use Array.concat. To merge 
and remove dupes:

	var array1 = ["Vijendra","Singh"];
	var array2 = ["Singh", "Shakya"];
	// Merges both arrays and gets unique items
	var array3 = array1.concat(array2).unique();

This will also preserve the order of the arrays (i.e, no sorting needed).

ref: http://stackoverflow.com/questions/1584370/how-to-merge-two-arrays-in-javascript
*/

/// Merges arrays and removes duplicates. To simply merge, use Array.concat
Array.prototype.unique = function () {
	var a = this.concat();
	for (var i = 0; i < a.length; ++i) {
		for (var j = i + 1; j < a.length; ++j) {
			if (a[i] === a[j])
				a.splice(j, 1);
		}
	}
	return a;
};
