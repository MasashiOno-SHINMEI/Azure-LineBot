// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
//ライブラリー追加
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Microsoft.Bot.Builder.EchoBot
{
    public class EchoBot : ActivityHandler
    {
        private readonly string translatorEndpoint = "https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&to=ja";
        private readonly string translatorSubscriptionKey = "YOUR_SUBSCRIPTION_KEY";

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            //await turnContext.SendActivityAsync(MessageFactory.Text($"Echo: {turnContext.Activity.Text}"), cancellationToken);

            /// ユーザー入力を翻訳して返す
            // ユーザー入力の取得
            var body = new object[] { new { Text = turnContext.Activity.Text } };
            var requestBody = JsonConvert.SerializeObject(body);

            // Translator Text API の利用
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(translatorEndpoint);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", translatorSubscriptionKey);
                var response = await client.SendAsync(request);

                // 結果の取得とデシリアライズ
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<TranslatorResult>>(jsonResponse);

                // 返答にセット
                var responseMessage =
                    $"「{result[0].translations[0].text}」って言ったね。" +
                    $"(検出言語: {result[0].detectedLanguage.language})";
                await turnContext.SendActivityAsync(responseMessage);
            }
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Welcome to Echo Bot."), cancellationToken);
                }
            }
        }

        // Translator API クラス(JSON デシリアライズ用)
        public class TranslatorResult
        {
            public Detectedlanguage detectedLanguage { get; set; }
            public List<Translation> translations { get; set; }
        }

        public class Detectedlanguage
        {
            public string language { get; set; }
            public float score { get; set; }
        }

        public class Translation
        {
            public string text { get; set; }
            public string to { get; set; }
        }
    }
}
