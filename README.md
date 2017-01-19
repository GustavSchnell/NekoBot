# NekoBot
[![Build status](https://ci.appveyor.com/api/projects/status/fjlpei28tsfwfd1i?svg=true)](https://ci.appveyor.com/project/dreanor/nekobot)

Discord Bot that posts random cat images and other things with an easy to implement plugin system.

##How to run:
https://discordapp.com/oauth2/authorize?&client_id=CLIENT_ID&scope=bot&permissions=0

##How to create a new plugin:

```c#
//Add the PluginContracts.dll to your project and implement the IPlugin interface

public class NekoPlugin : IPlugin
{
    public string Name => nameof(NekoPlugin);
    private DiscordClient client;

    public void Connect(DiscordClient client)
    {
        this.client = client;
        client.MessageReceived += Client_MessageReceived;
    }

    private void Client_MessageReceived(object sender, MessageEventArgs e)
    {
        if (!e.Message.IsAuthor && e.Message.Text.StartsWith("/cat"))
        {
            e.Channel.SendMessage("I'm a cat meow.);
        }
    }
}
'''

###Commands:
| Command | Description | Permission |
| ------------- | ------------- | ------------- |
| /cat  | Posts a random cat link from http://random.cat/  | @everyone |
| /8ball | Anwsers all your questions truthfully | @everyone |
