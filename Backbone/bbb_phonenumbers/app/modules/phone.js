define([
  // Application.
  "app"
],

// Map dependencies from above array.
function(app) {

  // Create a new module.
  var Phone = app.module();

  // Default model.
  Phone.Model = Backbone.Model.extend({

  });

  // Default collection.
  Phone.Collection = Backbone.Collection.extend({
    model: Phone.Model
  });

  // Return the module for AMD compliance.
  return Phone;

});
