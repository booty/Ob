phonenumbers.Models.Phonelist = Backbone.Model.extend({
    idAttribute:"_id",
    defaults:{
      _id:null,
      idMember:0,
      idPictureMember:0,
      login:null,
      phoneNumberUs:"0000000000",
      loginsPrevious:null,
      firstName:"John",
      lastName:"Dough",
      pictureUrl:null,
      clitterPreferencesUpdated:null
    },
    initialize: function(){
      console.log("init item model",this.id);
    }
});
