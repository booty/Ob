'use strict';
var privatemessages = window.privatemessages || {};
var Backbone = window.Backbone || {};
var _ = window._ || {};
privatemessages.Collections.MessageCollection = Backbone.Collection.extend({
    model: privatemessages.Models.MessageModel,
    url: function () {
        var token = '5DC92AF6-D811-43EA-B3A1-E6078C70E9C5';
        //return "http://127.0.0.1:8000/assets/testData.json";
        return 'http://api.otakubooty.com/api/privatemessages?authenticationToken='+token;
    },
    parse: function(response){
        var id = 1;
        _.each(response, function(item) {
            //console.log(item);
            if(typeof(item._id) === 'undefined') {
                item._id = id;
            }
            id++;
          });
        console.log('parse',response);
        return response;
    }
});
