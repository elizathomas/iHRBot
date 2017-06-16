using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.IO;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading;

namespace AuthDemoBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        static AuthenticationResponse authInfo;
        static CarouselCardsDialog carouselCard;
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                if (carouselCard == null)
                {
                    carouselCard = new CarouselCardsDialog();
                }
                string message = activity.Text.Trim().ToLower();
                if (message == "login")
                {
                    authInfo = await IhrHelper.LoginToIHR();
                    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                    Activity reply = activity.CreateReply("You are logged in. \n\n Type 'for you' to get content");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (message == "for you")
                {
                    if (authInfo == null)
                    {
                        ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                        Activity reply = activity.CreateReply("You need to login before you can load: " + activity.Text);
                        await connector.Conversations.ReplyToActivityAsync(reply);
                    }
                    else
                    {
                        var forYouList = await IhrHelper.GetForYou(authInfo);

                        CarouselCardsDialog.forYouResponse = forYouList;
                        CarouselCardsDialog.radioStationList = null;
                        await Conversation.SendAsync(activity, () => carouselCard);

                    }
                }
                else if (message == "radio")
                {
                    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                    var radioStationList = await IhrHelper.GetRadioStations();
                   
                    CarouselCardsDialog.radioStationList = radioStationList;
                    CarouselCardsDialog.forYouResponse = null;
                    await Conversation.SendAsync(activity, () => carouselCard);
                }
                else
                {
                    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                    Activity reply = activity.CreateReply("# iHR Bot \n\nType the following command.\n\nradio -- Gets you top 10 Radio Stations\n\nlogin -- You will login to iHR\n\nfor you -- Gets you top 10 For You Stations");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
            }
            else
            {
                HandleSystemMessage(activity);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels

            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
                if (message.Action == "add")
                {
                    ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));
                    Activity reply = message.CreateReply("# Bot Help\n\nType the following command. (You need your Office 365 Exchange Online subscription.)\n\nlogin -- Login to Office 365\n\nget mail -- Get your e-mail from Office 365\n\nrevoke -- Revoke permissions for accessing your e-mail");
                    connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (message.Action == "remove")
                {

                }
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}