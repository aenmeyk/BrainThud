using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrainThud.Core.Models;
using BrainThud.Web.Authentication;
using BrainThud.Web.Data.AzureTableStorage;

namespace BrainThud.Web.Api.Controllers
{
    public class ConfigController : ApiControllerBase
    {
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly Lazy<ITableStorageContext> lazyTableStorageContext;
        private ITableStorageContext TableStorageContext { get { return this.lazyTableStorageContext.Value; } }

        public ConfigController(
            ITableStorageContextFactory tableStorageContextFactory,
            IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
            this.lazyTableStorageContext = new Lazy<ITableStorageContext>(() =>
                tableStorageContextFactory.CreateTableStorageContext(AzureTableNames.CARD, this.authenticationHelper.NameIdentifier));
        }

        public UserConfiguration Get()
        {
            return this.TableStorageContext.UserConfigurations.GetByNameIdentifier();
        }

        public HttpResponseMessage Put(UserConfiguration userConfiguration)
        {
            if (this.ModelState.IsValid)
            {
                this.TableStorageContext.UserConfigurations.Update(userConfiguration);
                this.TableStorageContext.Commit();

                return this.Request.CreateResponse(HttpStatusCode.OK, userConfiguration);
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }
    }
}