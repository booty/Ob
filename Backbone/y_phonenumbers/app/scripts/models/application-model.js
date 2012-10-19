phonenumbers.Models.Phonelist = Backbone.Model.extend({
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
      console.log("init item model",this.id);
    }
});
