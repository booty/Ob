define([
  // Application.
  "app",
  // Libs
  "backbone",
  "lodash",
  // Views
  "modules/phone/views"
],

// Map dependencies from above array.
function(app, Backbone, _, Views) {

  // create a new module.
  var Phonelist = app.module();

  // default model.
  Phonelist.Model = Backbone.Model.extend({
    idAttribute:"_id",
    defaults:{
      _id:null,
      IdMember:0,
      IdPictureMember:0,
      Login:null,
      PhoneNumberUs:"0000000000",
      LoginsPrevious:null,
      FirstName:"John",
      LastName:"Dough",
      URL:null,
      ClitterPreferencesUpdated:null
    },
    initialize: function(){
      console.log("init item",this.id);
    }
  });

  // default collection.
  Phonelist.List = Backbone.Collection.extend({
    model: Phonelist.Model,
    token: "abc123",
    url: function(){
      //return "http://127.0.0.1:8000/assets/testData.json";
      return "http://api.otakubooty.com/api/phonenumber?token="+this.token+"&friendsonly=true";
    },
    parse: function(response){
     var id = 1;
      _.each(response, function(item){
        //console.log(item);
        if(typeof(item._id) === "undefined"){
          item._id = id;
        }
        id++;
      });
      console.log("parse",response);
      return response;
    },
    initialize: function(){
      console.log("phonelist init");

    }
  });

  Phonelist.Views = Views;

  // return the module for amd compliance.
  return Phonelist;

});
