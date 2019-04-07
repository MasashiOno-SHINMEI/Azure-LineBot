// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
//ライブラリー追加
using Microsoft.Bot.Builder.AI.QnA;
using Microsoft.Bot.Configuration;

namespace Microsoft.Bot.Builder.EchoBot
{
    public class EchoBot : ActivityHandler
    {
        // QnAMaker の設定
        private readonly QnAMakerService qnaService = new QnAMakerService()
        {
            Hostname = "https://YOUR_SERVER.azurewebsites.net/qnamaker",
            EndpointKey = "YOUR_ENDPOINTKEY",
            KbId = "YOUR_KBID",
        };
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            //await turnContext.SendActivityAsync(MessageFactory.Text($"Echo: {turnContext.Activity.Text}"), cancellationToken);
            // QnAMaker で回答を取得する
            var qnaClient = new QnAMaker(qnaService);
            var qnaResult = await qnaClient.GetAnswersAsync(turnContext);
            // 取得した回答をセット
            var responseMessage = $"{qnaResult[0].Answer}";
            await turnContext.SendActivityAsync(responseMessage);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"こんにちは！"), cancellationToken);
                }
            }
        }
    }
}
