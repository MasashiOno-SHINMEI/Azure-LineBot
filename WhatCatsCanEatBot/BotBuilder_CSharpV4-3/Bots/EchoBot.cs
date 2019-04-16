// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
// Adding libraries
using Microsoft.Bot.Builder.AI.QnA;
using Microsoft.Bot.Configuration;

namespace Microsoft.Bot.Builder.EchoBot
{
    public class EchoBot : ActivityHandler
    {
        // QnAMaker settings
        private static readonly QnAMakerService QnaService = new QnAMakerService()
        {
            Hostname = "https://YOUR_SERVER.azurewebsites.net/qnamaker",
            EndpointKey = "YOUR_ENDPOINT_KEY",
            KbId = "YOUR_KNOWLEDGEBASE_ID",
        };
        private static readonly QnAMaker QnaClient = new QnAMaker(QnaService);

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            //await turnContext.SendActivityAsync(MessageFactory.Text($"Echo: {turnContext.Activity.Text}"), cancellationToken);
            // Get answer from QnAMaker
            var qnaResult = await QnaClient.GetAnswersAsync(turnContext);
            if (qnaResult.Length > 0)
            {
                // Set answer from QnA Maker
                var responseMessage = MessageFactory.Text($"{qnaResult[0].Answer}");
                await turnContext.SendActivityAsync(responseMessage, cancellationToken);
            }
            else
            {
                // If no answer from QnAMaker
                await turnContext.SendActivityAsync(MessageFactory.Text($"ゴメンナサイ、分かりませんでした。他の食べ物を試してください。"));
            }
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"こんにちは！これ食べれるニャン？Bot だよ"), cancellationToken);
                }
            }
        }
    }
}
