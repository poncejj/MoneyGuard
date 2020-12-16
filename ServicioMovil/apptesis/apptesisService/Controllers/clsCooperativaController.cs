using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using apptesisService.DataObjects;
using apptesisService.Models;

namespace apptesisService.Controllers
{
    public class clsCooperativaController : TableController<clsCooperativa>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            CooperativaServiceContext context = new CooperativaServiceContext();
            DomainManager = new EntityDomainManager<clsCooperativa>(context, Request);
        }

        // GET tables/clsCooperativa
        public IQueryable<clsCooperativa> GetAllclsCooperativa()
        {
            return Query(); 
        }

        // GET tables/clsCooperativa/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<clsCooperativa> GetclsCooperativa(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/clsCooperativa/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<clsCooperativa> PatchclsCooperativa(string id, Delta<clsCooperativa> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/clsCooperativa
        public async Task<IHttpActionResult> PostclsCooperativa(clsCooperativa item)
        {
            clsCooperativa current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/clsCooperativa/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteclsCooperativa(string id)
        {
             return DeleteAsync(id);
        }
    }
}
