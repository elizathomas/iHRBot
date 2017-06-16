namespace AuthDemoBot
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class CarouselCardsDialog : IDialog<object>
    {
        public static ForYouResponse forYouResponse;
        public static RadioStationList radioStationList;
         public CarouselCardsDialog()
        {

        }

        public CarouselCardsDialog(ForYouResponse forYouResponse)
        {
            forYouResponse = forYouResponse;
        }

        public CarouselCardsDialog(RadioStationList radioStationList)
        {
            radioStationList = radioStationList;
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var reply = context.MakeMessage();
            reply.AttachmentLayout = forYouResponse==null? AttachmentLayoutTypes.Carousel:AttachmentLayoutTypes.List;
            reply.Attachments = GetAttachments();

            await context.PostAsync(reply);

            context.Wait(this.MessageReceivedAsync);
        }

        private IList<Attachment> GetAttachments()
        {
            if (forYouResponse != null)
            {
               return GetForYouCardsAttachments(forYouResponse);
            }
            else if (radioStationList != null)
            {
               return GetRadioCardsAttachments(radioStationList);
            }
            return null;
        }

        private static  IList<Attachment> GetForYouCardsAttachments(ForYouResponse forYou)
        {
            var contentList = new List<Attachment>();
            if (forYou != null && forYou.values.Length > 0)
            {
                foreach (var item in forYou.values)
                {
                    contentList.Add(GetHeroCard(
                        item.label,
                    item.subLabel,
                    item.content.description,
                    new CardImage(url: item.imagePath),
                    new CardAction(ActionTypes.OpenUrl, "Learn more", value: "https://azure.microsoft.com/en-us/services/storage/")));
                }
            }
            return contentList;
        }

        private static IList<Attachment> GetRadioCardsAttachments(RadioStationList radioStationList)
        {
            var contentList = new List<Attachment>();
            if (radioStationList != null && radioStationList.hits.Length > 0)
            {
                foreach (var item in radioStationList.hits)
                {
                    contentList.Add(GetHeroCard(
                        item.name,
                    item.responseType,
                    item.description,
                    new CardImage(url: IhrHelper.GetFitImage(item.responseType,item.id.ToString())),
                    new CardAction(ActionTypes.OpenUrl, "Learn more", value: "https://azure.microsoft.com/en-us/services/storage/")));
                }
            }
            return contentList;
        }

        private static Attachment GetHeroCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new HeroCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                //Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }

        private static Attachment GetThumbnailCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new ThumbnailCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }
    }
}
