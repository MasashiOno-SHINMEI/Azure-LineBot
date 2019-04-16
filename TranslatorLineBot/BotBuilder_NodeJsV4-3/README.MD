# TranslatorLineBot (Node.js, Bot Framework v4.3.2 as of Apr 16, 2019)

[Azure Bot Service で翻訳チャットボット作成](https://qiita.com/annie/items/f8c5964bce2d15c26253) によるサンプルコードです。

bot.js に記載されている YOUR_SUBSCRIPTION_KEY にご自分の Tramslator Text API Key を入力してください。
[Translator Text のサブスクリプション申込方法 & API Key 取得方法](https://qiita.com/annie/items/f8c5964bce2d15c26253#211-translator-text-api-%E3%81%AE%E3%82%B5%E3%83%96%E3%82%B9%E3%82%AF%E3%83%AA%E3%83%97%E3%82%B7%E3%83%A7%E3%83%B3%E4%BD%9C%E6%88%90)

以下、こちらのサンプルコードの元になる EchoBot テンプレートの前提事項、準備をご確認いただき、ご利用ください。

# Echo Bot template

This bot has been created using [Bot Framework][1], it shows how to create a simple bot that accepts input from the user and echoes it back.

## Prerequisites
- [Node.js][4] version 10.14 or higher
    ```bash
    # determine node version
    node --version
    ```
# To run the bot locally
- Download the bot code from the Build blade in the Azure Portal (make sure you click "Yes" when asked "Include app settings in the downloaded zip file?").
    - If you clicked "No" you will need to copy all the Application Settings properties from your App Service to your local .env file.
- Install modules
    ```bash
    npm install
    ```
- Run the bot
    ```bash
    npm start

# Testing the bot using Bot Framework Emulator
[Bot Framework Emulator][5] is a desktop application that allows bot developers to test and debug their bots on localhost or running remotely through a tunnel.

- Install the Bot Framework Emulator version 4.3.0 or greater from [here][6]

## Connect to the bot using Bot Framework Emulator
- Launch Bot Framework Emulator
- File -> Open Bot
- Enter a Bot URL of `http://localhost:3978/api/messages`

# Deploy the bot to Azure
To learn more about deploying a bot to Azure, see [Deploy your bot to Azure][40] for a complete list of deployment instructions.

# Further reading
- [Bot Framework Documentation][20]
- [Bot Basics][32]
- [Azure Bot Service Introduction][21]
- [Azure Bot Service Documentation][22]
- [Deploy Your Bot to Azure][40]
- [Azure CLI][7]
- [msbot CLI][9]
- [Azure Portal][10]
- [Language Understanding using LUIS][11]
- [Restify][30]
- [dotenv][31]

[1]: https://dev.botframework.com
[2]: https://www.typescriptlang.org
[3]: https://www.typescriptlang.org/#download-links
[4]: https://nodejs.org
[5]: https://github.com/microsoft/botframework-emulator
[6]: https://github.com/Microsoft/BotFramework-Emulator/releases
[7]: https://docs.microsoft.com/en-us/cli/azure/?view=azure-cli-latest
[8]: https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest
[9]: https://github.com/Microsoft/botbuilder-tools/tree/master/packages/MSBot
[10]: https://portal.azure.com
[11]: https://www.luis.ai
[20]: https://docs.botframework.com
[21]: https://docs.microsoft.com/en-us/azure/bot-service/bot-service-overview-introduction?view=azure-bot-service-4.0
[22]: https://docs.microsoft.com/en-us/azure/bot-service/?view=azure-bot-service-4.0
[30]: https://www.npmjs.com/package/restify
[31]: https://www.npmjs.com/package/dotenv
[32]: https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-basics?view=azure-bot-service-4.0
[40]: https://aka.ms/azuredeployment
