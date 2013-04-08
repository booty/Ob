if($.cookie('AuthenticationToken')){
    console.log('cookie exists');
    GOMOFO();
} else {
    $('body').append('<div><form id="login"><h1>login</h1><input type="text" name="username" id="username"></input><br><input type="password" name="password" id="password"></input><input type="submit" value="submit"></form></div>');
    $('#login').on('submit', function(){
        login();
        return false;
    });
}
function login(){
    var $username = $('#username').val();
    var $password = $('#password').val();
    console.log('submitting login');
    $.ajax({
        async: false,
        data: {
            login: $username,
            password: $password
        },
        dataType: 'json',
        type: 'POST',
        url: 'http://api.otakubooty.com/api/authenticationTokens',
        error: function (e) { console.log('error', this);}
    }).then(function(result){
        console.log('result:',result);
        $.cookie('AuthenticationToken', result.authenticationToken);
        GOMOFO();
    });
}
function GOMOFO(){

    $.ajaxSetup({
        beforeSend: function(xhr) {
            //xhr.withCredentials = true;
            console.log(xhr);
            console.log('aaaaaaahhhghgh');
            //xhr.setRequestHeader('X-CSRF-Token', $('meta[name="csrf-token"]').attr('content'));
            xhr.setRequestHeader('X-ObAuthenticationToken', $.cookie('AuthenticationToken'));
        },
        /*
        xhrFields: {
            withCredentials: true
        },*/
        dataType: 'json',
        async: false
    });
App = Ember.Application.create();

App.Store = DS.Store.extend({
    revision: 12,
    adapter: 'DS.RESTAdapter'//'DS.FixtureAdapter'
});
DS.RESTAdapter.reopen({
    url: 'http://api.otakubooty.com/api'
});
App.Privatemessage = DS.Model.extend({
    id: function () {
        return this.get( 'idMessage');
    }.property('idMessage'),
    subject: DS.attr('string'),
    body: DS.attr('string')
});
App.Phonenumber = DS.Model.extend({
    id: function () {
        return this.get( 'idMember' );
    }.property( 'idMember' ),
    idMember: DS.attr('string'),
    firstName: DS.attr('string'),
    lastName: DS.attr('string'),
    fullName: function () {
        return this.get( 'firstName' ) + " " + this.get( 'lastName' );
    }.property( 'firstName', 'lastName' ),
    login: DS.attr('string'),
    phoneNumber: function () {
        var raw = this.get('phoneNumberUs');
        return raw.substr(0,3)+'-'+raw.substr(3,3)+'-'+raw.substr(6);
    }.property( 'phoneNumber' ),
    phoneNumberUs:  DS.attr('number')
});

App.Forum = DS.Model.extend({
    id: function () {
        return this.get( 'idForum' );
    }.property( 'idForum' ),
    title: DS.attr('string')
})

App.Router.map(function() {
  // put your routes here
  this.resource( 'privatemessages');
  //this.resource( 'privatemessage', { path: '/privatemessage/:privatemessage_id'});
  this.resource( 'phonenumbers' );
  //this.resource( 'phonenumber', { path: '/phonenumber/:phonenumber_id'});
  this.resource( 'forums' );
});

App.PrivatemessagesRoute = Ember.Route.extend({
    model: function () {
        console.log('pm route');
        return App.Privatemessage.find();
    }
});
App.PrivatemessageRoute = Ember.Route.extend({
    model: function ( params ) {
        return App.Privatemessage.find( params.privatemessage_id );
    }
});

App.PhonenumbersRoute = Ember.Route.extend({
    model: function() {
        console.log('pn route');
        return App.Phonenumber.find();
    }
});
App.PhonenumberRoute = Ember.Route.extend({
    model: function( params ) {
        return App.Phonenumber.find( params.phonenumber_id );
    }
});

App.ForumsRoute = Ember.Route.extend({
    model: function() {
        return App.Forum.find();
    }
})

   // });
};
/*
App.Phonenumber.FIXTURES = [
  {
    "idMember": 13784,
    "idPictureMember": 118512,
    "login": "(s)Aint Chimo",
    "phoneNumberUs": "6263729297",
    "loginsPrevious": "(s)Aint She Sweet, (S) Class Mercades, (s)weet Sister Merci",
    "firstName": "Mercades",
    "lastName": "Victoria",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/13784_50.jpg",
    "clitterPreferencesUpdated": "2010-06-18T00:16:28.18"
  },
  {
    "idMember": 5236,
    "idPictureMember": 104386,
    "login": "1-Up",
    "phoneNumberUs": "4342498349",
    "loginsPrevious": "1-Up Pimp",
    "firstName": "Brandon",
    "lastName": "Nelson",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/52/5236_50.jpg",
    "clitterPreferencesUpdated": "2011-09-03T12:34:06.81"
  },
  {
    "idMember": 3532,
    "idPictureMember": 118060,
    "login": "Ak",
    "phoneNumberUs": "3023673498",
    "loginsPrevious": "Akinola, OB Blk Ppl Corps, Yellow Submarine, OJ Simpson",
    "firstName": "Akinola",
    "lastName": "Verissimo",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/35/3532_50.jpg",
    "clitterPreferencesUpdated": "2011-01-14T02:20:27.42"
  },
  {
    "idMember": 2584,
    "idPictureMember": 116310,
    "login": "Allison",
    "phoneNumberUs": "6785239099",
    "loginsPrevious": null,
    "firstName": "Allison",
    "lastName": "Warren",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/25/2584_50.jpg",
    "clitterPreferencesUpdated": "2009-06-03T14:58:41.53"
  },
  {
    "idMember": 9125,
    "idPictureMember": 114695,
    "login": "Amanda",
    "phoneNumberUs": "3032415950",
    "loginsPrevious": "SEXNEED, Yoko Onno, Yoko Ono",
    "firstName": "Amanda",
    "lastName": "Walton",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/91/9125_50.jpg",
    "clitterPreferencesUpdated": "2011-01-13T18:08:25.613"
  },
  {
    "idMember": 6340,
    "idPictureMember": 117971,
    "login": "Anal Rape Tsunami",
    "phoneNumberUs": "8189848347",
    "loginsPrevious": "Reilly, Pool Shark, B======D",
    "firstName": "Reilly",
    "lastName": "Campbell",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/63/6340_50.jpg",
    "clitterPreferencesUpdated": "2009-06-03T12:56:00.187"
  },
  {
    "idMember": 7042,
    "idPictureMember": 116581,
    "login": "Andrew",
    "phoneNumberUs": "2108678760",
    "loginsPrevious": "Mean Mister Mustard, Andew, Luchadork, MODDY LEE",
    "firstName": "Andrew",
    "lastName": "Knapik",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/70/7042_50.jpg",
    "clitterPreferencesUpdated": "2010-06-15T13:31:55.467"
  },
  {
    "idMember": 3282,
    "idPictureMember": 114823,
    "login": "ANIMAL",
    "phoneNumberUs": "2036444827",
    "loginsPrevious": "RonnyBojangles, 88MPH, __",
    "firstName": "juan",
    "lastName": "don",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/32/3282_50.jpg",
    "clitterPreferencesUpdated": "2011-12-04T22:02:07.547"
  },
  {
    "idMember": 1972,
    "idPictureMember": 96468,
    "login": "Anti",
    "phoneNumberUs": "5629658220",
    "loginsPrevious": "Imagine",
    "firstName": "Eddie",
    "lastName": "Smith",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/19/1972_50.jpg",
    "clitterPreferencesUpdated": "2010-06-17T09:21:55.277"
  },
  {
    "idMember": 22046,
    "idPictureMember": 106590,
    "login": "Arden FIERCE",
    "phoneNumberUs": "2155899867",
    "loginsPrevious": "ACCIO_username",
    "firstName": "Arden",
    "lastName": "Ytterberg",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/22/22046_50.jpg",
    "clitterPreferencesUpdated": "2011-01-14T21:55:42.3"
  },
  {
    "idMember": 8189,
    "idPictureMember": 114955,
    "login": "aUdioquark",
    "phoneNumberUs": "3106994607",
    "loginsPrevious": "Bacon, Bourbon, Seahorse Captain",
    "firstName": "Victoria",
    "lastName": "Charnley",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/81/8189_50.jpg",
    "clitterPreferencesUpdated": "2009-06-13T19:52:52.39"
  },
  {
    "idMember": 13314,
    "idPictureMember": 114288,
    "login": "Autumnflame",
    "phoneNumberUs": "2025805050",
    "loginsPrevious": "Shawty",
    "firstName": "Priscilla",
    "lastName": "",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/13314_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T15:43:07.197"
  },
  {
    "idMember": 1824,
    "idPictureMember": null,
    "login": "Bad Wolf",
    "phoneNumberUs": "5162507846",
    "loginsPrevious": "Phoenix, Strawberry Fields Forever, Rizu, MJ Watson",
    "firstName": "Elizabeth",
    "lastName": "Rishmawy",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/18/1824_50.jpg",
    "clitterPreferencesUpdated": "2011-06-16T01:44:58.903"
  },
  {
    "idMember": 1314,
    "idPictureMember": 5722,
    "login": "Beltane",
    "phoneNumberUs": "7328618517",
    "loginsPrevious": null,
    "firstName": "Doug",
    "lastName": "Rowley",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/1314_50.jpg",
    "clitterPreferencesUpdated": "2009-06-12T00:42:00.407"
  },
  {
    "idMember": 1428,
    "idPictureMember": 113412,
    "login": "BLAM!",
    "phoneNumberUs": "9739456839",
    "loginsPrevious": "Blam",
    "firstName": "Branden",
    "lastName": "Lam",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/14/1428_50.jpg",
    "clitterPreferencesUpdated": "2009-06-07T19:37:46.827"
  },
  {
    "idMember": 22640,
    "idPictureMember": 116298,
    "login": "bluemajik",
    "phoneNumberUs": "8057606819",
    "loginsPrevious": "Type desired member name",
    "firstName": "Brandon",
    "lastName": "Morris",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/22/22640_50.jpg",
    "clitterPreferencesUpdated": "2010-06-17T01:48:56.977"
  },
  {
    "idMember": 3701,
    "idPictureMember": 118474,
    "login": "Boudicca",
    "phoneNumberUs": "2524822426",
    "loginsPrevious": "Jew-Hatin' Southern Belle, Logan",
    "firstName": "Lisa",
    "lastName": "Jackson",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/37/3701_50.jpg",
    "clitterPreferencesUpdated": "2010-06-18T09:27:24.417"
  },
  {
    "idMember": 11361,
    "idPictureMember": 116242,
    "login": "Britishly Delicious",
    "phoneNumberUs": "7817925905",
    "loginsPrevious": "DifferentlySane, Dan-germouse, Momo Hazard, Dan Hazard, Method Dan, Mr. Kite, Paddington Bear, Deer Stalker, Johnny English",
    "firstName": "Daniel",
    "lastName": "Faulkner",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/11/11361_50.jpg",
    "clitterPreferencesUpdated": "2009-06-03T14:44:46.233"
  },
  {
    "idMember": 3536,
    "idPictureMember": 113658,
    "login": "BroBra",
    "phoneNumberUs": "3027500743",
    "loginsPrevious": "Vasha, Sugar Tits",
    "firstName": "claire",
    "lastName": "kirby",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/35/3536_50.jpg",
    "clitterPreferencesUpdated": "2009-06-06T14:40:43.733"
  },
  {
    "idMember": 4194,
    "idPictureMember": 95509,
    "login": "cake xplozion",
    "phoneNumberUs": "3016558384",
    "loginsPrevious": "CallieCat, idk my bff tom",
    "firstName": "Rogers",
    "lastName": "Callie",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/41/4194_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T17:15:14.853"
  },
  {
    "idMember": 1322,
    "idPictureMember": 106259,
    "login": "Carmila",
    "phoneNumberUs": "9492937639",
    "loginsPrevious": "Stromboli",
    "firstName": "Rebecca",
    "lastName": "Majoros",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/1322_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T17:11:00.26"
  },
  {
    "idMember": 8096,
    "idPictureMember": 113564,
    "login": "Cash",
    "phoneNumberUs": "2406780283",
    "loginsPrevious": "ShitWasSoCash, GlitchPhil, Starman, Dr. Mouscash, The Walrus, Gotta Cash 'em All, GlitchSkrull, Phil-Phil, Grin Reefer",
    "firstName": "Phil",
    "lastName": "Kahn",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/80/8096_50.jpg",
    "clitterPreferencesUpdated": "2009-06-15T03:16:56.07"
  },
  {
    "idMember": 3602,
    "idPictureMember": 106089,
    "login": "Cat",
    "phoneNumberUs": "4126007221",
    "loginsPrevious": ":(",
    "firstName": "Caitlin",
    "lastName": "Collins",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/36/3602_50.jpg",
    "clitterPreferencesUpdated": "2010-06-21T18:36:48.383"
  },
  {
    "idMember": 18169,
    "idPictureMember": 112595,
    "login": "CHARIZARD",
    "phoneNumberUs": "2569753234",
    "loginsPrevious": "Senko",
    "firstName": "Angela",
    "lastName": "Galzerano",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/18/18169_50.jpg",
    "clitterPreferencesUpdated": "2010-06-17T21:02:32.37"
  },
  {
    "idMember": 10909,
    "idPictureMember": 116161,
    "login": "Chrispy",
    "phoneNumberUs": "4087180060",
    "loginsPrevious": "Token Asian, Annyong, Affirmative Action, Dr. Christache, Ryan, Taxman, Speed Racer, Sartorialist, Ziggy Stardust, Chrispy Creme, CAPTAIN AMODICA",
    "firstName": "Chris",
    "lastName": "Nguyen",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/10/10909_50.jpg",
    "clitterPreferencesUpdated": "2011-01-13T23:11:32.907"
  },
  {
    "idMember": 2560,
    "idPictureMember": 117829,
    "login": "City Councilman Doug",
    "phoneNumberUs": "6142886857",
    "loginsPrevious": "Her Majesty, LeeChan, lisnork",
    "firstName": "Lisa",
    "lastName": "Steward",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/25/2560_50.jpg",
    "clitterPreferencesUpdated": "2010-06-15T08:16:54.89"
  },
  {
    "idMember": 4424,
    "idPictureMember": 109312,
    "login": "Cognac Jack",
    "phoneNumberUs": "6155067839",
    "loginsPrevious": "Action Hank",
    "firstName": "Grant",
    "lastName": "Brian",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/44/4424_50.jpg",
    "clitterPreferencesUpdated": "2010-09-03T17:36:13.14"
  },
  {
    "idMember": 1356,
    "idPictureMember": null,
    "login": "Corey Hart",
    "phoneNumberUs": "7147240116",
    "loginsPrevious": "HowlinMadMurphy",
    "firstName": "Corrie",
    "lastName": "Walton",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/1356_50.jpg",
    "clitterPreferencesUpdated": "2009-06-07T03:14:19.343"
  },
  {
    "idMember": 13829,
    "idPictureMember": 104424,
    "login": "D-Rock",
    "phoneNumberUs": "9526811495",
    "loginsPrevious": null,
    "firstName": "Derek",
    "lastName": "Lund",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/13829_50.jpg",
    "clitterPreferencesUpdated": "2010-10-06T20:42:39.873"
  },
  {
    "idMember": 12155,
    "idPictureMember": 118392,
    "login": "Deathwing",
    "phoneNumberUs": "7062470427",
    "loginsPrevious": "J.R.R. Tolkien (O.G.)",
    "firstName": "Robert",
    "lastName": "Thomason",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/12/12155_50.jpg",
    "clitterPreferencesUpdated": "2009-06-10T18:14:09.95"
  },
  {
    "idMember": 6199,
    "idPictureMember": 112873,
    "login": "Deebo",
    "phoneNumberUs": "4438343507",
    "loginsPrevious": "AT_SpitFire, The Old Spice Guy, The Friendliest Villain, Black Dynamite",
    "firstName": "Jared",
    "lastName": "Adams",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/61/6199_50.jpg",
    "clitterPreferencesUpdated": "2009-06-08T23:25:49.797"
  },
  {
    "idMember": 17358,
    "idPictureMember": 111272,
    "login": "Devil Fruit Bubble Tea",
    "phoneNumberUs": "9087232548",
    "loginsPrevious": "Short Round",
    "firstName": "Anthony",
    "lastName": "Le",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/17/17358_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T11:04:27.84"
  },
  {
    "idMember": 9485,
    "idPictureMember": 118086,
    "login": "Dia",
    "phoneNumberUs": "6786440903",
    "loginsPrevious": "Egg, Nurse Jennifer, Mellow Thighed Chick, Sexy Sadie",
    "firstName": "India",
    "lastName": "Davia",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/94/9485_50.jpg",
    "clitterPreferencesUpdated": "2010-01-02T11:29:03.63"
  },
  {
    "idMember": 3551,
    "idPictureMember": 114796,
    "login": "Dio",
    "phoneNumberUs": "9174066399",
    "loginsPrevious": null,
    "firstName": "Omar",
    "lastName": "Brutus",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/35/3551_50.jpg",
    "clitterPreferencesUpdated": "2010-06-15T20:45:34.623"
  },
  {
    "idMember": 9056,
    "idPictureMember": null,
    "login": "Doctor Who",
    "phoneNumberUs": "5854022204",
    "loginsPrevious": "catgoboom, Sam The Cat Detective",
    "firstName": "Dunham",
    "lastName": "Laurelin",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/90/9056_50.jpg",
    "clitterPreferencesUpdated": "2009-06-13T15:51:51.53"
  },
  {
    "idMember": 17823,
    "idPictureMember": 117425,
    "login": "domminess",
    "phoneNumberUs": "9196071713",
    "loginsPrevious": "Hey Jude, Wild Honey Pie, Nyota Uhura",
    "firstName": "Brooks",
    "lastName": "Dommi",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/17/17823_50.jpg",
    "clitterPreferencesUpdated": "2009-06-02T23:08:56.7"
  },
  {
    "idMember": 7584,
    "idPictureMember": null,
    "login": "Donitsu",
    "phoneNumberUs": "6037852409",
    "loginsPrevious": "Don Booty, Ser Dontos Hollard",
    "firstName": "Don",
    "lastName": "Grover",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/75/7584_50.jpg",
    "clitterPreferencesUpdated": "2009-06-10T17:11:29.31"
  },
  {
    "idMember": 18755,
    "idPictureMember": 117890,
    "login": "DoomMonky",
    "phoneNumberUs": "8476527566",
    "loginsPrevious": "The Fool on the Hill, WE DO NOT SOW, Rhonda, NCC-1701",
    "firstName": "Brandon",
    "lastName": "Behr",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/18/18755_50.jpg",
    "clitterPreferencesUpdated": "2011-10-28T18:05:27.83"
  },
  {
    "idMember": 1734,
    "idPictureMember": 100341,
    "login": "DP2000",
    "phoneNumberUs": "8649189509",
    "loginsPrevious": null,
    "firstName": "Stoop",
    "lastName": "Matthew",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/17/1734_50.jpg",
    "clitterPreferencesUpdated": "2009-06-02T22:58:31.763"
  },
  {
    "idMember": 14001,
    "idPictureMember": 111234,
    "login": "Driggs",
    "phoneNumberUs": "4843327980",
    "loginsPrevious": null,
    "firstName": "Nathaniel",
    "lastName": "Schrier",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/14/14001_50.jpg",
    "clitterPreferencesUpdated": "2009-06-10T21:50:49.75"
  },
  {
    "idMember": 19332,
    "idPictureMember": 96649,
    "login": "DrunkinBartender",
    "phoneNumberUs": "5133124479",
    "loginsPrevious": null,
    "firstName": "David",
    "lastName": "McCarty",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/19/19332_50.jpg",
    "clitterPreferencesUpdated": "2009-06-15T19:57:59.52"
  },
  {
    "idMember": 1379,
    "idPictureMember": 91806,
    "login": "Duck",
    "phoneNumberUs": "3108019450",
    "loginsPrevious": "Ham., momoduck, Octopus's Garden",
    "firstName": "Shing",
    "lastName": "Khor",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/1379_50.jpg",
    "clitterPreferencesUpdated": "2010-06-18T18:16:02.62"
  },
  {
    "idMember": 1623,
    "idPictureMember": null,
    "login": "Duncan",
    "phoneNumberUs": "6785762540",
    "loginsPrevious": "Baduncaduncan",
    "firstName": "Burris",
    "lastName": "Duncan",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/16/1623_50.jpg",
    "clitterPreferencesUpdated": "2009-06-27T18:16:04.623"
  },
  {
    "idMember": 1503,
    "idPictureMember": 114498,
    "login": "eppythatcher",
    "phoneNumberUs": "3017557964",
    "loginsPrevious": "Mark Argent, the sun machine",
    "firstName": "Silverman",
    "lastName": "Mark",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/15/1503_50.jpg",
    "clitterPreferencesUpdated": "2011-01-06T05:45:34.76"
  },
  {
    "idMember": 3393,
    "idPictureMember": 103699,
    "login": "Eternal Drifter",
    "phoneNumberUs": "5712152006",
    "loginsPrevious": "Dave, The Damned",
    "firstName": "Dave",
    "lastName": "Strauss",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/33/3393_50.jpg",
    "clitterPreferencesUpdated": "2009-06-08T22:30:25.47"
  },
  {
    "idMember": 3388,
    "idPictureMember": 114473,
    "login": "evilzug",
    "phoneNumberUs": "3234819297",
    "loginsPrevious": null,
    "firstName": "Tom",
    "lastName": "M",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/33/3388_50.jpg",
    "clitterPreferencesUpdated": "2011-01-14T14:34:00.923"
  },
  {
    "idMember": 4373,
    "idPictureMember": 109615,
    "login": "evol",
    "phoneNumberUs": "6262170079",
    "loginsPrevious": null,
    "firstName": "Bessie",
    "lastName": "Chang",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/43/4373_50.jpg",
    "clitterPreferencesUpdated": "2010-06-18T17:06:40.103"
  },
  {
    "idMember": 20516,
    "idPictureMember": 102868,
    "login": "ExecutiveProducerDickWolf",
    "phoneNumberUs": "6789257941",
    "loginsPrevious": "incandenza, Matthias",
    "firstName": "Matt",
    "lastName": "Michelson",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/20/20516_50.jpg",
    "clitterPreferencesUpdated": "2009-06-12T00:50:20.733"
  },
  {
    "idMember": 2117,
    "idPictureMember": 112735,
    "login": "Fang",
    "phoneNumberUs": "9497356135",
    "loginsPrevious": "Ayane, Bliss, So-so Romance, Captain Commando",
    "firstName": "shiki",
    "lastName": "enomoto",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/21/2117_50.jpg",
    "clitterPreferencesUpdated": "2009-06-04T01:06:23.467"
  },
  {
    "idMember": 12619,
    "idPictureMember": 118349,
    "login": "fangy",
    "phoneNumberUs": "2178015281",
    "loginsPrevious": "Kentucky, Twinkletoes, Commander Shepard",
    "firstName": "Johnathan",
    "lastName": "Alexander",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/12/12619_50.jpg",
    "clitterPreferencesUpdated": "2009-06-17T06:49:38.097"
  },
  {
    "idMember": 11278,
    "idPictureMember": 115086,
    "login": "Fiver",
    "phoneNumberUs": "5184289133",
    "loginsPrevious": null,
    "firstName": "Mike",
    "lastName": "Boston",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/11/11278_50.jpg",
    "clitterPreferencesUpdated": "2011-01-15T19:12:01.117"
  },
  {
    "idMember": 8912,
    "idPictureMember": 116253,
    "login": "Float",
    "phoneNumberUs": "6128453992",
    "loginsPrevious": "floatingtrem, Mr. FT, Sledgehamer Elephant",
    "firstName": "Erich",
    "lastName": "Buenger",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/89/8912_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T15:00:20.243"
  },
  {
    "idMember": 13707,
    "idPictureMember": 117895,
    "login": "fukkake",
    "phoneNumberUs": "2154600208",
    "loginsPrevious": "fukkatsu, Fuk kake, 36DD Hooter Frenzy",
    "firstName": "Stephanie",
    "lastName": "Zaidinski",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/13707_50.jpg",
    "clitterPreferencesUpdated": "2009-06-02T22:29:35.263"
  },
  {
    "idMember": 5619,
    "idPictureMember": 84376,
    "login": "GentlemanHobo",
    "phoneNumberUs": "7083414368",
    "loginsPrevious": "Pazuzuzu, Boring Death",
    "firstName": "Jon",
    "lastName": "Myers",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/56/5619_50.jpg",
    "clitterPreferencesUpdated": "2009-06-11T15:24:26.17"
  },
  {
    "idMember": 16546,
    "idPictureMember": 112808,
    "login": "Gnarly Sheen",
    "phoneNumberUs": "9105783565",
    "loginsPrevious": "The Jukebox Bul, OldDays_BadDays, The Jukebox Bully",
    "firstName": "",
    "lastName": "",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/16/16546_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T18:57:11.697"
  },
  {
    "idMember": 5722,
    "idPictureMember": 74134,
    "login": "Grave",
    "phoneNumberUs": "6314550153",
    "loginsPrevious": null,
    "firstName": "scott",
    "lastName": "richards",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/57/5722_50.jpg",
    "clitterPreferencesUpdated": "2009-06-03T13:16:03.827"
  },
  {
    "idMember": 5372,
    "idPictureMember": 118478,
    "login": "Hunky Dory",
    "phoneNumberUs": "4109293679",
    "loginsPrevious": "Jude, Kike-o, Mojit-o, Gonz-o, Duck Season, Captain Kirk, Spike-o, Horse's Ass",
    "firstName": "Rosen",
    "lastName": "Jeff",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/53/5372_50.jpg",
    "clitterPreferencesUpdated": "2010-01-17T22:46:15.23"
  },
  {
    "idMember": 14146,
    "idPictureMember": 111420,
    "login": "iamrhysfinch",
    "phoneNumberUs": "2106393405",
    "loginsPrevious": "DrCrowdPleaser",
    "firstName": "Finch",
    "lastName": "Rhys",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/14/14146_50.jpg",
    "clitterPreferencesUpdated": "2009-06-10T13:22:19.577"
  },
  {
    "idMember": 9451,
    "idPictureMember": 116785,
    "login": "IRON MOD",
    "phoneNumberUs": "8185231577",
    "loginsPrevious": "Tawny Isn't Very Tawny, Coenzyme T, Valheru, Chilly Down, The Tanya, Atilla the Cunt, Your Dyketator, The Old Scientist Guy, Lady Sphincter, Dr. Taylor Lay",
    "firstName": "",
    "lastName": "",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/94/9451_50.jpg",
    "clitterPreferencesUpdated": "2010-06-18T11:17:42.243"
  },
  {
    "idMember": 3272,
    "idPictureMember": 67919,
    "login": "J-Rock",
    "phoneNumberUs": "3028984633",
    "loginsPrevious": null,
    "firstName": "Joseph",
    "lastName": "Green",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/32/3272_50.jpg",
    "clitterPreferencesUpdated": "2011-06-21T06:31:18.997"
  },
  {
    "idMember": 6107,
    "idPictureMember": 116909,
    "login": "jennyfur",
    "phoneNumberUs": "2678082708",
    "loginsPrevious": null,
    "firstName": "Jen",
    "lastName": "",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/61/6107_50.jpg",
    "clitterPreferencesUpdated": "2011-06-22T00:40:00.717"
  },
  {
    "idMember": 15885,
    "idPictureMember": 113765,
    "login": "Jkid",
    "phoneNumberUs": "2023061027",
    "loginsPrevious": null,
    "firstName": "",
    "lastName": "",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/15/15885_50.jpg",
    "clitterPreferencesUpdated": "2010-01-02T12:04:17.863"
  },
  {
    "idMember": 1238,
    "idPictureMember": 114711,
    "login": "John Booty",
    "phoneNumberUs": "4842135700",
    "loginsPrevious": "Make Your Mother Sigh, Kneel Before ZOD, Modimus Prime",
    "firstName": "Brown                    ",
    "lastName": "Sally                    ",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/12/1238_50.jpg",
    "clitterPreferencesUpdated": "2009-06-08T00:42:33.53"
  },
  {
    "idMember": 12769,
    "idPictureMember": 96127,
    "login": "Jonci",
    "phoneNumberUs": "5712055815",
    "loginsPrevious": null,
    "firstName": "Jonci",
    "lastName": "Aguillard",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/12/12769_50.jpg",
    "clitterPreferencesUpdated": "2011-01-14T16:56:00.67"
  },
  {
    "idMember": 22995,
    "idPictureMember": 108420,
    "login": "Jugs",
    "phoneNumberUs": "4357306707",
    "loginsPrevious": "Rosie Cotton",
    "firstName": "",
    "lastName": "",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/22/22995_50.jpg",
    "clitterPreferencesUpdated": "2010-06-15T23:31:11.047"
  },
  {
    "idMember": 20262,
    "idPictureMember": 96028,
    "login": "Kamen",
    "phoneNumberUs": "7033433493",
    "loginsPrevious": null,
    "firstName": "Don",
    "lastName": "Frank",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/20/20262_50.jpg",
    "clitterPreferencesUpdated": "2009-06-12T13:27:17.67"
  },
  {
    "idMember": 3894,
    "idPictureMember": 116791,
    "login": "Kapt Dai",
    "phoneNumberUs": "8104297796",
    "loginsPrevious": "Bungalow Bill",
    "firstName": "Wilson",
    "lastName": "Wm",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/38/3894_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T15:50:44.573"
  },
  {
    "idMember": 3065,
    "idPictureMember": 115777,
    "login": "Kasai",
    "phoneNumberUs": "2019560726",
    "loginsPrevious": null,
    "firstName": "Liz",
    "lastName": "Italiaander",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/30/3065_50.jpg",
    "clitterPreferencesUpdated": "2010-06-15T13:48:47.03"
  },
  {
    "idMember": 22146,
    "idPictureMember": 118163,
    "login": "Kev",
    "phoneNumberUs": "6108092461",
    "loginsPrevious": null,
    "firstName": "Kevin",
    "lastName": "Costello",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/22/22146_50.jpg",
    "clitterPreferencesUpdated": "2010-06-15T16:10:54.78"
  },
  {
    "idMember": 2546,
    "idPictureMember": 117931,
    "login": "Killian",
    "phoneNumberUs": "9105549430",
    "loginsPrevious": "$$$$$$$$$$$$$$$$$$$$$$$$$, Dr. Dirtstache, Sgt Pepper",
    "firstName": "Corum",
    "lastName": "Sam",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/25/2546_50.jpg",
    "clitterPreferencesUpdated": "2009-12-31T16:18:20.433"
  },
  {
    "idMember": 11776,
    "idPictureMember": 117399,
    "login": "Kimchi Maker",
    "phoneNumberUs": "3122594914",
    "loginsPrevious": "JazzJack, MayBROnnaise, JizzJackoff, Seoul Brother",
    "firstName": "",
    "lastName": "",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/11/11776_50.jpg",
    "clitterPreferencesUpdated": "2011-06-20T08:22:40.543"
  },
  {
    "idMember": 2554,
    "idPictureMember": null,
    "login": "Lamb Eater",
    "phoneNumberUs": "4438384554",
    "loginsPrevious": "Rabbit Se, Rabbit Season, Eight Days A Week, *Torro_Torro*, Torro Torro",
    "firstName": "Allen",
    "lastName": "Jen",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/25/2554_50.jpg",
    "clitterPreferencesUpdated": "2009-06-03T12:33:18.123"
  },
  {
    "idMember": 17027,
    "idPictureMember": 113848,
    "login": "Laurette",
    "phoneNumberUs": "8047124470",
    "loginsPrevious": "Hippy Hippy Shake, Arya, Brovolone Cheese, VELOCIAPRICOT, The Last Jizzbender, Sam The Eagle",
    "firstName": "Tiffany",
    "lastName": "Crawford",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/17/17027_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T22:04:09.313"
  },
  {
    "idMember": 1425,
    "idPictureMember": 115994,
    "login": "LORD AWESOME",
    "phoneNumberUs": "4847449265",
    "loginsPrevious": "Joe The Plumber, MOMOSAYMOMOSAHMOMOKUSAH, Sailor Penis, Tony Hawk: BADGER, BARON SPLENDID, Back In The U.S.S.R., Toilet Robin Hood, SUPER NINTENDO",
    "firstName": "Christopher",
    "lastName": "Erb",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/14/1425_50.jpg",
    "clitterPreferencesUpdated": "2011-01-14T15:34:48.223"
  },
  {
    "idMember": 1711,
    "idPictureMember": 106945,
    "login": "LostDecoy",
    "phoneNumberUs": "5169744651",
    "loginsPrevious": null,
    "firstName": "Petrone",
    "lastName": "Christina",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/17/1711_50.jpg",
    "clitterPreferencesUpdated": "2010-06-17T01:22:00.54"
  },
  {
    "idMember": 14243,
    "idPictureMember": 117773,
    "login": "Mac",
    "phoneNumberUs": "3525141040",
    "loginsPrevious": "DarkLotus, Momo",
    "firstName": "Carlisle",
    "lastName": "Melissa",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/14/14243_50.jpg",
    "clitterPreferencesUpdated": "2009-06-03T17:11:39.14"
  },
  {
    "idMember": 13455,
    "idPictureMember": 117663,
    "login": "Maddtoaster",
    "phoneNumberUs": "2019209103",
    "loginsPrevious": null,
    "firstName": "Joey",
    "lastName": "",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/13455_50.jpg",
    "clitterPreferencesUpdated": "2010-06-20T15:27:04.76"
  },
  {
    "idMember": 9509,
    "idPictureMember": 100836,
    "login": "malikyiaue",
    "phoneNumberUs": "7708836945",
    "loginsPrevious": null,
    "firstName": "Erin",
    "lastName": "McCulley",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/95/9509_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T14:27:14.927"
  },
  {
    "idMember": 4985,
    "idPictureMember": 108489,
    "login": "MamaSkull",
    "phoneNumberUs": "2672425565",
    "loginsPrevious": "MommySkullFish",
    "firstName": "Jacqueline",
    "lastName": "Reitnauer",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/49/4985_50.jpg",
    "clitterPreferencesUpdated": "2009-06-09T12:09:46.123"
  },
  {
    "idMember": 7301,
    "idPictureMember": 106531,
    "login": "Mandori",
    "phoneNumberUs": "8017170711",
    "loginsPrevious": "Momo-sama",
    "firstName": "Williams",
    "lastName": "Midori",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/73/7301_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T17:35:06.04"
  },
  {
    "idMember": 3434,
    "idPictureMember": 115948,
    "login": "marinasaurus rex",
    "phoneNumberUs": "7073570752",
    "loginsPrevious": "flammable_kitty",
    "firstName": "Zekley",
    "lastName": "Marina",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/34/3434_50.jpg",
    "clitterPreferencesUpdated": "2011-06-20T10:08:20.373"
  },
  {
    "idMember": 2818,
    "idPictureMember": 117779,
    "login": "Matthew",
    "phoneNumberUs": "6144034649",
    "loginsPrevious": "ninjer",
    "firstName": "Bondy",
    "lastName": "Matthew",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/28/2818_50.jpg",
    "clitterPreferencesUpdated": "2011-06-15T23:39:24.71"
  },
  {
    "idMember": 1393,
    "idPictureMember": 105997,
    "login": "May the Lord Have Marcy",
    "phoneNumberUs": "4102621345",
    "loginsPrevious": "oohimemiyaoo, Marcy",
    "firstName": "Marcy",
    "lastName": "Hughes",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/1393_50.jpg",
    "clitterPreferencesUpdated": "2010-06-19T21:32:45.29"
  },
  {
    "idMember": 11871,
    "idPictureMember": 103291,
    "login": "MechaSteve",
    "phoneNumberUs": "4042134038",
    "loginsPrevious": "Thread Killa",
    "firstName": "Stephen",
    "lastName": "Culpepper",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/11/11871_50.jpg",
    "clitterPreferencesUpdated": "2009-06-03T01:06:01.67"
  },
  {
    "idMember": 3021,
    "idPictureMember": 117776,
    "login": "Mel",
    "phoneNumberUs": "9073151299",
    "loginsPrevious": "I Am the Milkshake",
    "firstName": "William",
    "lastName": "Growden",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/30/3021_50.jpg",
    "clitterPreferencesUpdated": "2011-09-02T19:17:51.173"
  },
  {
    "idMember": 1328,
    "idPictureMember": 108720,
    "login": "mheart",
    "phoneNumberUs": "5408501470",
    "loginsPrevious": "mheart the great, Rebel Rebel",
    "firstName": "Lockett",
    "lastName": "Brianna",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/1328_50.jpg",
    "clitterPreferencesUpdated": "2010-06-15T20:24:09.31"
  },
  {
    "idMember": 12735,
    "idPictureMember": 118030,
    "login": "momomomocide",
    "phoneNumberUs": "6318052114",
    "loginsPrevious": "momojenocide, Momo of Bond Street, cant buy momo love",
    "firstName": "Melillo",
    "lastName": "Jennie",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/12/12735_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T18:10:28.947"
  },
  {
    "idMember": 20557,
    "idPictureMember": 109361,
    "login": "Moonage Daydream",
    "phoneNumberUs": "7088371935",
    "loginsPrevious": "mrpenbrook, Phantom of the J.C.Penney, Pumpkin Pie, Blue Jay Way, Haricot Vert, Jethro Participle, STATION, STATIOUN, Jaded Jag, Dengar, Alistair Cookie, Blaster, Hodor",
    "firstName": "Jarod",
    "lastName": "Pranno",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/20/20557_50.jpg",
    "clitterPreferencesUpdated": "2010-06-20T21:48:05.743"
  },
  {
    "idMember": 2157,
    "idPictureMember": 118201,
    "login": "nomad",
    "phoneNumberUs": "5149412851",
    "loginsPrevious": "Tobias Fünke, M.D., NostraBangBus, Xantar Jr., Assman, NOAMRNWA, Daddy Warbutts",
    "firstName": "Rick",
    "lastName": "Kiriakidis",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/21/2157_50.jpg",
    "clitterPreferencesUpdated": "2009-06-16T19:03:48.817"
  },
  {
    "idMember": 4092,
    "idPictureMember": 117854,
    "login": "NomiJade",
    "phoneNumberUs": "5622564374",
    "loginsPrevious": "Lucy Sky Diamond",
    "firstName": "Nomi",
    "lastName": "Jade",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/40/4092_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T23:15:59.84"
  },
  {
    "idMember": 12359,
    "idPictureMember": 115934,
    "login": "Nostalgia",
    "phoneNumberUs": "7576184145",
    "loginsPrevious": null,
    "firstName": "Brian",
    "lastName": "Smith",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/12/12359_50.jpg",
    "clitterPreferencesUpdated": "2010-06-15T11:04:36.607"
  },
  {
    "idMember": 2249,
    "idPictureMember": 99699,
    "login": "Nowhere Man",
    "phoneNumberUs": "3049528527",
    "loginsPrevious": "Man Who Sold The World, Starfire",
    "firstName": "Justin",
    "lastName": "Grathwohl",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/22/2249_50.jpg",
    "clitterPreferencesUpdated": "2010-06-15T14:26:19.077"
  },
  {
    "idMember": 8623,
    "idPictureMember": 50045,
    "login": "Oliphaunt",
    "phoneNumberUs": "7808935081",
    "loginsPrevious": "Best7BucksEverSpent, That Guy, Major Moose, Lt.-Cmdr. Snowth, Ensign Scooter, Kiss My Shiny Metal Ass",
    "firstName": "Kris",
    "lastName": "S.",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/86/8623_50.jpg",
    "clitterPreferencesUpdated": "2011-06-20T13:30:08.19"
  },
  {
    "idMember": 1357,
    "idPictureMember": 114517,
    "login": "Oshi",
    "phoneNumberUs": "4843544694",
    "loginsPrevious": null,
    "firstName": "Paul",
    "lastName": "Sunner",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/1357_50.jpg",
    "clitterPreferencesUpdated": "2010-01-02T13:32:29.897"
  },
  {
    "idMember": 15315,
    "idPictureMember": 106374,
    "login": "OverHeadCam",
    "phoneNumberUs": "3174267844",
    "loginsPrevious": "Idiot Parrot Man, Jake the Dog",
    "firstName": "Cameron",
    "lastName": "Richmond",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/15/15315_50.jpg",
    "clitterPreferencesUpdated": "2010-01-01T10:02:00.513"
  },
  {
    "idMember": 17101,
    "idPictureMember": 107797,
    "login": "P.Y.T.",
    "phoneNumberUs": "6193996747",
    "loginsPrevious": "xoxostrawberry",
    "firstName": "Rowena",
    "lastName": "Bautista",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/17/17101_50.jpg",
    "clitterPreferencesUpdated": "2009-06-10T22:27:11.513"
  },
  {
    "idMember": 13051,
    "idPictureMember": 113426,
    "login": "pamelaNeko",
    "phoneNumberUs": "3129536887",
    "loginsPrevious": "The Seaward, Polythene Pam",
    "firstName": "",
    "lastName": "",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/13051_50.jpg",
    "clitterPreferencesUpdated": "2011-06-10T11:12:00.49"
  },
  {
    "idMember": 13763,
    "idPictureMember": 96529,
    "login": "Paperback Writer",
    "phoneNumberUs": "2027251917",
    "loginsPrevious": "falseprophet",
    "firstName": "Totty",
    "lastName": "Lindsay",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/13763_50.jpg",
    "clitterPreferencesUpdated": "2011-01-14T13:50:59.187"
  },
  {
    "idMember": 4538,
    "idPictureMember": 111000,
    "login": "Peach",
    "phoneNumberUs": "4344701398",
    "loginsPrevious": "Mrs. Bowie, Princess Peach",
    "firstName": "Brown",
    "lastName": "Erin",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/45/4538_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T07:24:07.993"
  },
  {
    "idMember": 9464,
    "idPictureMember": 95443,
    "login": "Pez",
    "phoneNumberUs": "9739858017",
    "loginsPrevious": null,
    "firstName": "Christopher",
    "lastName": "Filippis",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/94/9464_50.jpg",
    "clitterPreferencesUpdated": "2009-06-14T01:27:44.11"
  },
  {
    "idMember": 15285,
    "idPictureMember": 114953,
    "login": "Pixie~",
    "phoneNumberUs": "5403161901",
    "loginsPrevious": "MomoMimosa",
    "firstName": "Adrianne",
    "lastName": "Lunn",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/15/15285_50.jpg",
    "clitterPreferencesUpdated": "2011-01-13T00:31:12.11"
  },
  {
    "idMember": 14806,
    "idPictureMember": 104647,
    "login": "plumis",
    "phoneNumberUs": "5133134284",
    "loginsPrevious": null,
    "firstName": "kwong",
    "lastName": "sherri",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/14/14806_50.jpg",
    "clitterPreferencesUpdated": "2010-06-17T17:42:09.18"
  },
  {
    "idMember": 15826,
    "idPictureMember": 117891,
    "login": "Princess Katy Cuppincakes",
    "phoneNumberUs": "7143308764",
    "loginsPrevious": "Across The Universe, NekoStar",
    "firstName": "Kat",
    "lastName": "O'Connor",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/15/15826_50.jpg",
    "clitterPreferencesUpdated": "2011-01-13T18:54:29.217"
  },
  {
    "idMember": 7047,
    "idPictureMember": 116085,
    "login": "Put A Bird On It!",
    "phoneNumberUs": "6787554235",
    "loginsPrevious": "PopeCorky, Jareth the Goblin King, Father McKenzie, Earl Grey Hot, Jizanthipus, Sir Digby Chicken Caesar, Pope Corky XXIII",
    "firstName": "Alberghini",
    "lastName": "Luke",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/70/7047_50.jpg",
    "clitterPreferencesUpdated": "2010-06-15T08:56:21.793"
  },
  {
    "idMember": 21815,
    "idPictureMember": 118460,
    "login": "Quul",
    "phoneNumberUs": "4789529306",
    "loginsPrevious": "A Taste Of Honey, Evia",
    "firstName": "Melissa",
    "lastName": "Ritchie",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/21/21815_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T22:14:30.273"
  },
  {
    "idMember": 2406,
    "idPictureMember": null,
    "login": "Ratnax",
    "phoneNumberUs": "8567704490",
    "loginsPrevious": "Xantar",
    "firstName": "Patrick",
    "lastName": "McKee",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/24/2406_50.jpg",
    "clitterPreferencesUpdated": "2010-06-19T01:31:39.743"
  },
  {
    "idMember": 2951,
    "idPictureMember": 113144,
    "login": "Ree_The_Tard",
    "phoneNumberUs": "7176766505",
    "loginsPrevious": "Ree, \"R-Word\", Brotato Bread",
    "firstName": "Ree",
    "lastName": "Rondeau",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/29/2951_50.jpg",
    "clitterPreferencesUpdated": "2011-06-17T06:00:39.59"
  },
  {
    "idMember": 5542,
    "idPictureMember": 118282,
    "login": "Rexall",
    "phoneNumberUs": "7329101570",
    "loginsPrevious": "Papi, Mark David Chapman",
    "firstName": "Danny",
    "lastName": "Minhas",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/55/5542_50.jpg",
    "clitterPreferencesUpdated": "2009-06-03T13:29:42.95"
  },
  {
    "idMember": 13184,
    "idPictureMember": 118247,
    "login": "RogueSamus",
    "phoneNumberUs": "5743393973",
    "loginsPrevious": "Joe Six-Pack, MR F, Finn the Human",
    "firstName": "Samus",
    "lastName": "Aran",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/13184_50.jpg",
    "clitterPreferencesUpdated": "2009-06-10T12:40:15.327"
  },
  {
    "idMember": 6632,
    "idPictureMember": 116580,
    "login": "Ryan the Lion",
    "phoneNumberUs": "8474453404",
    "loginsPrevious": "Komm, Gib Mir Deine Hand, Littlefinger, Woohoo Cthulhu, Tarsier Tyrant, Fozzie, The Voice of Nurgle, G.W. Hammercock",
    "firstName": "Ryon",
    "lastName": "Numrich",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/66/6632_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T18:38:40.51"
  },
  {
    "idMember": 5616,
    "idPictureMember": 116866,
    "login": "Sans Sheriff",
    "phoneNumberUs": "5135602779",
    "loginsPrevious": "moistbuffalovag",
    "firstName": "Aarib",
    "lastName": "Burghard",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/56/5616_50.jpg",
    "clitterPreferencesUpdated": "2010-06-22T03:46:05.15"
  },
  {
    "idMember": 3246,
    "idPictureMember": 117190,
    "login": "Scarlett",
    "phoneNumberUs": "6199615179",
    "loginsPrevious": "Penny Lane, Ygritte, It's Not Easy Being Breen",
    "firstName": "Scarlett",
    "lastName": "Dowdy",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/32/3246_50.jpg",
    "clitterPreferencesUpdated": "2010-06-15T10:33:13.093"
  },
  {
    "idMember": 14514,
    "idPictureMember": 117749,
    "login": "seggs",
    "phoneNumberUs": "9522506176",
    "loginsPrevious": "SCRUFFYFAGGOT69, Zoot, Sorry Bout The CD Thing",
    "firstName": "Sean",
    "lastName": "Segersin",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/14/14514_50.jpg",
    "clitterPreferencesUpdated": "2010-06-17T01:33:11.27"
  },
  {
    "idMember": 20051,
    "idPictureMember": 118223,
    "login": "Sharp Cheddar",
    "phoneNumberUs": "8182615395",
    "loginsPrevious": "Rock 'n' Roll Suicide, Rocky Raccoon, Maverick Dangerous",
    "firstName": "Tommy",
    "lastName": "Eisenhauer",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/20/20051_50.jpg",
    "clitterPreferencesUpdated": "2010-06-16T20:34:17.583"
  },
  {
    "idMember": 13711,
    "idPictureMember": 104166,
    "login": "ShirtNinja",
    "phoneNumberUs": "4344096045",
    "loginsPrevious": null,
    "firstName": "Lucas",
    "lastName": "White",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/13711_50.jpg",
    "clitterPreferencesUpdated": "2010-01-01T01:16:50.52"
  },
  {
    "idMember": 7542,
    "idPictureMember": 112320,
    "login": "Shot Dad",
    "phoneNumberUs": "5037536732",
    "loginsPrevious": "TheFritoB, Threadsecutioner, Dr. Derpenstein, Frito, Sweetums",
    "firstName": "",
    "lastName": "",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/75/7542_50.jpg",
    "clitterPreferencesUpdated": "2010-06-18T00:45:33.15"
  },
  {
    "idMember": 16157,
    "idPictureMember": 115644,
    "login": "Siglah",
    "phoneNumberUs": "9043471120",
    "loginsPrevious": null,
    "firstName": "",
    "lastName": "",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/16/16157_50.jpg",
    "clitterPreferencesUpdated": "2009-06-13T12:41:47.013"
  },
  {
    "idMember": 6571,
    "idPictureMember": 113389,
    "login": "Skewdles",
    "phoneNumberUs": "4043455743",
    "loginsPrevious": "When I'm Nintendo 64, HELLO, I AM THE INTERNET!, beef jerky eagle, DJO Apple Juice",
    "firstName": "SQ",
    "lastName": "Sunseri",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/65/6571_50.jpg",
    "clitterPreferencesUpdated": "2010-06-15T16:14:19.007"
  },
  {
    "idMember": 1868,
    "idPictureMember": 36828,
    "login": "Skits",
    "phoneNumberUs": "3016749658",
    "loginsPrevious": null,
    "firstName": "Cohen",
    "lastName": "Reuven",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/18/1868_50.jpg",
    "clitterPreferencesUpdated": "2009-06-12T12:31:24.967"
  },
  {
    "idMember": 9880,
    "idPictureMember": 109577,
    "login": "Slade xTekno",
    "phoneNumberUs": "6618895527",
    "loginsPrevious": "UBER-NERD",
    "firstName": "Josh",
    "lastName": "Shin",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/98/9880_50.jpg",
    "clitterPreferencesUpdated": "2010-10-29T18:52:55.44"
  },
  {
    "idMember": 2139,
    "idPictureMember": 100838,
    "login": "soaze",
    "phoneNumberUs": "5166066736",
    "loginsPrevious": "henry chinaski",
    "firstName": "O'Brien",
    "lastName": "Tim",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/21/2139_50.jpg",
    "clitterPreferencesUpdated": "2011-01-15T14:07:59.873"
  },
  {
    "idMember": 17084,
    "idPictureMember": 115435,
    "login": "soulbird",
    "phoneNumberUs": "8323315488",
    "loginsPrevious": null,
    "firstName": "Vivian",
    "lastName": "Perez",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/17/17084_50.jpg",
    "clitterPreferencesUpdated": "2011-01-05T05:22:43.977"
  },
  {
    "idMember": 6276,
    "idPictureMember": 115560,
    "login": "sparkledonkey",
    "phoneNumberUs": "2064340820",
    "loginsPrevious": null,
    "firstName": "Jen",
    "lastName": "Bixby",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/62/6276_50.jpg",
    "clitterPreferencesUpdated": "2011-06-12T15:28:20.91"
  },
  {
    "idMember": 16458,
    "idPictureMember": 110153,
    "login": "SpeedBrkr",
    "phoneNumberUs": "2245221557",
    "loginsPrevious": "Sun King",
    "firstName": "",
    "lastName": "",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/16/16458_50.jpg",
    "clitterPreferencesUpdated": "2010-06-18T15:23:33.417"
  },
  {
    "idMember": 2263,
    "idPictureMember": 112049,
    "login": "spyrral",
    "phoneNumberUs": "3107706322",
    "loginsPrevious": "Gazpacho Hammer, The Clever Name Change, OtakuBootyTopUser spyrral, John&#8201;&#8201;Booty",
    "firstName": "Sasha",
    "lastName": "Sklar",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/22/2263_50.jpg",
    "clitterPreferencesUpdated": "2010-06-17T23:45:21.807"
  },
  {
    "idMember": 4348,
    "idPictureMember": 114338,
    "login": "StuntCock",
    "phoneNumberUs": "3103517305",
    "loginsPrevious": "Wingman, The Sovereign, Cry Baby Cry, Sean's Turgid Penis",
    "firstName": "Sean",
    "lastName": "Manzano",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/43/4348_50.jpg",
    "clitterPreferencesUpdated": "2009-06-13T17:48:37.047"
  },
  {
    "idMember": 19777,
    "idPictureMember": 118193,
    "login": "tachikoma",
    "phoneNumberUs": "9253608246",
    "loginsPrevious": "jumpjump",
    "firstName": "Christine",
    "lastName": "DeMartinis",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/19/19777_50.jpg",
    "clitterPreferencesUpdated": "2010-06-17T01:53:18.073"
  },
  {
    "idMember": 7150,
    "idPictureMember": 95523,
    "login": "Tech Bromancer",
    "phoneNumberUs": "2154806278",
    "loginsPrevious": "tomweiser",
    "firstName": "",
    "lastName": "",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/71/7150_50.jpg",
    "clitterPreferencesUpdated": "2009-06-02T22:24:38.607"
  },
  {
    "idMember": 21233,
    "idPictureMember": 97394,
    "login": "ThatguyChris",
    "phoneNumberUs": "4239468836",
    "loginsPrevious": null,
    "firstName": "Chris",
    "lastName": "Rowe",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/21/21233_50.jpg",
    "clitterPreferencesUpdated": "2010-01-03T13:34:10.02"
  },
  {
    "idMember": 21509,
    "idPictureMember": 118477,
    "login": "The happy sausage artist",
    "phoneNumberUs": "2402649899",
    "loginsPrevious": "Willy Wonka, usarmydoc, OB-doc, EMH",
    "firstName": "Ross",
    "lastName": "Meade",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/21/21509_50.jpg",
    "clitterPreferencesUpdated": "2011-06-22T00:26:51.95"
  },
  {
    "idMember": 15215,
    "idPictureMember": 113464,
    "login": "The Naniwa Tiger",
    "phoneNumberUs": "7707577469",
    "loginsPrevious": "Rufio, COCKRUB WARRIOR, Ulyth, William The Conqueror, HUSTLA DA RABBIT",
    "firstName": "Parrish",
    "lastName": "Billy",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/15/15215_50.jpg",
    "clitterPreferencesUpdated": "2011-06-20T22:11:17.903"
  },
  {
    "idMember": 10606,
    "idPictureMember": 42752,
    "login": "The Operator",
    "phoneNumberUs": "3023791847",
    "loginsPrevious": "3-Stacks, WhiteSquirrel, Party Foul, Young American, Number Nine",
    "firstName": "Alan",
    "lastName": "Threefoot",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/10/10606_50.jpg",
    "clitterPreferencesUpdated": "2011-06-16T02:19:14.847"
  },
  {
    "idMember": 15928,
    "idPictureMember": null,
    "login": "Thunderer",
    "phoneNumberUs": "7065662330",
    "loginsPrevious": "Action Bastard, The Big Bad Booty Daddy",
    "firstName": "Kellen",
    "lastName": "Neal",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/15/15928_50.jpg",
    "clitterPreferencesUpdated": "2009-06-02T22:57:23.25"
  },
  {
    "idMember": 1332,
    "idPictureMember": 91797,
    "login": "Tiara",
    "phoneNumberUs": "2175560939",
    "loginsPrevious": "Noodlehead, Tiramisu",
    "firstName": "Boston",
    "lastName": "Nicole",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/1332_50.jpg",
    "clitterPreferencesUpdated": "2009-06-08T02:18:36.39"
  },
  {
    "idMember": 6347,
    "idPictureMember": 78236,
    "login": "Tim",
    "phoneNumberUs": "3232060322",
    "loginsPrevious": "Paddy O'Furniture, SiliconeSoldier",
    "firstName": "Tim",
    "lastName": "Williams",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/63/6347_50.jpg",
    "clitterPreferencesUpdated": "2010-09-04T19:06:22.417"
  },
  {
    "idMember": 1415,
    "idPictureMember": 116050,
    "login": "trampersand",
    "phoneNumberUs": "4438021031",
    "loginsPrevious": "Eleanor Rigby, twistedkissed, GodzillaTwins, Sexy Sex",
    "firstName": "rodriguez",
    "lastName": "rose",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/14/1415_50.jpg",
    "clitterPreferencesUpdated": "2010-06-15T11:16:28.483"
  },
  {
    "idMember": 6920,
    "idPictureMember": 116917,
    "login": "tranoid no ki",
    "phoneNumberUs": "5165224569",
    "loginsPrevious": "xscarydrummerx, Hustla Da Bunny, Dorothy Zbornak, Princess Shoujo Maiku, Straight Baby Thighs, tranoidnoki@buttmail.com",
    "firstName": "Sheridan",
    "lastName": "Mike",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/69/6920_50.jpg",
    "clitterPreferencesUpdated": "2011-06-09T20:27:05.313"
  },
  {
    "idMember": 6367,
    "idPictureMember": 117345,
    "login": "TrigunnerSyphon",
    "phoneNumberUs": "2159101048",
    "loginsPrevious": null,
    "firstName": "Syphon",
    "lastName": "Tristaria",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/63/6367_50.jpg",
    "clitterPreferencesUpdated": "2009-06-04T06:39:02.53"
  },
  {
    "idMember": 16857,
    "idPictureMember": 118041,
    "login": "triM",
    "phoneNumberUs": "8182096184",
    "loginsPrevious": null,
    "firstName": "Matthew",
    "lastName": "Mangini",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/16/16857_50.jpg",
    "clitterPreferencesUpdated": "2010-06-21T01:37:51.277"
  },
  {
    "idMember": 14214,
    "idPictureMember": 110723,
    "login": "usarnaem",
    "phoneNumberUs": "6784317347",
    "loginsPrevious": "Fuckshoes, Darling Facist Bullyboy, Darling Fascist Bullyboy, Throatwobbler Mangrove",
    "firstName": "Walden",
    "lastName": "Jesse",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/14/14214_50.jpg",
    "clitterPreferencesUpdated": "2009-06-14T23:50:23.69"
  },
  {
    "idMember": 22726,
    "idPictureMember": 113446,
    "login": "VicViper",
    "phoneNumberUs": "5204004242",
    "loginsPrevious": null,
    "firstName": "Jose",
    "lastName": "Rios",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/22/22726_50.jpg",
    "clitterPreferencesUpdated": "2011-06-17T02:39:03.733"
  },
  {
    "idMember": 13736,
    "idPictureMember": 91907,
    "login": "Wampus Cat",
    "phoneNumberUs": "4344664475",
    "loginsPrevious": "Ka-sama, Se_chan",
    "firstName": "Ashley",
    "lastName": "White",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/13/13736_50.jpg",
    "clitterPreferencesUpdated": "2009-06-03T12:17:44.81"
  },
  {
    "idMember": 12583,
    "idPictureMember": 113641,
    "login": "Winter",
    "phoneNumberUs": "2024896986",
    "loginsPrevious": "TheInsomniac",
    "firstName": "Ryan",
    "lastName": "Taylor",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/12/12583_50.jpg",
    "clitterPreferencesUpdated": "2009-12-31T15:18:29.807"
  },
  {
    "idMember": 5892,
    "idPictureMember": 110785,
    "login": "xoring",
    "phoneNumberUs": "2406788808",
    "loginsPrevious": null,
    "firstName": "Ben",
    "lastName": "Mendis",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/58/5892_50.jpg",
    "clitterPreferencesUpdated": "2009-06-10T17:29:11.89"
  },
  {
    "idMember": 6804,
    "idPictureMember": 97880,
    "login": "Yo, Adrienne!",
    "phoneNumberUs": "9109886212",
    "loginsPrevious": "MidoriNotori, Boomshakalaka, Boomshakamomo",
    "firstName": "Adrienne",
    "lastName": "Mayton",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/68/6804_50.jpg",
    "clitterPreferencesUpdated": "2009-06-11T01:53:46.5"
  },
  {
    "idMember": 2067,
    "idPictureMember": 117989,
    "login": "Zordon",
    "phoneNumberUs": "2675955726",
    "loginsPrevious": "zimthehomicidal",
    "firstName": "Varnam",
    "lastName": "Clint",
    "pictureUrl": "http://assets.otakubooty.com/user/pic/20/2067_50.jpg",
    "clitterPreferencesUpdated": "2011-10-12T19:19:53.427"
  }
];
*/
//App.PhonenumbersController = Ember.ArrayController.extend({});
//App.PhonenumberController = Ember.ObjectController.extend();
/*
App.Phonenumber = Ember.Object.extend({
    loaded: false,

    id: function () {
        return this.get( '_id' );
    }.property( 'id' ),

    firstName: function () {
        return this.get( 'firstName' );
    }.property( 'firstName' ),

    lastName: function () {
        return this.get( 'lastName' );
    }.property( 'lastName' ),

    fullName: function () {
        var fullName = this.get( 'firstName' ) + " " + this.get( 'lastName' );
        return fullName;
    }.property( 'firstName', 'lastName' ),

    login: function () {
        return this.get( 'login' );
    }.property( 'login' ),

    phoneNumber: function () {
        var rawNumber = this.get('phoneNumberUs');
        var phoneNumber = rawNumber.substr(0,3)+'-'+rawNumber.substr(3,3)+'-'+rawNumber.substr(6);
        return phoneNumber;
    }.property( 'phoneNumber' ),



    loadNumbers: function() {
      if (this.get('loaded')) return;

      var api = this;
      $.getJSON("http://api.otakubooty.com/api/phonenumbers").then(function(response) {
        var links = Em.A();
        response.data.children.forEach(function (child) {
          links.push(App.Phonenumber.create(child.data));
        });
        api.setProperties({links: links, loaded: true});
      });
    }

//        _id:null,
//      idMember:0,
//      idPictureMember:0,
//      login:null,
//     phoneNumberUs:'0000000000',
//     loginsPrevious:null,
//     firstName:'John',
//     lastName:'Dough',
//     pictureUrl:null,
//     clitterPreferencesUpdated:null

});

App.Phonenumber.reopenClass({
    store: {},

    find: function(id) {
      if (!this.store[id]) {
        this.store[id] = App.Phonenumber.create({id: id});
      }
      return this.store[id];
    },
    all: function () {
        return this;
    }
});
*/
