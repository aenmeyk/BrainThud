using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using BrainThud.Core;
using BrainThud.Web.ActionResults;
using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Controllers
{
    public class SitemapController : Controller
    {
        private readonly Lazy<ITableStorageContext> lazyTableStorageContext;
        private ITableStorageContext TableStorageContext { get { return this.lazyTableStorageContext.Value; } }
        
        public SitemapController(ITableStorageContextFactory tableStorageContextFactory)
        {
            this.lazyTableStorageContext = new Lazy<ITableStorageContext>(() =>
                tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD));
        }

        [OutputCache(Duration = 3600)]
        public ActionResult Feed()
        {
            var cards = this.TableStorageContext.Cards.GetAll()
                .Where(x => x.PartitionKey != ConfigurationSettings.TEST_PARTITION_KEY && 
                    x.PartitionKey != ConfigurationSettings.DEV_PARTITION_KEY && 
                    x.PartitionKey != ConfigurationSettings.DEV_PARTITION_KEY_2)
                .ToList();

            var items = new List<SyndicationItem>();

            foreach(var card in cards)
            {
                var title = card.Question.Substring(0, Math.Min(card.Question.Length, ConfigurationSettings.PAGE_TITLE_LENGTH));
                var uri = new Uri(string.Format("http://www.brainthud.com/cards/{0}/{1}/{2}", card.UserId, card.EntityId, card.CardSlug));

                var item = new SyndicationItem(title, string.Empty, uri, card.EntityId.ToString(CultureInfo.InvariantCulture), card.Timestamp);
                items.Add(item);
            }

            var feed = new SyndicationFeed("BrainThud", string.Empty, new Uri("http://www.brainthud.com"), "BrainThud", DateTime.UtcNow);
            feed.Items = items;

            return new RssActionResult { Feed = feed };
        }
    }
}