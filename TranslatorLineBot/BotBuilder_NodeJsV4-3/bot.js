// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

const { ActivityHandler } = require('botbuilder');

const rp = require('request-promise');

class MyBot extends ActivityHandler {
    constructor() {
        super();
        // See https://aka.ms/about-bot-activity-message to learn more about the message and other activity types.
        //this.onMessage(async turnContext => { console.log('this gets called'); await turnContext.sendActivity(`You said '${ turnContext.activity.text }'`); });
        this.onMessage(async turnContext => {
            console.log('got a message.');

            // Add and set Translator Text API configuration
            const subscriptionKey = 'YOUR_SUBSCRIPTION_KEY';

            const options = {
                method: 'POST',
                baseUrl: 'https://api.cognitive.microsofttranslator.com/',
                url: 'translate',
                qs: {
                  'api-version': '3.0',
                  'to': 'ja'
                },
                headers: {
                  'Ocp-Apim-Subscription-Key': subscriptionKey,
                  'Content-type': 'application/json'
                },
                body: [{
                      'text': turnContext.activity.text
                }],
                json: true,
            };

            // Make web request to Translator Text API
            const responseBody = await rp(options);

            // Get API result and return answer
            const translated = responseBody[0].translations[0].text;
            const responseMessage = `「${translated}」って言ったね。`
                + `(検出言語: ${responseBody[0].detectedLanguage.language})`;

            await turnContext.sendActivity(responseMessage);
        });
        
        this.onConversationUpdate(async turnContext => { 
            console.log('this gets called (conversatgion update'); 
            //await turnContext.sendActivity('[conversationUpdate event detected]'); 
            });
    }
}

module.exports.MyBot = MyBot;
