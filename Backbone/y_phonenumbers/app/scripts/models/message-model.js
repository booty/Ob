'use strict';
var privatemessages = window.privatemessages || {};
var Backbone = window.Backbone || {};
privatemessages.Models.MessageModel = Backbone.Model.extend({
    idAttribute: '_id',
    defaults:{
        _id:null,
        idMessage: 0,
        idMemberSender: 0,
        idMemberRecipient: 0,
        idMessageReplyTo: 0,
        readCount: 0,
        created: null,
        read: null,
        validationCode: null,
        deletedBySender: null,
        deletedByRecipient: null,
        subject: 'subject',
        body: 'body'
    },
    initialize: function() {
      console.log('init item model',this.id);
    }
});
