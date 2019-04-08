// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

const { ActivityHandler } = require('botbuilder');
const { QnAMaker } = require('botbuilder-ai');

const qnaEndpoint = {
    knowledgeBaseId: 'YOUR_KBID',
    endpointKey: 'YOUR_ENDPOINTKEY',
    host: 'https://YOUR_SERVER.azurewebsites.net/qnamaker'
};

class MyBot extends ActivityHandler {
    constructor() {
        super();
        var qnaMaker = new QnAMaker(qnaEndpoint);
        // See https://aka.ms/about-bot-activity-message to learn more about the message and other activity types.
        this.onMessage(
            async turnContext => { 
                const qnaResults = await qnaMaker.getAnswers(turnContext);
                console.log('this gets called');
                //await turnContext.sendActivity(`You said '${ turnContext.activity.text }'`); 
                if (qnaResults[0]) {
                    await turnContext.sendActivity(qnaResults[0].answer);
                } else {    
                        await turnContext.sendActivity('ゴメンナサイ、分かりませんでした。他の食べ物を試してください。');
                }
            });
        this.onConversationUpdate(
            async turnContext => {
                console.log('this gets called (conversatgion update');
                //await turnContext.sendActivity('[conversationUpdate event detected]'); 
            });
    }
}

module.exports.MyBot = MyBot;
