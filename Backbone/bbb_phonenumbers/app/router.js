define([
  // Application.
  "app",
  // Modules
  "modules/phoneList"
],

function(app, Phonelist) {

  // Defining the application router, you can attach sub routers here.
  var Router = Backbone.Router.extend({
    routes: {
      "/": "index"
    },

    index: function() {
      console.log("index");
      var list = new Phonelist.List();
      // Create a layout and associate it with the main div
      app.useLayout("main").setViews({
        "#list": new Phonelist.Views.List({
          collection: list
        })
      });//.render();
      // Render to the DOM
      list.fetch({
          success: function(model,response) {
            console.log("success!",model,response);
            console.log("views",app);
          },
          error: function(response){
            console.log("error",response);
          }
      });
      console.log("fetching",list);
    }
  });

  return Router;

});
