# Twitter scraping notes

Chronjob runs every 5 minutes
look up last recorded tweet ID from otakubooty twitter
call twitter GET search for all tweets after last recorded ID
GET	http://search.twitter.com/search.json?q=#bootycon&since_id=(retrievedID)


parse returned json data and insert tweet records in using stub insert method

call twitter GET

https://dev.twitter.com/docs/using-search
https://dev.twitter.com/docs/api
https://dev.twitter.com/docs/api/1/get/search

probably don’t want to use streams
https://dev.twitter.com/docs/api/2/get/user
https://dev.twitter.com/docs/streaming-apis/streams/user