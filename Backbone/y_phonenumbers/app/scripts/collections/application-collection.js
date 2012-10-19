phonenumbers.Collections.Phonelist = Backbone.Collection.extend({
    model: phonenumbers.Models.Phonelist,
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
